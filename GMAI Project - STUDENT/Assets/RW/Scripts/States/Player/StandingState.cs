/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class StandingState : GroundedState
    {
        private bool jump;
        private bool crouch;
        //private bool dash;
        private bool draw;
        private bool sheath;
        private bool attack;
        private bool hit;
        private bool dead;

        HealthSystem healthSystem;

        public StandingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        private void HandleHit()
        {
            hit = true;
        }

        public override void Enter()
        {
            base.Enter();
            speed = character.MovementSpeed;
            rotationSpeed = character.RotationSpeed;
            crouch = false;
            jump = false;
            //dash = false;
            draw = false;
            sheath = false;
            attack = false;
            hit = false;
            dead = false;

            healthSystem = character.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.OnHit += HandleHit;
            }
        }

        public override void Exit()
        {
            base.Exit();
            if (healthSystem != null)
            {
                healthSystem.OnHit -= HandleHit;
            }
        }

        public override void HandleInput()
        {
            base.HandleInput();
            crouch = Input.GetButtonDown("Crouch");
            jump = Input.GetButtonDown("Jump");
            //dash = Input.GetButtonDown("Dash");
            draw = Input.GetButtonDown("Draw");
            sheath = Input.GetButtonDown("Sheath");
            attack = Input.GetButtonDown("Attack");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (crouch)
            {
                stateMachine.ChangeState(character.ducking);
            }

            //if (dash)
            //{
            //    stateMachine.ChangeState(character.dashing);
            //}

            if (draw && !character.isWeaponDrawn)
            {
                stateMachine.ChangeState(character.drawing);
            }

            if (sheath && character.isWeaponDrawn)
            {
                stateMachine.ChangeState(character.sheathing);
            }

            if (attack && character.isWeaponDrawn)
            {
                stateMachine.ChangeState(character.swinging);
            }

            if (hit)
            {
                stateMachine.ChangeState(character.hit);
            }

            if (dead)
            {
                stateMachine.ChangeState(character.death);
            }

            else if (jump)
            {
                stateMachine.ChangeState(character.jumping);
            }
        }
    }
}
