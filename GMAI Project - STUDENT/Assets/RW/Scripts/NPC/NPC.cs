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
        public NPCRoamState roaming;
        public NPCAttackState attack;
        public NPCChaseState chase;

        [SerializeField] public float health = 3;

        [Header("Combat")]
        [SerializeField] public float attackCD = 3f;
        [SerializeField] public float attackRange = 1f;
        [SerializeField] public float aggroRange = 4f;

        public int hitParam => Animator.StringToHash("Damage");
        public int deathParam => Animator.StringToHash("Dead");
        public int attackParam => Animator.StringToHash("Attack");
        public int battlecryParam => Animator.StringToHash("LowHealth");

        public bool isAlive = true;

        public GameObject player;
        public NavMeshAgent agent;
        public Animator animator;
        public float timePassed;
        public float newDestinationCD = 0.5f;

        public float range; //radius of sphere

        public Transform centrePoint; //centre of the area the agent wants to move around in

        //Define an event to notify when the NPC is hit
        //public event System.Action OnHit;

        //public event System.Action OnDeath;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");

            aiMovementSM = new AIStateMachine();

            roaming = new NPCRoamState(this, aiMovementSM);
            attack = new NPCAttackState(this, aiMovementSM);
            chase = new NPCChaseState(this, aiMovementSM);

            aiMovementSM.Initialize(roaming);
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

            //if (timePassed >= attackCD && player.GetComponent<RayWenderlich.Unity.StatePatternInUnity.Character>().isAlive)
            //{
            //    if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            //    {
            //        animator.SetTrigger("Attack");
            //        timePassed = 0;
            //    }
            //}
            //timePassed += Time.deltaTime;

            //if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            //{
            //    newDestinationCD = 0.5f;
            //    agent.SetDestination(player.transform.position);
            //}
            //newDestinationCD -= Time.deltaTime;

            //if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            //{
            //    transform.LookAt(player.transform);
            //}
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

        public void Battlecry()
        {
            agent.isStopped = true;
            animator.SetTrigger("LowHealth");
        }

        public void Die()
        {
            agent.isStopped = true;
            animator.SetTrigger("Dead");

            //OnDeath?.Invoke();

            //Instantiate(ragdoll, transform.position, transform.rotation);
            //Destroy(this.gameObject);
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            animator.SetTrigger("Damage");
            agent.isStopped = true;

            //Notify subscribers that the player was hit
            //OnHit?.Invoke();

            if (health < 2)
            {
                Battlecry();
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

        public void StopAgent()
        {
            agent.isStopped = true;
            Debug.Log("Agent stopped.");
        }

        //Method to start the agent (called by animation event)
        public void StartAgent()
        {
            agent.isStopped = false;
            Debug.Log("Agent started.");
        }

        // Code taken from: https://youtu.be/dYs0WRzzoRc?si=trA4glk6j-zH6tzZ
        public bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            {
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
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