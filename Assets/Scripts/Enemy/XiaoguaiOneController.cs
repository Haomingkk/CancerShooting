using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XiaoguaiOneController : EnemyController
{
    void Start()
    {
        startPoint = transform.position;
        shootWaitCounter = waitBetweenShoots;
        shootTimeCounter = timeToShoot;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.timeFlow == false)
        {
            agent.destination = transform.position;
            return;
        }
        EnemyStatus();

    }

    public override void Attack()
    {
        base.Attack();
        //shooting time interval
        if (shootWaitCounter > 0)
        {
            shootWaitCounter -= Time.deltaTime;
            shootTimeCounter = timeToShoot;
            //anim.SetBool("isMoving", true);
        }
        else
        {
            shootTimeCounter -= Time.deltaTime;
            if (shootTimeCounter >= 0)
            {
                //bullet interval
                fireCount -= Time.deltaTime;



                if (fireCount <= 0)
                {
                    fireCount = fireRate;

                    firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));
                    //check the angle to the Player
                    Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                    float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                    if (Mathf.Abs(angle) < 60f)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);

                        anim.SetTrigger("fireShoot");
                    }
                    else
                    {
                        shootWaitCounter = waitBetweenShoots;
                    }

                }
                agent.destination = transform.position;

                anim.SetBool("isMoving", false);
            }
            else
            {
                shootWaitCounter = waitBetweenShoots;
            }

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
}
