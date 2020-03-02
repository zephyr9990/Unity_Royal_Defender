using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnScript : MonoBehaviour
{
    public float lerpSmoothing = 5.0f;

    private ArrayList enemies;
    private Animator animator;
    private bool lockOnToggled = false;
    private PlayerEquippedWeapon equippedWeapon;
    private GameObject currentlyLockedOnTarget;    

    private void Awake()
    {
        enemies = new ArrayList();
        animator = transform.root.GetComponent<Animator>();
        equippedWeapon = transform.parent.GetComponent<PlayerEquippedWeapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Player cannot lockon if there's no weapon equipped.
        if (equippedWeapon.GetWeaponInfo() == null
            || equippedWeapon.GetWeaponInfo().type != WeaponType.Ranged)
        {
            TurnOffLockOn();
            return;
        }

        // Toggle lock on when player presses the lock-on button
        if (Input.GetButtonDown("LockOnToggle") 
            && !animator.GetBool("IsSprinting")
            && PlayerHasRangedWeapon())
        {
            lockOnToggled = !lockOnToggled;
            animator.SetBool("LockOnToggled", lockOnToggled);
            if (!lockOnToggled)
            { TurnOffLockOn(); }
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
                else
                {
                    animator.SetBool("LockOnToggled", false);
                }
            }
        }
    }

    private bool PlayerHasRangedWeapon()
    {
        if (equippedWeapon.GetWeaponInfo() == null)
            return false;

        WeaponType equippedWeaponType = equippedWeapon.GetWeaponInfo().type;
        return equippedWeaponType == WeaponType.Ranged;
    }

    private GameObject GetNearestEnemy()
    {
        GameObject closestEnemy = null;
        Vector3 toClosestEnemy = Vector3.zero;
        for (int index = 0; index < enemies.Count; index++)
        {
            GameObject enemy = enemies[index] as GameObject;
            if (enemy)
            {
                SetHealthBarEnabled(enemy, false);
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth.isAlive())
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
                else
                {
                    RemoveFromLockOnList(enemy);
                }
            }
        }

        return closestEnemy;
    }

    private void LockOnto(GameObject target)
    {
        // Make the player always face the target so that they do not have to aim
        if (target)
        {
            EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
            if (targetHealth.isAlive())
            {
                Vector3 toTarget = target.transform.position - transform.parent.position;
                toTarget.y = 0;
                Vector3 toTargetRotation = Vector3.RotateTowards(transform.parent.forward, toTarget, Time.deltaTime * lerpSmoothing, 0.0f);
                transform.parent.rotation = Quaternion.LookRotation(toTargetRotation);

                currentlyLockedOnTarget = target;
                SetHealthBarEnabled(currentlyLockedOnTarget, true);
            }
        }
    }

    public GameObject GetCurrentTarget()
    {
        return currentlyLockedOnTarget;
    }

    public bool GetLockOnToggled()
    {
        return lockOnToggled;
    }

    public void TurnOffLockOn()
    {
        lockOnToggled = false;
        animator.SetBool("LockOnToggled", false);
        animator.SetBool("IsShooting", false);

        if (currentlyLockedOnTarget)
        {
            SetHealthBarEnabled(currentlyLockedOnTarget, false);
        }
        currentlyLockedOnTarget = null;

    }

    private void SetHealthBarEnabled(GameObject target, bool value)
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        enemyHealth.isLocked = value;
    }

    public void RemoveFromLockOnList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public ArrayList GetEnemiesInRange()
    {
        return enemies;
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
            RemoveFromLockOnList(other.gameObject);
        }
    }
}
