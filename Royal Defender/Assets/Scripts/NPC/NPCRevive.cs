using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class NPCRevive : MonoBehaviour
{
    public int amountNeededToRevive = 500;
    public int amountAddedPerReviveCall = 100;
    public int amountSubtractedPerReviveCall = 100;
    public PointsManager pointsManager;

    private NPCHealth npcHealth;
    private Animator animator;
    private NPCUI npcUI;
    private float currentReviveProgress;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcUI = GetComponent<NPCUI>();
        npcHealth = GetComponent<NPCHealth>();
        currentReviveProgress = 0;
    }

    private void Start()
    {
        npcUI.SetSliderText("0/" + amountNeededToRevive);
    }

    public void BeginReviving()
    {
        if (PointsManager.points - amountSubtractedPerReviveCall < 0
            || animator.GetBool("IsRevived")) 
            return; // not enough points to activate revive tick or is already revived.

        pointsManager.DecreasePoints(amountSubtractedPerReviveCall);

        currentReviveProgress += amountAddedPerReviveCall;
        npcUI.SetSliderValue(currentReviveProgress / amountNeededToRevive);
        npcUI.SetSliderText(currentReviveProgress + "/" + amountNeededToRevive);
        
        if (currentReviveProgress == amountNeededToRevive)
        {
            animator.SetBool("IsRevived", true);
        }
    }

    private void GetUp()
    {
        npcHealth.SetIsDown(false);
        GetComponent<NPCMovement>().enabled = true;
        GetComponent<NPCCombat>().enabled = true;
        GetComponent<NPCEquippedWeapon>().enabled = true;
    }
}
