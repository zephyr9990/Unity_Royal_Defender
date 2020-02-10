using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public GameObject baseLocation;
    public GameObject locationToGuard;
    public SphereCollider attackRangeSphere;
    public float meleeDistance = 3;
    public float lerpSmoothing = 7f;
    public float baseStoppingDistance = 5;
    public float safeShootingDistance = 15;
    public float movementSpeed = 3.5f;
    public float aimingMovementSpeed = 2.0f;

    private GameObject enemy;

    // components
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private NPCEquippedWeapon npcEquippedWeapon;
    private NPCAttackRange attackRange;
    private NPCCombat npcCombat;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        animator = GetComponent<Animator>();
        npcEquippedWeapon = GetComponent<NPCEquippedWeapon>();
        attackRange = transform.GetChild(0).GetComponent<NPCAttackRange>();
        npcCombat = GetComponent<NPCCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        bool npcHasWeapon = IsArmed();

        if (npcHasWeapon)
        {
            enemy = attackRange.GetNearestTarget();
            if (enemy == null)
            {
                GoCombatIdle();
                GoToLocation(locationToGuard);
            }
            else
            {
                Attack(enemy);
            }
        }
        else // NPC has no weapon. Go back to base.
        {
            SetIsAiming(false);
            GoToLocation(baseLocation);
            StopWithinDistance(baseLocation, baseStoppingDistance);
        }

        if (IsAiming())
        { navMeshAgent.speed = aimingMovementSpeed; }
        else
        { navMeshAgent.speed = movementSpeed; }
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        animator.SetFloat("VelocityX", navMeshAgent.velocity.x);
        animator.SetFloat("VelocityZ", navMeshAgent.velocity.z);
    }

    public void GoCombatIdle()
    {
        SetIsAiming(false);
        npcCombat.SetInMeleeRange(false);
        npcCombat.SetInShootingRange(false);
    }

    private bool IsArmed()
    {
        WeaponInfo weapon = npcEquippedWeapon.GetWeaponInfo();
        return weapon != null;
    }

    private void StopWithinDistance(GameObject targetPoint, float acceptableDistance)
    {
        Vector3 distanceToTarget = targetPoint.transform.position - transform.position;

        if (distanceToTarget.magnitude <= acceptableDistance)
        {
            setNavStopped(true);
        }
    }

    private void GoToLocation(GameObject targetPoint)
    {
        setNavStopped(false);
        navMeshAgent.SetDestination(targetPoint.transform.position);
    }
    private void GoToLocation(Vector3 targetPosition)
    {
        setNavStopped(false);
        navMeshAgent.SetDestination(targetPosition);
    }

    private void Attack(GameObject target)
    {
        WeaponInfo weapon = npcEquippedWeapon.GetWeaponInfo();
        if (weapon.type == WeaponType.Melee)
        {
            GoToMeleeRange(target);
        }
        else
        {
            GoToShootingRange(target);
        }
    }

    private void GoToMeleeRange(GameObject target)
    {
        SetIsAiming(false);
        Vector3 distanceToEnemy = target.transform.position - transform.position;
        LockOnto(target);
        if (distanceToEnemy.magnitude > meleeDistance)
        {
            GoToLocation(target);
        }
        else
        {
            npcCombat.SetInMeleeRange(true);
            setNavStopped(true);
        }
    }

    private void GoToShootingRange(GameObject target)
    {
        SetIsAiming(true);
        Vector3 distanceToEnemy = target.transform.position - transform.position;
        LockOnto(target);
        if (distanceToEnemy.magnitude > attackRangeSphere.radius)
        {
            npcCombat.SetInShootingRange(false);
            GoToLocation(target);
        }
        else if (distanceToEnemy.magnitude < safeShootingDistance)
        {
            npcCombat.SetInShootingRange(true);
            Vector3 enemyToSelf = transform.position - target.transform.position;
            Vector3 awayFromEnemy = transform.position + enemyToSelf;
            GoToLocation(awayFromEnemy);
        }
        else
        {
            npcCombat.SetInShootingRange(true);
            setNavStopped(true);
        }
    }

    private void setNavStopped(bool value)
    {
        navMeshAgent.isStopped = value;
    }

    private void LockOnto(GameObject target)
    {
        if (target)
        {
            EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
            if (targetHealth.isAlive())
            {
                Vector3 toTarget = target.transform.position - transform.position;
                toTarget.y = 0;
                Vector3 toTargetRotation = Vector3.RotateTowards(transform.forward, toTarget, Time.deltaTime * lerpSmoothing, 0.0f);
                transform.rotation = Quaternion.LookRotation(toTargetRotation);
            }
        }
    }

    private bool IsAiming()
    {
        return animator.GetBool("LockOnToggled");
    }

    private void SetIsAiming(bool value)
    {
        animator.SetBool("LockOnToggled", value);
    }

    public GameObject getTarget()
    {
        if (enemy)
        { return enemy; }

        return null;
    }
}
