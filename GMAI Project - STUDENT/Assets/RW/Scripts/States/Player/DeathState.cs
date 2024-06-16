using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DeathState : StandingState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool dead;

        public DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.deathParam, true);
            dead = false;
            timePassed = 0f;

            character.isAlive = false; 

            Debug.Log("Died");
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            clipLength = character.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            clipSpeed = character.anim.GetCurrentAnimatorStateInfo(0).speed;

            if (timePassed >= clipLength / clipSpeed && dead)
            {
                stateMachine.ChangeState(character.death);
            }
        }
    }
}