using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public GameObject aimingCube;
    private Animator animator;
    private LockOnScript lockOn;

    private const float flyingEnemyRootLocation = 6.0f;
    private const float groundedEnemyRootLocation = 2f;
    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        lockOn = transform.GetComponentInChildren<LockOnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isLockedOn = animator.GetBool("LockOnToggled");
        GameObject target = lockOn.GetCurrentTarget();
        if (isLockedOn && target)
        {
            AimTowards(target);
        }
        else
        {
            ResetAim();
        }
    }

    private void ResetAim()
    {
        animator.SetFloat("AimDirectionX", 0);
    }

    private void AimTowards(GameObject target)
    {
        if (target)
        {
            Vector3 rootLocation = GetRootLocation(target);
            Vector3 toTarget = (target.transform.position + rootLocation) - aimingCube.transform.position;
            aimingCube.transform.rotation = Quaternion.LookRotation(toTarget);
            Quaternion aimDirection = aimingCube.transform.rotation.normalized;
            animator.SetFloat("AimDirectionX", aimDirection.x);
        }
    }

    private void AimTowards(Vector3 toTarget)
    {
        aimingCube.transform.rotation = Quaternion.LookRotation(toTarget);
        Quaternion aimDirection = aimingCube.transform.rotation.normalized;
        animator.SetFloat("AimDirectionX", aimDirection.x);        
    }

    private static Vector3 GetRootLocation(GameObject target)
    {
        if (target.GetComponent<EnemyController>() != null)
        { return new Vector3(0, groundedEnemyRootLocation, 0); }
        else
        { return new Vector3(0, flyingEnemyRootLocation, 0); }
    }
}
