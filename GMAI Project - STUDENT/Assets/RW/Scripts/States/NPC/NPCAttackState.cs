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

        //public int currentAttack = 0;
        //private float comboResetTime = 1f; // Time in seconds to reset the combo if no new attack

        public NPCAttackState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            npc.SetAnimationBool(npc.attackParam, true);
            attack = false;
            timePassed = 0f;

            npc.animator.applyRootMotion = true;
            //character.anim.SetFloat("speed", 0f);

            //PlayAttackAnimation();

            //SoundManager.Instance.PlaySound(SoundManager.Instance.meleeSwings);
        }

        public override void Exit()
        {
            base.Exit();
            npc.SetAnimationBool(npc.attackParam, false);

            npc.animator.applyRootMotion = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            attack = Input.GetButtonDown("Attack");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            clipLength = npc.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = npc.animator.GetCurrentAnimatorStateInfo(1).speed;

            if (timePassed >= clipLength / clipSpeed && attack)
            {
                aiStateMachine.ChangeState(npc.attack);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                aiStateMachine.ChangeState(npc.idle);
            }
        }
    }
}