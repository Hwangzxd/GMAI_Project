using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawingState : GroundedState
    {
        private bool draw;

        public DrawingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.drawMeleeParam, true);

            character.Equip();
        }

        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.drawMeleeParam, false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
            draw = Input.GetButtonDown("G");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (draw)
            {
                stateMachine.ChangeState(character.drawing);
            }
        }
    }
}
