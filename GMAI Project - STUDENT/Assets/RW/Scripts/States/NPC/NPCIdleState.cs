using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class NPCIdleState : AIState
    {
        private bool attack;
        private bool hit;
        private bool dead;

        public NPCIdleState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            attack = false;
            hit = false;
            dead = false;
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

            //if (attack)
            //{
            //    aiStateMachine.ChangeState(npc.attack);
            //}

            //if (npc.timePassed >= npc.attackCD && npc.player.GetComponent<RayWenderlich.Unity.StatePatternInUnity.Character>().isAlive)
            //{
            //    if (Vector3.Distance(npc.player.transform.position, npc.transform.position) <= npc.attackRange)
            //    {
            //        aiStateMachine.ChangeState(npc.attack);
            //        npc.timePassed = 0;
            //    }
            //}
            //npc.timePassed += Time.deltaTime;

            if (hit)
            {
                aiStateMachine.ChangeState(npc.hit);
            }

            else if (dead)
            {
                aiStateMachine.ChangeState(npc.death);
            }
        }
    }
}
