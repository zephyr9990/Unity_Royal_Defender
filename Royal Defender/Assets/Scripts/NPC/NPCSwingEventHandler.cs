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
        if (weaponSwung == null)
            return;

        GameObject weapon = npcEquippedWeapon.GetEquippedMeleeWeaponObject();
        SetColliderEnabled(weapon, true);
        weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
    }

    void StopSwinging()
    {
        animator.SetBool("IsSwinging", false);

        WeaponInfo weaponSwung = npcEquippedWeapon.GetWeaponInfo();
        if (weaponSwung == null)
            return;

        GameObject weapon = npcEquippedWeapon.GetEquippedMeleeWeaponObject();
        SetColliderEnabled(weapon, false);
        weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
    }

    private void SetColliderEnabled(GameObject weapon, bool value)
    {
        weapon.GetComponent<BoxCollider>().enabled = value;
    }
}
