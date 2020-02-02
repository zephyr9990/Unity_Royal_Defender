using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeHealth : MonoBehaviour
{
    public int maxHealth = 100;     //Starting health and maximum health for cube
    public Slider healthSlider;     //UI slider component
    public AudioClip deathAudio;    //Death Audio Clip


    Animator anim;                  //cube anim component
    AudioSource cubeAudio;          //cube audio component

    CubeRotator CubeRotator;        //Reference the CubeRotator Script

    bool isDead;                    //Changed to true when currentHealth = 0

    private int currentHealth;      //Current health for the cube

    void Awake()
    {
        //Set up referenced components
        anim = GetComponent<Animator>();
        cubeAudio = GetComponent<AudioSource>();
        CubeRotator = GetComponent<CubeRotator>();

        // Set up the initial cube health
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        //Update cube Health
        currentHealth -= damageAmount;

        //Update Health Slider
        healthSlider.value = currentHealth;

        //Update Cube Rotator Speed based on current health

        if (currentHealth <= maxHealth / 2)
        {
            CubeRotator.speed = 2;
        }
        else if (currentHealth <= maxHealth / 4)
        {
            CubeRotator.speed = 3;
        } 

        //Check if the cube is dead
        if (currentHealth <= 0 && !isDead)
        {
            //Kill the cube and end the game
            Death();
        }
    }

    public void RestoreHealth(int restoreAmount)
    {
        //Update cube Health
        currentHealth += restoreAmount;

        //Update Health Slider
        healthSlider.value = currentHealth;

        //Do not let health exceed maximum
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Death()
    {
        //Set the death flag so this function does not repeat
        isDead = true;

        //Tell the animator to play the death animation
        anim.SetTrigger("Die");

        //Play death audio clip
        cubeAudio.clip = deathAudio;
        cubeAudio.Play();
    }
}
