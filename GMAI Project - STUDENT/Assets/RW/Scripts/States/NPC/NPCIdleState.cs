using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class NPCIdleState : AIState
    {
        protected float speed;
        protected float rotationSpeed;

        public NPCIdleState(NPC npc, AIStateMachine aiStateMachine) : base(npc, aiStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            //npc.ResetMoveParams();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
