using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    //public Rigidbody theRB;
    private Vector3 targetPoint, startPoint;

    private bool chasing;
    public float distanceToChase = 20f, distanceToLose = 30f, distanceToStop = 8f;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate;
    private float fireCount;

    public float waitBetweenShoots = 2f, timeToShoot = 1f;
    private float shootWaitCounter, shootTimeCounter;

    public Animator anim;

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

        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            if(Vector3.Distance(transform.position, targetPoint) <= distanceToChase)
            {
                chasing = true;

                fireCount = 0.1f;
                shootWaitCounter = waitBetweenShoots;
                shootTimeCounter = timeToShoot;
            }
            
            //wait chaseCounter seconds then turn back
            if(chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if(chaseCounter <= 0)
                {
                    agent.destination = startPoint;

                    
                }
                
            }

            if (agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        //if the enemy is chasing
        else
        {
            //transform.LookAt(targetPoint);
            //theRB.velocity = transform.forward * moveSpeed;

            //if the enemy is closed enough
            if(Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = PlayerController.instance.transform.position;

                anim.SetBool("isMoving", true);
            }
            else
            {
                agent.destination = transform.position;

                anim.SetBool("isMoving", false);
            }

            //if the enemy is far away enough
            if(Vector3.Distance(transform.position, targetPoint) >= distanceToLose)
            {
                chasing = false;
                
                chaseCounter = keepChasingTime;
                
                if(shootTimeCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
            }

            //shooting time interval
            if (shootWaitCounter > 0)
            {
                shootWaitCounter -= Time.deltaTime;
                shootTimeCounter = timeToShoot;
                anim.SetBool("isMoving", true);
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
}
