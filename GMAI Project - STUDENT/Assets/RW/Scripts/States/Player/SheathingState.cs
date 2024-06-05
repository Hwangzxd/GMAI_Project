using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SheathingState : StandingState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool draw;

        public SheathingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.sheathMeleeParam, true);
            character.SheathWeapon();
            draw = false;
            timePassed = 0f;
            Debug.Log("Entering SheathingState");
        }

        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.sheathMeleeParam, false);
            Debug.Log("Exiting SheathingState");
        }

        public override void HandleInput()
        {
            base.HandleInput();
            draw = Input.GetButtonDown("Draw");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            clipLength = character.anim.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = character.anim.GetCurrentAnimatorStateInfo(1).speed;

            if (timePassed >= clipLength / clipSpeed && draw)
            {
                Debug.Log("Draw input detected");
                stateMachine.ChangeState(character.drawing);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                stateMachine.ChangeState(character.standing);
            }
        }
    }
}