﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect_Ranged : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

            // stores info into a struct so that information is saved when object is destroyed
            WeaponInfo weaponInfo = new WeaponInfo(GetComponent<WeaponProperties>());
            playerInventory.AddRangedWeapon(weaponInfo);
            Destroy(gameObject);
        }
    }
}
