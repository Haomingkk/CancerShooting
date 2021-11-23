using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int currentHealth = 5;

    [HideInInspector]
    public Vector3 targetPoint, startPoint;

    [HideInInspector]
    public bool chasing;

    public float distanceToChase = 20f, distanceToLose = 30f, distanceToStop = 8f;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;

    [HideInInspector]
    public float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate;

    [HideInInspector]
    public float fireCount;


    public float waitBetweenShoots = 2f, timeToShoot = 1f;

    [HideInInspector]
    public float shootWaitCounter, shootTimeCounter;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //µÐÈË×·»÷AI
    public virtual void EnemyStatus()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) <= distanceToChase)
            {
                chasing = true;

                fireCount = 0.1f;
                shootWaitCounter = waitBetweenShoots;
                shootTimeCounter = timeToShoot;
            }

            //wait chaseCounter seconds then turn back
            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
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
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
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
            if (Vector3.Distance(transform.position, targetPoint) >= distanceToLose)
            {
                chasing = false;

                chaseCounter = keepChasingTime;

                if (shootTimeCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
            }

            Attack();
            

        }
    }

    public virtual void DamageEnemy(int damageAmount = 1)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Attack() { }
}
