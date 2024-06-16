using Panda.Examples.PlayTag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class NPCAttackState : AIState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool attack;

        public NPCAttackState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            npc.SetAnimationBool(npc.attackParam, true);
            timePassed = 0f;

            //npc.animator.applyRootMotion = true;
        }

        public override void Exit()
        {
            base.Exit();
            npc.SetAnimationBool(npc.attackParam, false);

            //npc.animator.applyRootMotion = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (npc.player == null)
            {
                return;
            }

            timePassed += Time.deltaTime;
            clipLength = npc.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            clipSpeed = npc.animator.GetCurrentAnimatorStateInfo(0).speed;

            if (timePassed >= clipLength / clipSpeed && attack)
            {
                aiStateMachine.ChangeState(npc.attack);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                aiStateMachine.ChangeState(npc.roaming);
            }

            //Transition to chase state
            if (npc.newDestinationCD <= 0 && Vector3.Distance(npc.player.transform.position, npc.transform.position) <= npc.aggroRange)
            {
                npc.newDestinationCD = 0.5f;
                //npc.agent.SetDestination(npc.player.transform.position);
                aiStateMachine.ChangeState(npc.chase);
            }
            npc.newDestinationCD -= Time.deltaTime;
        }
    }
}