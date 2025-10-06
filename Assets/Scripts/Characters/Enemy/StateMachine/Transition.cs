abstract class Transition
    {
        protected StateMachine StateMachine;

        protected Transition(StateMachine stateMachine) => StateMachine = stateMachine;

        public abstract bool IsNeedTransit();

        public abstract void Transit();
    }

