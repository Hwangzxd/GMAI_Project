using Panda.Examples.PlayTag;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace FSM
{
    public class NPCRoamState : AIState
    {
        private bool isWaiting;

        public NPCRoamState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
            //isWaiting = false;
        }

        public override void Enter()
        {
            base.Enter();

            SetNewDestination();

            //Vector3 point;
            //if (npc.RandomPoint(npc.centrePoint.position, npc.range, out point)) //pass in our centre point and radius of area
            //{
            //    //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            //    npc.agent.SetDestination(point);
            //}
        }

        public override void Exit()
        {
            base.Exit();
            //isWaiting = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            //if (npc.agent.remainingDistance <= npc.agent.stoppingDistance) //done with path
            //{
            //    Vector3 point;
            //    if (npc.RandomPoint(npc.centrePoint.position, npc.range, out point)) //pass in our centre point and radius of area
            //    {
            //        //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            //        //npc.agent.SetDestination(point);

            //        aiStateMachine.ChangeState(npc.roaming);
            //    }
            //}

            // Check if the NPC has reached its destination
            if (npc.agent.remainingDistance <= npc.agent.stoppingDistance)
            {
                SetNewDestination();
            }

            //// Check if the NPC has reached its destination
            //if (!isWaiting && npc.agent.remainingDistance <= npc.agent.stoppingDistance)
            //{
            //    npc.StartCoroutine(WaitAndSetNewDestination(1.0f));
            //}

            if (npc.player != null)
            {
                if (npc.timePassed >= npc.attackCD && npc.player.GetComponent<RayWenderlich.Unity.StatePatternInUnity.Character>().isAlive)
                {
                    if (Vector3.Distance(npc.player.transform.position, npc.transform.position) <= npc.attackRange)
                    {
                        aiStateMachine.ChangeState(npc.attack);
                        npc.timePassed = 0;
                    }
                }
                npc.timePassed += Time.deltaTime;

                //Transition to chase state
                if (npc.newDestinationCD <= 0 && Vector3.Distance(npc.player.transform.position, npc.transform.position) <= npc.aggroRange)
                {
                    npc.newDestinationCD = 0.5f;
                    //npc.agent.SetDestination(npc.player.transform.position);
                    aiStateMachine.ChangeState(npc.chase);
                }
                npc.newDestinationCD -= Time.deltaTime;
            }

            return;
        }

        private void SetNewDestination()
        {
            Vector3 point;
            if (npc.RandomPoint(npc.centrePoint.position, npc.range, out point))
            {
                npc.agent.SetDestination(point);
                npc.newDestinationCD = 0.5f;  // Reset the cooldown timer for setting a new destination
            }
        }

        //private IEnumerator WaitAndSetNewDestination(float waitTime)
        //{
        //    isWaiting = true;
        //    yield return new WaitForSeconds(waitTime);
        //    SetNewDestination();
        //    isWaiting = false;
        //}
    }
}
