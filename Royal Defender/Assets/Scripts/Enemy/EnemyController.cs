using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IAIController
{
    private GameObject player; //The Player object
    private GameObject cube; // the goal 
    private GameObject NPC; // The NPC 
    private NavMeshAgent _nav;
    private GameObject target;
    private float PlayerRange = 30;
    private float NPCRange = 20;
    private Animator _anim;
    private AudioSource _audio;
    private bool movementStopped;

    public float SD = 5; // Initially set to 5 needs to be adjusted to find right ditance and then set private
    public int attackDamage = 10;
    public float attackInterval = 4;
    public GameObject[] Weapons;

    // Start is called before the first frame update
    void Start()
    {
        //setting up the navAgent
        _nav = GetComponent < NavMeshAgent >();
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        FindTargets();
        _nav.stoppingDistance = SD;
        attackInterval = 0;
        movementStopped = false;
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
        if (Vector3.Distance(this.transform.position, target.transform.position) > SD && this.GetComponent<EnemyHealth>().isAlive())
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

            AttackTarget(); 
            
            
        }

    }

    public void AttackTarget()
    {
        // Removed the atteackInterval <= 0 from the if statement
        if (target.CompareTag("Player") && this.GetComponent<EnemyHealth>().isAlive()&& attackInterval <= 0)
        {
            _anim.SetTrigger("Attack Trigger");
            _anim.SetBool("Basic Attack", true);
            target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            _audio.Play();
            Debug.Log("Attacking Player");
            attackInterval = 4;
        }
        else
        {
            _anim.SetTrigger("Attack Trigger");
           _anim.SetBool("Basic Attack", false);
            attackInterval -= Time.deltaTime;
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
    }

    public void EnableMovement()
    {
        _nav.speed = 3.5f;
        _nav.enabled = true;
        movementStopped = false;
    }
}
