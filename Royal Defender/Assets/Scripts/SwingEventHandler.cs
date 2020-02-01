using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingEventHandler : MonoBehaviour
{
    public EquippedWeapon equippedWeapon;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Swing()
    {
        SetColliderEnabled(true);
        GameObject weapon = equippedWeapon.GetEquippedMeleeWeaponObject();
        weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
    }

    void StopSwinging()
    {
        animator.SetBool("IsSwinging", false);
        SetColliderEnabled(false);
        GameObject weapon = equippedWeapon.GetEquippedMeleeWeaponObject();
        weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
    }
    private void SetColliderEnabled(bool value)
    {
        GameObject weapon = equippedWeapon.GetEquippedMeleeWeaponObject();
        weapon.GetComponent<BoxCollider>().enabled = value;
    }
}
