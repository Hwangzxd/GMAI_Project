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

        //public int currentAttack = 0;
        //private float comboResetTime = 1f; // Time in seconds to reset the combo if no new attack

        public SwingingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(character.swingMeleeParam, true);
            attack = false;
            timePassed = 0f;

            character.anim.applyRootMotion = true;
            //character.anim.SetFloat("speed", 0f);

            //PlayAttackAnimation();

            //SoundManager.Instance.PlaySound(SoundManager.Instance.meleeSwings);
        }

        public override void Exit()
        {
            base.Exit();
            character.SetAnimationBool(character.swingMeleeParam, false);

            character.anim.applyRootMotion = false;
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

        //private void PerformComboAttack()
        //{
        //    currentAttack++;
        //    if (currentAttack > 3) 
        //    {
        //        currentAttack = 1;
        //    }
        //    PlayAttackAnimation();
        //}

        //private void PlayAttackAnimation()
        //{
        //    switch (currentAttack)
        //    {
        //        case 1:
        //            character.SetAnimationBool(character.swingMeleeParam, true);
        //            break;
        //        case 2:
        //            character.SetAnimationBool(character.swingMelee2Param, true);
        //            break;
        //        case 3:
        //            character.SetAnimationBool(character.swingMelee3Param, true);
        //            break;
        //        // Add more cases if needed for additional combo attacks
        //        default:
        //            character.SetAnimationBool(character.swingMeleeParam, true);
        //            break;
        //    }
        //}
    }
}