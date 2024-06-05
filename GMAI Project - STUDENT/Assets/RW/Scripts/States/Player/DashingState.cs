using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DashingState : StandingState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool dash;

        public DashingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //character.SetAnimationBool(character.dashParam, true);
            dash = false;
            timePassed = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            //character.SetAnimationBool(character.dashParam, false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
            dash = Input.GetButtonDown("Dash");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            clipLength = character.anim.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = character.anim.GetCurrentAnimatorStateInfo(1).speed;

            if (timePassed >= clipLength / clipSpeed && dash)
            {
                //stateMachine.ChangeState(character.dashing);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                stateMachine.ChangeState(character.standing);
            }
        }
    }
}