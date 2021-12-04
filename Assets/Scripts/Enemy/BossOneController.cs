using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossOneController : EnemyController
{
    public List<GameObject> allLaser = new List<GameObject>();
    public Vector3 laserPosition;

    public float f1Interval, f2Interval;

    private float f1Count, f2Count;

    public GameObject bullet2;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        f1Count = f1Interval * 0.5f;
        f2Count = f2Interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.timeFlow == false)
        {
            return;
        }
        transform.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));
        f1Count -= Time.deltaTime;
        f2Count -= Time.deltaTime;
        if (f1Count<=0)
        {
            Debug.Log("Function1 works!");
            anim.SetTrigger("f1");
            function1();
            f1Count = f1Interval;
        }
        if (f2Count<=0)
        {
            Debug.Log("Function2 works!");
            anim.SetTrigger("f2");
            function2();
            f2Count = f2Interval;
        }


    }

    public override void EnemyStatus() { }


    //投掷食物
    public void function1()
    {
        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));
        Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        if (Mathf.Abs(angle) < 60f)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);

        }
    }

    //发射激光
    public void function2()
    {
        //int laserNumber = Random.Range(0, allLaser.Count);
        //Instantiate(allLaser[laserNumber], laserPosition, transform.rotation);
        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));
        Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        if (Mathf.Abs(angle) < 60f)
        {
            Instantiate(bullet2, firePoint.position, firePoint.rotation);

        }
    }

    public override void DamageEnemy(int damageAmount = 1)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            anim.SetTrigger("isDead");
            StartCoroutine(WaitToDestroy());
        }
    }

    public IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    //到时候给触发器加个script写在里面
    //private void OnTriggerEnter(Collider other)
    //{
    //    //hit enemy
    //    if (other.gameObject.tag == "Boss1Collider")
    //    {
    //        function1.setActive(true);
    //    }
    //}

}
