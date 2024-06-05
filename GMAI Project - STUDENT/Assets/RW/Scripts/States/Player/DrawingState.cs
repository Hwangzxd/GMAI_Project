using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawingState : StandingState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool sheath;
        private bool swing;

        public DrawingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.drawMeleeParam, true);
            character.Equip();
            sheath = false;
            swing = false;
            timePassed = 0f;
            Debug.Log("Entering DrawingState");
        }

        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.drawMeleeParam, false);
            Debug.Log("Exiting DrawingState");
        }

        public override void HandleInput()
        {
            base.HandleInput();
            sheath = Input.GetButtonDown("Sheath");
            swing = Input.GetButtonDown("Attack");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            clipLength = character.anim.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = character.anim.GetCurrentAnimatorStateInfo(1).speed;

            if (timePassed >= clipLength / clipSpeed && sheath)
            {
                Debug.Log("Sheath input detected");
                stateMachine.ChangeState(character.sheathing);
            }

            if (timePassed >= clipLength / clipSpeed && swing)
            {
                Debug.Log("Swing input detected");
                stateMachine.ChangeState(character.swinging);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                stateMachine.ChangeState(character.standing);
            }
        }
    }
}
