using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, jumpPower, runSpeed = 12f;
    public CharacterController charCon;

    public float gravityModifier;
    private Vector3 moveInput;

    public Transform camTrans;
    public float mouseSensibivity;

    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    public GameObject bullet;
    public Transform firePoint;
    //public List<Transform> allfirePoint = new List<Transform>();

    public Gun activeGun;

    public List<Gun> allGuns = new List<Gun>();
    public int currentGun;

    public GameObject muzzleFlash;

    //public GameObject camera;

    public float CheckAngle(float value)
    {
        float angle = value - 180;

        if (angle > 0)
            return angle - 180;

        return angle + 180;
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        firePoint = activeGun.firePoint;
        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmoInGun + "/" + activeGun.currentTotalAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        //charCon.Move(moveInput);

        //store y velocity
        float yStore = moveInput.y;

        //Handle Moving
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = (vertMove + horiMove);
        moveInput.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;
        }
        moveInput.y = yStore;
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if(charCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            //print(charCon.velocity.magnitude);
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;

        //Handle Jumping
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
        }else if(Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }

        charCon.Move(moveInput * Time.deltaTime);


        //insight rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensibivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        camTrans.rotation = Quaternion.Euler(camTrans.transform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        //camTrans.rotation = Quaternion.Euler(new Vector3(CheckAngle(camTrans.transform.rotation.eulerAngles.x), camTrans.transform.rotation.eulerAngles.y, camTrans.transform.rotation.eulerAngles.z) + new Vector3(-mouseInput.y, 0f, 0f));


        muzzleFlash.SetActive(false);
        //Handle Shooting

        //reload the gun
        if (Input.GetKeyDown(KeyCode.R))
        {
            activeGun.ReloadGun();
            UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmoInGun + "/" + activeGun.currentTotalAmmo;
        }

        //single shoot
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {
            
            RaycastHit hit;
            //if (Physics.Raycast(camTrans.position + new Vector3(0, 1f, 0), camTrans.forward, out hit, 50f))
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            //if not hit sth.
            else
            {
                //firePoint.LookAt(camTrans.position + new Vector3(0, 1f, 0) + (camTrans.forward * 30f));
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }
            FireShot();
        }

        //consistent shoot
        if(Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if(activeGun.fireCounter <= 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
                {
                    if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                    {
                        firePoint.LookAt(hit.point);
                    }
                }
                //if not hit sth.
                else
                {
                    //firePoint.LookAt(camTrans.position + new Vector3(0, 1f, 0) + (camTrans.forward * 30f));
                    firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
                }
                FireShot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentGun++;
            if (currentGun >= allGuns.Count)
            {
                currentGun = 0;
            }
            SwitchGun();
            
        }

        anim.SetFloat("moveSpeed", moveInput.magnitude);
        anim.SetBool("onGround", canJump);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("Hit 1!");
            if(currentGun != 0)
            {
                currentGun = 0;
                SwitchGun();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Debug.Log("Hit 2!");
            if (currentGun != 1)
            {
                currentGun = 1;
                SwitchGun();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Debug.Log("Hit 1!");
            if (currentGun != 2)
            {
                currentGun = 2;
                SwitchGun();
            }
        }
    }

    public void FireShot()
    {
        if(activeGun.currentAmmoInGun > 0)
        {
            activeGun.currentAmmoInGun--;

            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);

            activeGun.fireCounter = activeGun.fireRate;

            UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmoInGun + "/" + activeGun.currentTotalAmmo;

            muzzleFlash.SetActive(true);
        }
        
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        firePoint = activeGun.firePoint;
        Camera.main.fieldOfView = 60f;
        StartCoroutine(WaitOneMillisecond());
        //UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmoInGun + "/" + activeGun.currentTotalAmmo;
    }

    public IEnumerator WaitOneMillisecond()
    {
        yield return new WaitForFixedUpdate();
        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmoInGun + "/" + activeGun.currentTotalAmmo;
    }
}
