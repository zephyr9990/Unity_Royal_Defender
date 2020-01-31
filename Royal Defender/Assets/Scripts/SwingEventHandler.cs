using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingEventHandler : MonoBehaviour
{
    public GameObject meleeSocket;

    private Animator animator;

    private float timer;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Swing()
    {
        SetColliderEnabled(true);
        GameObject weapon = GetWeaponObject();
        weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
    }

    void StopSwinging()
    {
        SetColliderEnabled(false);
        animator.SetBool("IsSwinging", false);
        GameObject weapon = GetWeaponObject();
        weapon.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
    }
    private void SetColliderEnabled(bool value)
    {
        GameObject weapon = GetWeaponObject();
        weapon.GetComponent<BoxCollider>().enabled = value;
    }

    private GameObject GetWeaponObject()
    {
        // player switches weapons so must be called actively.
        return meleeSocket.transform.GetChild(0).gameObject;
    }
}
