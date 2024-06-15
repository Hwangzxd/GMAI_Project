using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

namespace FSM
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class NPC : MonoBehaviour
    {
        public AIStateMachine aiMovementSM;
        public NPCIdleState idle;
        public NPCHitState hit;
        public NPCDeathState death;
        public NPCAttackState attack;

        [SerializeField] public float health = 3;

        [Header("Combat")]
        [SerializeField] public float attackCD = 3f;
        [SerializeField] public float attackRange = 1f;
        [SerializeField] public float aggroRange = 4f;

        public int hitParam => Animator.StringToHash("Damage");
        public int deathParam => Animator.StringToHash("Dead");
        public int attackParam => Animator.StringToHash("Attack");

        public bool isAlive = true;

        public GameObject player;
        NavMeshAgent agent;
        public Animator animator;
        public float timePassed;
        float newDestinationCD = 0.5f;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");

            aiMovementSM = new AIStateMachine();

            idle = new NPCIdleState(this, aiMovementSM);
            hit = new NPCHitState(this, aiMovementSM);
            death = new NPCDeathState(this, aiMovementSM);
            attack = new NPCAttackState(this, aiMovementSM);

            aiMovementSM.Initialize(idle);
        }

        void Update()
        {
            aiMovementSM.CurrentState.HandleInput();

            aiMovementSM.CurrentState.LogicUpdate();

            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

            if (player == null)
            {
                return;
            }

            if (timePassed >= attackCD && player.GetComponent<RayWenderlich.Unity.StatePatternInUnity.Character>().isAlive)
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

            if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            {
                transform.LookAt(player.transform);
            }
        }

        private void FixedUpdate()
        {
            aiMovementSM.CurrentState.PhysicsUpdate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player = collision.gameObject;
            }
        }

        void Berserk()
        {
            animator.SetTrigger("LowHealth");
        }

        void Die()
        {
            agent.isStopped = true;
            animator.SetTrigger("Dead");

            

            //Instantiate(ragdoll, transform.position, transform.rotation);
            //Destroy(this.gameObject);
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            animator.SetTrigger("Damage");

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

        public void SetAnimationBool(int param, bool value)
        {
            animator.SetBool(param, value);
        }

        public void TriggerAnimation(int param)
        {
            animator.SetTrigger(param);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}