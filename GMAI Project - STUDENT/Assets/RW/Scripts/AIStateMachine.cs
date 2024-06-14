using UnityEngine;
using System.Collections;

namespace FSM
{
    public class AIStateMachine
    {
        public AIState CurrentState { get; private set; }

        public void Initialize(AIState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(AIState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }

    }
}
