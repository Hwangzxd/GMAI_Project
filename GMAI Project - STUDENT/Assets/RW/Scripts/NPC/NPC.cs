using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(CapsuleCollider))]
public class NPC : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        if (player == null)
        {
            return;
        }

        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //print(true);
            player = collision.gameObject;
        }
    }

    void Berserk()
    {
        animator.SetTrigger("LowHealth");
    }

    void Die()
    {
        animator.SetTrigger("Dead");
  
        //Instantiate(ragdoll, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("Damage");
        Debug.Log("NPC took damage");
        //CameraShake.Instance.ShakeCamera(2f, 0.2f);

        if (health < 2)
        {
            Berserk();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<NPCDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<NPCDamageDealer>().EndDealDamage();
    }

    //public void HitVFX(Vector3 hitPosition)
    //{
    //    GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
    //    Destroy(hit, 3f);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
