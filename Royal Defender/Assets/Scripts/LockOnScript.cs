using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnScript : MonoBehaviour
{
    public float lerpSmoothing = 5.0f;

    private PlayerInventory playerInventory;
    private ArrayList enemies;
    private bool isLockedOn;
    private Animator animator;
    private bool lockOnToggled = false;

    private void Awake()
    {
        playerInventory = transform.parent.GetComponent<PlayerInventory>();
        enemies = new ArrayList();
        isLockedOn = false;
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LockOnToggle") 
            && !animator.GetBool("IsSprinting")
            && PlayerHasRangedWeapon())
        {
            lockOnToggled = !lockOnToggled;
            animator.SetBool("LockOnToggled", lockOnToggled);
        }

        if (enemies.Count > 0 && PlayerHasRangedWeapon())
        {
            GameObject closestEnemy = GetNearestEnemy();
            // can only lock on if the player is not sprinting.
            if (!animator.GetBool("IsSprinting"))
            {
                if (lockOnToggled)
                {
                    LockOnto(closestEnemy);
                }
            }
        }
        else
        {
            isLockedOn = false;
        }
    }

    private bool PlayerHasRangedWeapon()
    {
        if (playerInventory.getEquippedWeaponInfo() != null)
            return playerInventory.getEquippedWeaponInfo().weaponType == WeaponType.Ranged;
        return false;
    }

    private GameObject GetNearestEnemy()
    {
        GameObject closestEnemy = null;
        Vector3 toClosestEnemy = Vector3.zero;
        foreach (GameObject enemy in enemies)
        {
            Vector3 ToEnemy = enemy.transform.position - transform.parent.position;
            if (!closestEnemy)
            {
                closestEnemy = enemy;
                toClosestEnemy = ToEnemy;
            }
            else
            {
                if (ToEnemy.magnitude < toClosestEnemy.magnitude)
                {
                    closestEnemy = enemy;
                    toClosestEnemy = ToEnemy;
                }
            }
        }

        return closestEnemy;
    }

    private void LockOnto(GameObject target)
    {
        // Make the player always face the target so that they do not have to aim
        isLockedOn = true;
        Vector3 toTarget = target.transform.position - transform.parent.position;
        toTarget.y = 0;
        Vector3 toTargetRotation = Vector3.RotateTowards(transform.parent.forward, toTarget, Time.deltaTime * lerpSmoothing, 0.0f);

        transform.parent.rotation = Quaternion.LookRotation(toTargetRotation);
    }

    public bool GetIsLockedOn()
    {
        return isLockedOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
