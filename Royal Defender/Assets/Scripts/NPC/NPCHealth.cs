using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 500;     //Starting health and maximum health for NPC
    public AudioClip downAudio;    //Down Audio Clip

    Animator anim;                  //NPC animator component
    AudioSource npcAudio;           //NPC Audio component

    bool isDown;                    //Changed to true when health = 0

    private int currentHealth;      //Current health for NPC

    void Awake()
    {
        //Set up references
        anim = GetComponent<Animator>();
        npcAudio = GetComponent<AudioSource>();

        // Set up the initial NPC health
        currentHealth = maxHealth;
        isDown = true;
    }

    public void TakeDamage(int damageAmount)
    {
        //Update NPC Health
        currentHealth -= damageAmount;

        //Check if the NPC is Down
        if (currentHealth <= 0 && !isDown)
        {
            //Incapacitate the NPC
            Down();
        }
    }

    public void RestoreHealth(int restoreAmount)
    {
        //Update NPC  Health
        currentHealth += restoreAmount;

        //Do not let health exceed maximum
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Down()
    {
        //Set the death flag so this function does not repeat
        isDown = true;

        //Tell the animator to play the Down animation
        anim.SetTrigger("Down");

        //Play down audio clip
        npcAudio.clip = downAudio;
        npcAudio.Play();
    }

    public void SetIsDown(bool value)
    {
        isDown = value;
    }

    public bool GetIsDown()
    {
        return isDown;
    }
}
