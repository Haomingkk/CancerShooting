using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth;

    private int currentHealth;

    //invincible time
    public float invicibleLength;
    private float invicibleCounter;

    private void Awake()
    {
        instance = this;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH:" + currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("It works!");
        if(invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if(invicibleCounter <= 0)
        {
            currentHealth -= damageAmount;
            UIController.instance.ShowDamage();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);

                GameManager.instance.PlayerDied();
            }

            invicibleCounter = invicibleLength;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH:" + currentHealth + "/" + maxHealth;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH:" + currentHealth + "/" + maxHealth;
    }
}
