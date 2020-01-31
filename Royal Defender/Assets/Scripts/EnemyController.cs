using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject player; //The Player object
    private GameObject cube; // the goal 
    private GameObject NPC; // The NPC 
    public float SD = 5; // Initially set to 5 needs to be adjusted to find right ditance and then set private
    private NavMeshAgent _nav;
    private GameObject target;

    private float PlayerRange = 30;
    private float NPCRange = 20;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        //setting up the navAgent
        _nav = GetComponent < NavMeshAgent >();
        _anim = GetComponent<Animator>();
        
        FindTargets();
        _nav.stoppingDistance = SD;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log(enemyTargets.Count);

        SelectTarget();
        GoToTarget();
        //Debug.Log(Vector3.Distance(this.transform.position, target.transform.position));
        //Debug.Log(target);

    }

    void FixedUpdate()
    {
        
    }
   
    void FindTargets()
    {
        //Finding all availible targets
        player = GameObject.FindGameObjectWithTag("Player");
        NPC = GameObject.FindGameObjectWithTag("NPC");
        cube = GameObject.FindGameObjectWithTag("Cube");
        
    }

    void SelectTarget()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < PlayerRange)
        {
            target = player;
        }
        else if ((Vector3.Distance(this.transform.position, NPC.transform.position) < NPCRange))
        {
            target = NPC;
        }
        else
        {
            target = cube;

        }
    }
       
    
    void GoToTarget() 
    { 
        //checks to see if target is within range
        if (Vector3.Distance(this.transform.position, target.transform.position) > SD)
        {
            _nav.isStopped = false;
            _nav.SetDestination(target.transform.position);
            _anim.SetBool("Basic Attack", false);
            _anim.SetBool("Walk", true);
        }
        else
        {
            _anim.SetBool("Walk", false);
            _nav.isStopped= true;

            //ATTACKTARGET 
            _anim.SetBool("Basic Attack", true);
            
        }

    }

    

}
