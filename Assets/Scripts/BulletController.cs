using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public Rigidbody theRB;

    public GameObject impactEffect;

    public bool damageEnemy, damagePlayer;

    public int bulletDanage = 1;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        theRB.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    //destroy the bullet when it collide with sth.
    private void OnTriggerEnter(Collider other)
    {
        //hit enemy
        if (other.gameObject.tag == "HeadShoot" && damageEnemy)
        {
            //Destroy(other.gameObject);
            other.transform.parent.GetComponent<EnemyController>().DamageEnemy(bulletDanage * 2);
        }
        else if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyController>().DamageEnemy(bulletDanage);
        }
        
        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            //Debug.Log("Hit Player at" + transform.position);
            PlayerHealthController.instance.DamagePlayer(bulletDanage);
        }

        //bullet effect
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }
}
