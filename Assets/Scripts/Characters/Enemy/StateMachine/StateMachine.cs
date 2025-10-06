
using System;
using System.Collections.Generic;

abstract class StateMachine
    {
        protected State CurrentState;
        protected Dictionary<Type, State> States;

        public void Update()
        {
            if (CurrentState == null)
                return;

            CurrentState.Update();

            CurrentState.TryTransit();
        }

        public void ChacgeState<TSate>() where TSate : State
        {
            if (CurrentState != null && CurrentState.GetType() == typeof(TSate))
                return;

            if (States.TryGetValue(typeof(TSate), out State newState))
            {
                CurrentState?.Exit(newState);
                State previousState = CurrentState;
                CurrentState = newState;
                CurrentState.Enter(previousState);
            }
        }
    }

