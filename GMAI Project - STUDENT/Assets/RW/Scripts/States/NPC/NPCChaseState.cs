using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class NPCChaseState : AIState
    {
        public NPCChaseState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
             
            npc.agent.SetDestination(npc.player.transform.position);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (npc.player != null)
            {
                if (npc.newDestinationCD >= 0 && Vector3.Distance(npc.player.transform.position, npc.transform.position) >= npc.aggroRange)
                {
                    aiStateMachine.ChangeState(npc.roaming);
                    npc.timePassed = 0;
                }
                npc.timePassed += Time.deltaTime;

                if (npc.newDestinationCD <= 0 && Vector3.Distance(npc.player.transform.position, npc.transform.position) <= npc.aggroRange)
                {
                    npc.newDestinationCD = 0.5f;
                    npc.agent.SetDestination(npc.player.transform.position);
                    aiStateMachine.ChangeState(npc.chase);
                }
                npc.newDestinationCD -= Time.deltaTime;

                if (npc.timePassed >= npc.attackCD && npc.player.GetComponent<RayWenderlich.Unity.StatePatternInUnity.Character>().isAlive)
                {
                    if (Vector3.Distance(npc.player.transform.position, npc.transform.position) <= npc.attackRange)
                    {
                        aiStateMachine.ChangeState(npc.attack);
                        npc.timePassed = 0;
                    }
                }
                npc.timePassed += Time.deltaTime;
            }
            return;
        }
    }
}
