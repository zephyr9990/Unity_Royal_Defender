using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapWeapon : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private PlayerEquippedWeapon playerEquippedWeapon;
    private ArrayList NPCs;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = transform.parent.GetComponent<PlayerInventory>();
        playerEquippedWeapon = transform.parent.GetComponent<PlayerEquippedWeapon>();
        NPCs = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCs.Count <= 0)
            return;

        GameObject closestNPC = GetClosestNPC();
        // TODO make closestNPC show "Swap Weapon" UI 

        if (Input.GetButtonDown("SwapWeapon"))
        {
            SwapWeapons(closestNPC);
        }
    }

    private GameObject GetClosestNPC()
    {
        Vector3 distanceToClosestNPC = Vector3.zero;
        GameObject closestNPC = null;
        foreach (GameObject NPC in NPCs)
        {
            Vector3 distanceToNPC = NPC.transform.position - transform.parent.position;

            if (distanceToClosestNPC == Vector3.zero)
            {
                distanceToClosestNPC = distanceToNPC;
                closestNPC = NPC;
            }
            else if (distanceToNPC.magnitude < distanceToClosestNPC.magnitude)
            {
                distanceToClosestNPC = distanceToNPC;
                closestNPC = NPC;
            }
        }

        return closestNPC;
    }


    private void SwapWeapons(GameObject closestNPC)
    {
        NPCEquippedWeapon NPCEquippedWeapon = closestNPC.GetComponent<NPCEquippedWeapon>();
        WeaponInfo NPCWeapon = NPCEquippedWeapon.GetWeaponInfo();
        NPCEquippedWeapon.UnequipWeapon();

        WeaponInfo playerWeapon = playerEquippedWeapon.GetWeaponInfo();
        playerEquippedWeapon.UnequipWeapon();

        NPCEquippedWeapon.EquipWeapon(playerWeapon);
        playerEquippedWeapon.EquipWeapon(NPCWeapon);

        playerInventory.SwapWeapons(playerWeapon, NPCWeapon);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCs.Remove(other.gameObject);
        }
    }
}
