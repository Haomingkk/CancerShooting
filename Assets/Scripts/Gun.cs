using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;

    public bool canAutoFire;

    public float fireRate;

    public int maxTotalAmmo;
    public int maxAmmoInGun;

    [HideInInspector]
    public int currentAmmoInGun;

    [HideInInspector]
    public int currentTotalAmmo;

    [HideInInspector]
    public float fireCounter;

    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        currentTotalAmmo = maxTotalAmmo;
        currentAmmoInGun = maxAmmoInGun;
    }

    //private void OnEnable()
    //{
    //    currentTotalAmmo = maxTotalAmmo;
    //    currentAmmoInGun = maxAmmoInGun;
    //}

    // Update is called once per frame
    void Update()
    {
        if(fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void ReloadGun()
    {
        if (currentAmmoInGun < maxAmmoInGun)
        {
            if (currentTotalAmmo >= maxAmmoInGun - currentAmmoInGun)
            {
                currentTotalAmmo -= maxAmmoInGun - currentAmmoInGun;
                currentAmmoInGun = maxAmmoInGun;
            }
            else
            {
                currentAmmoInGun += currentTotalAmmo;
                currentTotalAmmo = 0;
            }
        }
    }
}
