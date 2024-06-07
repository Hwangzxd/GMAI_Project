using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SwingingState : StandingState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool attack;

        public SwingingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.swingMeleeParam, true);
            attack = false;
            timePassed = 0f;

            //SoundManager.Instance.PlaySound(SoundManager.Instance.meleeSwings);
        }

        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.swingMeleeParam, false);
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
            clipLength = character.anim.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = character.anim.GetCurrentAnimatorStateInfo(1).speed;

            if (timePassed >= clipLength / clipSpeed && attack)
            {
                stateMachine.ChangeState(character.swinging);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                stateMachine.ChangeState(character.standing);
            }
        }
    }
}