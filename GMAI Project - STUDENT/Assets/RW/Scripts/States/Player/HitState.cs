using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class HitState : StandingState
    {
        private float timePassed;
        private float clipLength;
        private float clipSpeed;
        private bool hit;

        public HitState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.hitParam, true);
            hit = false;
            timePassed = 0f;

            //Debug.Log("Taking damage");
        }

        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.hitParam, false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            timePassed += Time.deltaTime;
            //clipLength = character.anim.GetCurrentAnimatorClipInfo(2)[0].clip.length;
            //clipSpeed = character.anim.GetCurrentAnimatorStateInfo(2).speed;

            //if (timePassed >= clipLength / clipSpeed && hit)
            //{
            //    stateMachine.ChangeState(character.hit);
            //}

            //if (timePassed >= clipLength / clipSpeed)
            //{
            //    stateMachine.ChangeState(character.standing);
            //}

            var clipInfo = character.anim.GetCurrentAnimatorClipInfo(2);
            if (clipInfo.Length > 0)
            {
                clipLength = clipInfo[0].clip.length;
                clipSpeed = character.anim.GetCurrentAnimatorStateInfo(2).speed;

                if (timePassed >= clipLength / clipSpeed && hit)
                {
                    stateMachine.ChangeState(character.hit);
                }

                if (timePassed >= clipLength / clipSpeed)
                {
                    stateMachine.ChangeState(character.standing);
                }
            }
            else
            {
                Debug.LogWarning("No animation clip found on layer 2.");
                // Handle cases where there is no animation on layer 2
                stateMachine.ChangeState(character.standing);
            }
        }

        //public void TakeDamage(float damageAmount)
        //{
        //    character.health -= damageAmount;
        //    character.SetAnimationBool(character.hitParam, true);
        //    CameraShake.Instance.ShakeCamera(2f, 0.2f);

        //    if (character.health <= 0)
        //    {
        //        character.Die();
        //    }
        //}
    }
}