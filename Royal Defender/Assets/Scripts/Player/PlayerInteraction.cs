﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private PlayerEquippedWeapon playerEquippedWeapon;
    private ArrayList npcs;
    private float timeBetweenReviveCalls;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = transform.parent.GetComponent<PlayerInventory>();
        playerEquippedWeapon = transform.parent.GetComponent<PlayerEquippedWeapon>();
        npcs = new ArrayList();
        timeBetweenReviveCalls = 1f;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (npcs.Count <= 0)
            return;

        GameObject closestNPC = GetClosestNPC();
        // TODO make closestNPC show "Swap Weapon" UI 

        if (Input.GetButtonDown("SwapWeapon"))
        {
            SwapWeapons(closestNPC);
        }

        if (Input.GetButton("Revive") && timer > timeBetweenReviveCalls)
        {
            timer = 0f;
            Revive(closestNPC);
        }
    }

    private GameObject GetClosestNPC()
    {
        Vector3 distanceToClosestNPC = Vector3.zero;
        GameObject closestNPC = null;
        foreach (GameObject npc in npcs)
        {
            Vector3 distanceToNPC = npc.transform.position - transform.parent.position;

            if (distanceToClosestNPC == Vector3.zero)
            {
                distanceToClosestNPC = distanceToNPC;
                closestNPC = npc;
            }
            else if (distanceToNPC.magnitude < distanceToClosestNPC.magnitude)
            {
                distanceToClosestNPC = distanceToNPC;
                closestNPC = npc;
            }
        }

        return closestNPC;
    }


    private void SwapWeapons(GameObject closestNPC)
    {
        NPCEquippedWeapon NPCEquippedWeapon = closestNPC.GetComponent<NPCEquippedWeapon>();
        WeaponInfo NPCWeapon = NPCEquippedWeapon.GetWeaponInfo();
        WeaponInfo playerWeapon = playerEquippedWeapon.GetWeaponInfo();

        if (NPCEquippedWeapon.enabled == false || playerWeapon == null && NPCWeapon == null)
            return; // nothing to exchange.

        //NPCEquippedWeapon.UnequipWeapon();
        //playerEquippedWeapon.UnequipWeapon();

        NPCEquippedWeapon.EquipWeapon(playerWeapon);
        playerEquippedWeapon.EquipWeapon(NPCWeapon);

        playerInventory.SwapWeapons(playerWeapon, NPCWeapon);
    }

    private void Revive(GameObject closestNPC)
    {
        if (closestNPC)
        {
            NPCRevive npcRevive = closestNPC.GetComponent<NPCRevive>();
            npcRevive.BeginReviving();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npcs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npcs.Remove(other.gameObject);
        }
    }
}