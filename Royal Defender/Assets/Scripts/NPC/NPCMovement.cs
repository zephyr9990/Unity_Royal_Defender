using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public GameObject baseLocation;

    // components
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO set logic if armed/unarmed
        navMeshAgent.SetDestination(baseLocation.transform.position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        animator.SetFloat("VelocityX", navMeshAgent.velocity.x);
        animator.SetFloat("VelocityZ", navMeshAgent.velocity.z);
    }
}
