using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    int maxHealth;
    int currentHealth;

    public GameObject healthText;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth=100;
        currentHealth=100;
        UpdateHealthText();
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthText()
    {
        healthText.GetComponent<Text>().text="Current Health:\n"+currentHealth+"/"+maxHealth;
    }

    public void takeDamage(int amount)
    {
        currentHealth-=amount;
        UpdateHealthText();
        if(currentHealth<=0)
        {
            die();
        }
    }

    void die()
    {
        gameOverText.SetActive(true);
    }
}
