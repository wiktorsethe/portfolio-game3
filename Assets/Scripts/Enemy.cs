using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    //Patroling
    [SerializeField] private Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    private bool alreadyAttacked;
    [SerializeField] private GameObject projectile;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange, isDead;

    [SerializeField] private HealthBar healthBar;
    private Animator animator;
    private Rigidbody rb;
    private float timer;
    private PlayerHealth playerHp;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        isDead = false;
        playerHp = GameObject.FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !isDead)
        {
            animator.SetTrigger("Sprint");
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange && !isDead)
        {
            animator.SetTrigger("Sprint");
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange && !isDead)
        {
            AttackPlayer();
        }

        timer += Time.deltaTime;
        if(timer > 8f && !isDead)
        {
            SearchWalkPoint();
            timer = 0f;
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        else if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        NavMeshHit hit;
        if(NavMesh.SamplePosition(walkPoint, out hit, 0.1f, NavMesh.AllAreas))
        {
            walkPointSet = true;
        }

        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            //walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.x = 0f;
        eulerRotation.z = 0f;
        transform.rotation = Quaternion.Euler(eulerRotation);

        if (timer >= timeBetweenAttacks)
        {
            animator.ResetTrigger("Sprint");
            int randInt = Random.Range(0, 2);
            if(randInt == 0) animator.SetTrigger("LeftPunch");
            else if(randInt == 1) animator.SetTrigger("RightPunch");
            playerHp.Damage(5f);
            timer = 0f;
        }
    }
    /*
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            animator.SetTrigger("BowShot");
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    */
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
            Invoke(nameof(DestroyEnemy), 4f);
        }
        else animator.SetTrigger("GetHit");
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
