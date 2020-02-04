using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSwingEventHandler : MonoBehaviour
{

    private NPCEquippedWeapon npcEquippedWeapon;
    private Animator animator;

    private void Awake()
    {
        npcEquippedWeapon = GetComponent<NPCEquippedWeapon>();
        animator = GetComponent<Animator>();
    }

    void Swing()
    {
        WeaponInfo weaponSwung = npcEquippedWeapon.GetWeaponInfo();
        if (weaponSwung == null || weaponSwung.type == WeaponType.Ranged)
            return; // if there's no weapon or if player swaps out weapon mid-swing

        EnableWeapon(true);
    }

    void StopSwinging()
    {
        animator.SetBool("IsSwinging", false);

        WeaponInfo weaponSwung = npcEquippedWeapon.GetWeaponInfo();
        if (weaponSwung == null || weaponSwung.type == WeaponType.Ranged)
            return; // if there's no weapon or if player swaps out weapon mid-swing

        EnableWeapon(false);
    }

    private void EnableWeapon(bool value)
    {
        GameObject weapon = npcEquippedWeapon.GetEquippedMeleeWeaponObject();
        SetColliderEnabled(weapon, value);

        if (value == true)
        { weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play(); }
        else
        { weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop(); }
    }

    private void SetColliderEnabled(GameObject weapon, bool value)
    {
        weapon.GetComponent<BoxCollider>().enabled = value;
    }
}
