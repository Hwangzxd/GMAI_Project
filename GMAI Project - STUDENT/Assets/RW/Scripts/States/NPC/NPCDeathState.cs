using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class NPCDeathState : AIState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool dead;

        public NPCDeathState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            npc.SetAnimationBool(npc.deathParam, true);
            dead = false;
            timePassed = 0f;
        }

        //public override void Exit()
        //{
        //    base.Exit();
        //    npc.SetAnimationBool(npc.deathParam, false);
        //}

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            clipLength = npc.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            clipSpeed = npc.animator.GetCurrentAnimatorStateInfo(0).speed;

            if (timePassed >= clipLength / clipSpeed && dead)
            {
                aiStateMachine.ChangeState(npc.death);
            }

            //if (timePassed >= clipLength / clipSpeed)
            //{
            //    aiStateMachine.ChangeState(npc.idle);
            //}
        }
    }
}