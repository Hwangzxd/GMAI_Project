using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class CreatureTasks : MonoBehaviour
{
    private Rigidbody rb;
    private Creature creature;
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    AIVision vision;
    float timePassed;
    float newDestinationCD = 0.5f;
    public float range;
    public Transform centrePoint;

    [HideInInspector]
    public Vector3 destination; // The movement destination
    public Vector3 target;      // The position to aim to

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        creature = GetComponent<Creature>();
        animator = GetComponent<Animator>();
        vision = GetComponentInChildren<AIVision>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        if (player != null)
        {
            return;
        }
    }

    [Task]
    bool SetDestination_Player()
    {
        bool succeeded = false;

        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, agent.transform.position) <= creature.aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
            succeeded = true;
        }

        newDestinationCD -= Time.deltaTime;
        return succeeded;
    }

    [Task]
    bool InAggroRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= creature.aggroRange)
        {
            //Debug.Log("Player is in aggro range");
            return true;
        }

        return false;
    }

    [Task]
    bool InAttackRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= creature.attackRange)
        {
            //Debug.Log("Player is in attack range");
            return true;
        }

        return false;
    }

    [Task]
    bool Attack()
    {
        bool succeeded = false;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
            timePassed = 0;

            succeeded = true;
        }

        timePassed += Time.deltaTime;
        return succeeded;
    }

    [Task]
    public void TakeDamage(float damageAmount)
    {
        creature.health -= damageAmount;
        agent.isStopped = true;

        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }

        if (creature.health <= 0)
        {
            Die();
        }
    }

    [Task]
    public bool IsHealthLessThan(float health)
    {
        return creature.health < health;
    }

    [Task]
    bool Die()
    {
        agent.isStopped = true;
        animator.SetTrigger("Dead");
        Debug.Log("Creature died.");
        return true;
    }

    // Task to check if a player is visible
    [Task]
    bool IsVisible_Player()
    {
        // Iterate through the list of visible objects
        foreach (var v in vision.visibles)
        {
            // Check if the tag of the visible object is "Player"
            if (v != null && v.CompareTag("Player"))
            {
                // If a player is found, return true
                return true;
            }
        }

        // If no player is found, return false
        return false;
    }

    [Task]
    bool IsDestinationReached()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    [Task]
    bool FindRandomPoint()
    {
        Vector3 point;

        if (RandomPoint(centrePoint.position, range, out point))
        {
            agent.SetDestination(point);
            return true;
        }

        return false;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void StopAgent()
    {
        agent.isStopped = true;
        Debug.Log("Agent stopped.");
    }

    // Method to start the agent (called by animation event)
    public void StartAgent()
    {
        agent.isStopped = false;
        Debug.Log("Agent started.");
    }

    public void DestroyAgent()
    {
        Destroy(this.gameObject);
    }

}