using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemyController : MonoBehaviour, IAIController
{
    private GameObject player; //The Player object
    private GameObject cube; // the goal 
    private GameObject NPC; // The NPC 
    public float SD = 15; // Initially set to 5 needs to be adjusted to find right ditance and then set private
    private NavMeshAgent _nav;
    private GameObject target;

    private float PlayerRange = 30;
    private float NPCRange = 20;
    private bool movementStopped;
    private Animator _anim;

    public GameObject[] Weapons;

    // Start is called before the first frame update
    void Start()
    {
        //setting up the navAgent
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        movementStopped = false;

        FindTargets();
        _nav.stoppingDistance = SD;
    }

// Update is called once per frame
    void Update()
    {
        if (movementStopped)
            return;
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
        // Check if NPC is alive
        NPCHealth npcHealth = NPC.GetComponent<NPCHealth>();
        bool npcIsDown = true;
        if (npcHealth)
        {
            npcIsDown = npcHealth.GetIsDown();
        }

        if (Vector3.Distance(this.transform.position, player.transform.position) < PlayerRange)
        { 
            target = player;
        }
        else if (!npcIsDown && (Vector3.Distance(this.transform.position, NPC.transform.position) < NPCRange))
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
            _anim.SetBool("ShootFireball", false);
            _anim.SetBool("FlyingForward", true);
        }
        else
        {
            _anim.SetBool("FlyingForward", false);
            _nav.isStopped = true;

            //ATTACKTARGET 
            _anim.SetBool("ShootFireball", true);

        }

    }

    public void DropWeapon()
    {
        int randInt = UnityEngine.Random.Range(0, Weapons.Length);
        //Instantiate(Weapons[randInt], transform.position);
        Instantiate(Weapons[randInt], transform.position, Quaternion.identity);
    }

    public void StopMovement()
    {
        movementStopped = true;
        _nav.speed = 0f;
        _nav.enabled = false;
        //enabled = false;
    }

    public void EnableMovement()
    {
        movementStopped = false;
        _nav.speed = 3.5f;
        _nav.enabled = true;
    }
}