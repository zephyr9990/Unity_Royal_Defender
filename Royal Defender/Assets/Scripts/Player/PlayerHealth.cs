using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    GameObject DeathMenu;

    public int maxHealth = 100;     //Starting health and maximum health for player
    public Slider healthSlider;     //UI slider component
    public AudioClip deathAudio;    //Death Audio Clip


    Animator anim;                  //Player anim component
    AudioSource playerAudio;        //Player audio component
    PlayerMovement PlayerMovement;  //References Player Movement Script
    PlayerCombat PlayerCombat;      //References Player Combat Script
    PlayerInventory playerInventory; //References PlayerInventory Script

    bool isDead;                    //Changed to true when currentHealth = 0

    private int currentHealth;      //Current health for the player

    void Awake()
    {
        //Set up referenced components
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerCombat = GetComponent<PlayerCombat>();
        playerInventory = GetComponent < PlayerInventory >();

        // Set up the initial player health
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        //Update Player Health
        currentHealth -= damageAmount;

        //Update Health Slider
        healthSlider.value = currentHealth;

        //Check if the player is dead
        if (currentHealth <= 0 && !isDead)
        {
            //Kill the player
            Death();
        }
    }

    public void RestoreHealth(int restoreAmount)
    {
        //Update Player Health
        currentHealth += restoreAmount;

        //Update Health Slider
        healthSlider.value = currentHealth;

        //Do not let health exceed maximum
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool isGreaterThanZero()
    {
        return currentHealth > 0;
    }


    public void Death()
    {
        //Set the death flag so this function does not repeat
        isDead = true;

        //TODO Comment out for player death.
        //Tell the animator to play the death animation
        anim.SetBool("IsDead", true);

        //Play death audio clip
       /* playerAudio.clip = deathAudio;
        playerAudio.Play();*/
            
        //Turn off player functions
        PlayerMovement.enabled = false;
        PlayerCombat.enabled = false;
        playerInventory.enabled = false;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        DeathMenu.gameObject.SetActive(true);
    }
}
