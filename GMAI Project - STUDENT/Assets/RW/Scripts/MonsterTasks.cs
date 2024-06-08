using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class MonsterTasks : MonoBehaviour
{
    private Rigidbody rb;
    private Monster monster;
    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;

    [HideInInspector]
    public Vector3 destination; // The movement destination
    public Vector3 target;      // The position to aim to

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            return;
        }
    }

    [Task]
    void SetDestination_Player()
    {
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, agent.transform.position) <= monster.aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }

        newDestinationCD -= Time.deltaTime;
    }

    [Task]
    void Attack()
    {
        if (animator != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= monster.attackRange)
            {
                animator.SetTrigger("Attack");
                timePassed = 0;
            }
        }

        timePassed += Time.deltaTime;
    }

    [Task]
    void TakeDamage(float damageAmount)
    {
        monster.health -= damageAmount;
        animator.SetTrigger("Damage");
        //CameraShake.Instance.ShakeCamera(2f, 0.2f);

        if (monster.health <= 0)
        {
            Die();
        }
    }

    [Task]
    void Die()
    {
        //Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    //// Task to set the destination to the player's position
    //[Task]
    //bool SetDestination_Enemy()
    //{
    //    bool succeeded = false;

    //    if (player != null)
    //    {
    //        agent.SetDestination(player.transform.position);
    //        succeeded = true;
    //    }
    //    return succeeded;
    //}

    //[Task]
    //public bool SetDestination(Vector3 p)
    //{
    //    destination = p;
    //    agent.destination = destination;

    //    if (Task.isInspected)
    //        Task.current.debugInfo = string.Format("({0}, {1})", destination.x, destination.y);
    //    return true;
    //}

    //[Task]
    //public void WaitArrival()
    //{
    //    var task = Task.current;
    //    float d = agent.remainingDistance;
    //    if (!task.isStarting && agent.remainingDistance <= 1e-2)
    //    {
    //        task.Succeed();
    //        d = 0.0f;
    //    }

    //    if (Task.isInspected)
    //        task.debugInfo = string.Format("d-{0:0.00}", d);
    //}

    //[Task]
    //public void MoveTo(Vector3 dst)
    //{
    //    SetDestination(dst);
    //    if (Task.current.isStarting)
    //        agent.isStopped = false;
    //    WaitArrival();
    //}

    //[Task]
    //public void MoveTo_Destination()
    //{
    //    MoveTo(destination);
    //    WaitArrival();
    //}
}
