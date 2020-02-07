using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingEventHandler : MonoBehaviour
{

    private PlayerEquippedWeapon equippedWeapon;
    private Animator animator;

    private void Awake()
    {   
        equippedWeapon = GetComponent<PlayerEquippedWeapon>();
        animator = GetComponent<Animator>();
    }

    void Swing()
    {
        WeaponInfo weaponSwung = equippedWeapon.GetWeaponInfo();
        if (weaponSwung == null)
            return;

        EnableWeapon(true);
    }

    void StopSwinging()
    {
        animator.SetBool("IsSwinging", false);

        WeaponInfo weaponSwung = equippedWeapon.GetWeaponInfo();
        if (weaponSwung == null)
            return;


        EnableWeapon(false);
    }

    private void EnableWeapon(bool value)
    {
        GameObject weapon = equippedWeapon.GetEquippedMeleeWeaponObject();
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
