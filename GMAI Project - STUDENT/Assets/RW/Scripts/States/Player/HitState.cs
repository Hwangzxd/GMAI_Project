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

            Debug.Log("Taking damage");
            //SoundManager.Instance.PlaySound(SoundManager.Instance.meleeSwings);
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
            clipLength = character.anim.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = character.anim.GetCurrentAnimatorStateInfo(1).speed;

            if (timePassed >= clipLength / clipSpeed && hit)
            {
                stateMachine.ChangeState(character.hit);
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                stateMachine.ChangeState(character.standing);
            }
        }
    }
}