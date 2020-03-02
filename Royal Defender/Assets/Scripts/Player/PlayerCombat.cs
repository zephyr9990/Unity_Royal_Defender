using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float range = 100.0f;
    public GameObject gunSocket;

    private float timer = 0;

    private Animator animator;
    private PlayerEquippedWeapon equippedWeapon;
    private LockOnScript lockOn;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            CheckIfPlayerWantsToShoot(currentWeapon);
        }
        else // player has melee weapon
        {
            CheckIfPlayerWantsToSwing(currentWeapon);
        }
    }

    private void CheckIfPlayerWantsToSwing(WeaponInfo weapon)
    {
        if (Input.GetButtonDown("Attack"))
        {
            bool isSprinting = animator.GetBool("IsSprinting");
            if (isSprinting)
                return; // cannot swing weapon if sprinting;

            if (timer > weapon.attackDelay)
            {
                timer = 0f;
                animator.SetBool("IsSwinging", false);
                animator.SetBool("IsSwinging", true);
            }
        }
    }

    private void CheckIfPlayerWantsToShoot(WeaponInfo weapon)
    {
        if (Input.GetButton("Attack"))
        {
            if (timer > weapon.attackDelay
                && animator.GetBool("LockOnToggled"))
            {
                Shoot();
            }
            else
            {
                // TODO make sure this is truly needed.
                StopAttackingAnimations();
            }
        }

        if (Input.GetButtonUp("Attack"))
        {
            StopShooting();
        }
    }

    private void Shoot()
    {
        timer = 0.0f;
        
        // Player can change weapons, so must be actively called for different weapons
        IRangedWeapon rangedWeapon = gunSocket.transform.GetChild(0).GetComponent<IRangedWeapon>();
        rangedWeapon.Fire(animator);

        if (rangedWeapon is SpecialWeapon)
            return;  // special weapons deal damage at end of cutscene.

        // Damage enemy that is currently locked on.
        GameObject enemy = lockOn.GetCurrentTarget();
        if (enemy)
        {
            equippedWeapon.Shoot(enemy);
        }
    }

    private void StopShooting()
    {
        animator.SetBool("IsShooting", false);
    }

    public void StopAttackingAnimations()
    {
        animator.SetBool("IsShooting", false);
    }
}
