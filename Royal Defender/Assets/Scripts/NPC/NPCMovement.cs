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
            GameObject enemyToAttack = attackRange.GetNearestTarget();
            if (enemyToAttack == null)
            {
                GoCombatIdle();
                GoToLocation(locationToGuard);
            }
            else
            {
                Attack(enemyToAttack);
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

    private void Attack(GameObject enemy)
    {
        WeaponInfo weapon = npcEquippedWeapon.GetWeaponInfo();
        if (weapon.type == WeaponType.Melee)
        {
            GoToMeleeRange(enemy);
        }
        else
        {
            GoToShootingRange(enemy);
        }
    }

    private void GoToMeleeRange(GameObject enemy)
    {
        SetIsAiming(false);
        Vector3 distanceToEnemy = enemy.transform.position - transform.position;
        LockOnto(enemy);
        if (distanceToEnemy.magnitude > meleeDistance)
        {
            GoToLocation(enemy);
        }
        else
        {
            npcCombat.SetInMeleeRange(true);
        }
    }

    private void GoToShootingRange(GameObject enemy)
    {
        SetIsAiming(true);
        Vector3 distanceToEnemy = enemy.transform.position - transform.position;
        LockOnto(enemy);
        if (distanceToEnemy.magnitude > attackRangeSphere.radius)
        {
            npcCombat.SetInShootingRange(false);
            GoToLocation(enemy);
        }
        else if (distanceToEnemy.magnitude < safeShootingDistance)
        {
            npcCombat.SetInShootingRange(true);
            Vector3 enemyToSelf = transform.position - enemy.transform.position;
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
}
