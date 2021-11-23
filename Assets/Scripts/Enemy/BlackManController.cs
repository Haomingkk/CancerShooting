using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackManController : EnemyController
{

   // private Vector3 targetPoint, startPoint;
   //
   // private bool chasing;
   //
   // private float chaseCounter;
   //
   //
   // private float fireCount;
   //
   // private float shootWaitCounter, shootTimeCounter;

    // Start is called before the first frame update
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
}
