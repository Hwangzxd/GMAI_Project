using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class NPCHitState : AIState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool hit;

        public NPCHitState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            npc.SetAnimationBool(npc.hitParam, true);
            hit = false;
            timePassed = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            npc.SetAnimationBool(npc.hitParam, false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            clipLength = npc.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = npc.animator.GetCurrentAnimatorStateInfo(1).speed;

            if (timePassed >= clipLength / clipSpeed && hit)
            {
                aiStateMachine.ChangeState(npc.hit);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                aiStateMachine.ChangeState(npc.idle);
            }
        }
    }
}