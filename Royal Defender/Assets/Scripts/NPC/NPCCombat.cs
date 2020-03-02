using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCombat : MonoBehaviour
{
    public float range = 100.0f;
    public int numberOfShotsPerBurst = 15;
    public float timeBetweenBursts = 3f;
    public GameObject rangedSocket;

    private float timer = 0;
    private bool InMeleeRange;
    private bool InShootingRange;
    private bool isCoolingDownFromBurst;

    private int numberOfShotsFired;

    private Animator animator;
    private NPCEquippedWeapon npcEquippedWeapon;

    // Start is called before the first frame update
    private void Awake()
    {
        numberOfShotsFired = 0;
        InMeleeRange = false;
        InShootingRange = false;
        isCoolingDownFromBurst = false;
        animator = GetComponentInChildren<Animator>();
        npcEquippedWeapon = GetComponent<NPCEquippedWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        WeaponInfo currentWeapon = npcEquippedWeapon.GetWeaponInfo();

        if (currentWeapon == null)
        {
            StopAttackingAnimations();
            return;
        }

        if (currentWeapon.type == WeaponType.Ranged)
        {
            CheckIfNPCWantsToShoot(currentWeapon);
        }
        else // player has melee weapon
        {
            CheckIfNPCWantsToSwing(currentWeapon);
        }


    }

    private void CheckIfNPCWantsToSwing(WeaponInfo weapon)
    {
        if (InMeleeRange)
        {
            bool isSprinting = animator.GetBool("IsSprinting");
            if (isSprinting)
                return; // cannot swing weapon if sprinting;

            if (timer > weapon.npcAttackDelay)
            {
                timer = 0f;
                animator.SetBool("IsSwinging", false);
                animator.SetBool("IsSwinging", true);
            }
        }
    }

    private void CheckIfNPCWantsToShoot(WeaponInfo weapon)
    {
        if (InShootingRange)
        {
            if (timer > weapon.npcAttackDelay
                && animator.GetBool("LockOnToggled"))
            {
                if (!IsOnCooldownFromBurst())
                {
                    Shoot();
                }
            }
            else
            {
                //TODO check if this is truly needed.
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
        animator.SetBool("IsShooting", true);

        // Player can change weapons, so must be actively called for different weapons
        IRangedWeapon rangedWeapon = rangedSocket.transform.GetChild(0).GetComponent<IRangedWeapon>();
        rangedWeapon.Fire(animator);

        // Damage enemy that is currently locked on.
        GameObject enemy = transform.GetChild(0).GetComponent<NPCAttackRange>().GetNearestTarget();
        if (enemy)
        {
            npcEquippedWeapon.Shoot(enemy);
        }

        if (++numberOfShotsFired > numberOfShotsPerBurst)
        {
            isCoolingDownFromBurst = true;
            return;
        }
        isCoolingDownFromBurst = false;
    }

    private bool IsOnCooldownFromBurst()
    {
        if (timer > timeBetweenBursts)
        {
            isCoolingDownFromBurst = false;
            numberOfShotsFired = 0;
        }
        return isCoolingDownFromBurst;
    }

    private void StopShooting()
    {
        animator.SetBool("IsShooting", false);
    }

    public void StopAttackingAnimations()
    {
        animator.SetBool("IsShooting", false);
    }

    public void SetInMeleeRange(bool value)
    {
        InMeleeRange = value;
    }

    public void SetInShootingRange(bool value)
    {
        InShootingRange = value;
    }
}

