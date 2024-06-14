using UnityEngine;
using System.Collections;

namespace FSM
{
    public abstract class AIState
    {
        protected NPC npc;
        protected AIStateMachine aiStateMachine;

        protected AIState(NPC npc, AIStateMachine aiStateMachine)
        {
            this.npc = npc;
            this.aiStateMachine = aiStateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
