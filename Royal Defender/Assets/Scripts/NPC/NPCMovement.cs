using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public GameObject baseLocation;
    public SphereCollider attackRangeSphere;

    // components
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private NPCEquippedWeapon npcEquippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        npcEquippedWeapon = GetComponent<NPCEquippedWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponInfo npcWeapon = npcEquippedWeapon.GetWeaponInfo();

        // TODO set logic if armed/unarmed
        if (!(npcWeapon == null))
        {
        }

        navMeshAgent.SetDestination(baseLocation.transform.position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        animator.SetFloat("VelocityX", navMeshAgent.velocity.x);
        animator.SetFloat("VelocityZ", navMeshAgent.velocity.z);
    }
}
