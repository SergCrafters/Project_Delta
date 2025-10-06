class ReturnedTransition : Transition
    {
        private BackToPoint _backToPoint;

        public ReturnedTransition(StateMachine stateMachine, BackToPoint backToPoint) : base(stateMachine) => _backToPoint = backToPoint;

        public override bool IsNeedTransit() => _backToPoint.IsReturned();

        public override void Transit() => StateMachine.ChacgeState<IdleState>();
    }

