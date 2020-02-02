using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float timeBetweenShots = 0.15f;
    public float timeBetweenSwings = .6f;
    public float range = 100.0f;
    public GameObject gunSocket;

    private float effectsDisplayTime = .2f;
    private float timer = 0;

    private PlayerInventory playerInventory;
    private Animator animator;
    private PlayerEquippedWeapon equippedWeapon;
    private LockOnScript lockOn;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
        equippedWeapon = GetComponent<PlayerEquippedWeapon>();
        lockOn = GetComponentInChildren<LockOnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        WeaponInfo currentWeapon = equippedWeapon.GetWeaponInfo();

        if (currentWeapon == null)
        {
            StopAttackingAnimations();
            return;
        }

        if (currentWeapon.type == WeaponType.Ranged)
        {
            CheckIfPlayerWantsToShoot();
        }
        else // player has melee weapon
        {
            CheckIfPlayerWantsToSwing();
        }


    }

    private void CheckIfPlayerWantsToSwing()
    {
        if (Input.GetButtonDown("Attack"))
        {
            bool isSprinting = animator.GetBool("IsSprinting");
            if (isSprinting)
                return; // cannot swing weapon if sprinting;

            if (timer > timeBetweenSwings)
            {
                timer = 0f;
                animator.SetBool("IsSwinging", false);
                animator.SetBool("IsSwinging", true);
            }
        }
    }

    private void CheckIfPlayerWantsToShoot()
    {
        if (Input.GetButton("Attack"))
        {
            if (timer > timeBetweenShots
                && animator.GetBool("LockOnToggled"))
            {
                Shoot();
            }
            else
            {
                StopAttackingAnimations();
            }
        }

        if (timer >= timeBetweenShots * effectsDisplayTime)
        {
            StopEffects();
        }

        if (Input.GetButtonUp("Attack"))
        {
            StopShooting();
        }
    }

    private void Shoot()
    {
        timer = 0.0f;
        animator.SetBool("IsShooting", true);

        // Player can change weapons, so must be actively called for different weapons
        AudioSource gunAudio = gunSocket.GetComponentInChildren<AudioSource>();
        ParticleSystem muzzleFlash = gunSocket.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        Light muzzleLight = gunSocket.transform.GetChild(0).GetComponentInChildren<Light>();

        muzzleFlash.Play();
        muzzleLight.enabled = true;

        gunAudio.pitch = UnityEngine.Random.Range(.99f, 1.01f);
        gunAudio.Play();

        // Damage enemy that is currently locked on.
        GameObject enemy = lockOn.GetCurrentTarget();
        if (enemy)
        {
            equippedWeapon.Shoot(enemy);
        }
    }

    private void StopShooting()
    {
        Light muzzleLight = gunSocket.transform.GetChild(0).GetComponentInChildren<Light>();
        muzzleLight.enabled = false;
        animator.SetBool("IsShooting", false);
    }

    public void StopAttackingAnimations()
    {
        animator.SetBool("IsShooting", false);
    }

    private void StopEffects()
    {
        Light muzzleLight = gunSocket.transform.GetChild(0).GetComponentInChildren<Light>();
        muzzleLight.enabled = false;
    }
}
