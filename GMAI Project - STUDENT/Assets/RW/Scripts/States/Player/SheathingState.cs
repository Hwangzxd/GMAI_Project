using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SheathingState : GroundedState
    {
        private bool sheath;

        public SheathingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.sheathMeleeParam, true);

            character.SheathWeapon();
        }

        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.sheathMeleeParam, false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
            sheath = Input.GetButtonDown("G");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (sheath)
            {
                stateMachine.ChangeState(character.sheathing);
            }
        }
    }
}