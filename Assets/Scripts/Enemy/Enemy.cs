
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyVision), typeof(BackToPoint))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask _waypointLayer;
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;

    private EnemyStateMachine _stateMachine;

    private void Start()
    {
        var mover = GetComponent<Mover>();
        var vision = GetComponent<EnemyVision>();
        var backToPoint = GetComponent<BackToPoint>();

        _stateMachine = new EnemyStateMachine(mover, vision, backToPoint, _waypointLayer, _wayPoints, _maxSqrDistance, transform, _waitTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }
}

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

    abstract class State
    {
        protected Transition[] Transitions;

        protected State(StateMachine stateMachine) { }

        public virtual void Enter(State previousState) { }
        public virtual void Exit(State nextState) { }
        public virtual void Update() { }

        public virtual void TryTransit()
        {
            foreach (Transition transition in Transitions)
            {
                if (transition.IsNeedTransit())
                {
                    transition.Transit();
                    return;
                }
            }
        }
    }

    abstract class Transition
    {
        protected StateMachine StateMachine;

        protected Transition(StateMachine stateMachine) => StateMachine = stateMachine;

        public abstract bool IsNeedTransit();

        public abstract void Transit();
    }

    class EnemyStateMachine : StateMachine
    {
        public EnemyStateMachine(Mover mover, EnemyVision vision, BackToPoint backToPoint, LayerMask waypointLayer, WayPoint[] wayPoints,
                                float maxSqrDistance, Transform transform, float waitTime)
        {
            States = new Dictionary<Type, State>()
            {
                {typeof(PatrolState), new PatrolState(this, mover, vision, waypointLayer, wayPoints, maxSqrDistance, transform) },
                {typeof(IdleState), new IdleState(this, vision, waypointLayer, waitTime) },
                {typeof(FollowState), new FollowState(this, vision, mover, waypointLayer) },
                {typeof(ReturnState), new ReturnState(this, backToPoint, mover, vision, waypointLayer, wayPoints) },

            };

            ChacgeState<PatrolState>();
        }
    }

    class PatrolState : State
    {
        private WayPoint[] _wayPoints;
        private EnemyVision _vision;
        private Mover _mover;
        private Transform _target;
        private int _wayPointIndex;

        public PatrolState(StateMachine stateMachine, Mover mover, EnemyVision vision, LayerMask waypointLayer, WayPoint[] wayPoints,
                            float maxSqrDistance, Transform transform) : base(stateMachine)
        {
            _mover = mover;
            _vision = vision;
            _wayPoints = wayPoints;

            Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine, vision, waypointLayer),
                new TargetReachedTransition(stateMachine, this, maxSqrDistance, transform)
            };
        }

        public Transform Target => _target;

        public override void Enter(State previousState)
        {  
            if (_wayPointIndex == -1)
                FindClosestWayPoint();

            else 
                ChangeTarget();
        }

        public override void Exit(State nextState)
        {
            if (nextState is FollowState)
                _wayPointIndex = -1;
        }

        public override void Update()
        {
            _mover.Walk(_target);
            _vision.LookAtTarget(_target.position);
        }

        private void ChangeTarget()
        {
            _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
            _target = _wayPoints[_wayPointIndex].transform;
        }

        private void FindClosestWayPoint()
        {

            float minDistance = float.MaxValue;
            int closestIndex = 0;

            for (int i = 0; i < _wayPoints.Length; i++)
            {

                float distance = Vector3.Distance(_mover.transform.position, _wayPoints[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }

            _wayPointIndex = ++closestIndex % _wayPoints.Length;
            _target = _wayPoints[_wayPointIndex].transform;
        }
    }

    class IdleState : State
    {
        private float _endWaitTime;
        private float _waitTime;

        public IdleState(StateMachine stateMachine, EnemyVision vision, LayerMask waypointLayer, float waitTime) : base(stateMachine)
        {
            _waitTime = waitTime;

            Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine, vision, waypointLayer),
                new EndIdleTransition(stateMachine, this)
            };
        }

        public bool IsEndWait => _endWaitTime <= Time.time;

        public override void Enter(State previousState)
        {
            _endWaitTime = Time.time + _waitTime;
        }

    }

    class FollowState : State
    {
        private EnemyVision _vision;
        private LayerMask _waypointLayer;
        private Mover _mover;
        private Transform _target;

        public FollowState(StateMachine stateMachine, EnemyVision vision, Mover mover, LayerMask waypointLayer) : base(stateMachine)
        {
            _mover = mover;
            _vision = vision;
            _waypointLayer = waypointLayer;

            Transitions = new Transition[]
            {
                new LostTargetTransition(stateMachine, vision, _waypointLayer),
            };
        }

        public override void Enter(State previousState)
        {
            _vision.TrySeeTarget(out _target, _waypointLayer);
        }

        public override void Update()
        {
            if (_target != null)
            {
                _mover.Run(_target);
                _vision.LookAtTarget(_target.position);
            }
        }
    }

    class ReturnState : State
    {
        private BackToPoint _backToPoint;
        private Mover _mover;
        private EnemyVision _vision;
        private LayerMask _waypointLayer;
        private WayPoint[] _wayPoints;

        public ReturnState(StateMachine stateMachine, BackToPoint backToPoint, Mover mover, EnemyVision vision, 
                            LayerMask waypointLayer, WayPoint[] wayPoints): base(stateMachine)
        {
            _mover = mover;
            _vision = vision;
            _backToPoint = backToPoint;
            _waypointLayer = waypointLayer;
            _wayPoints = wayPoints;

            Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine, vision, waypointLayer),
                new ReturnedTansition(stateMachine, backToPoint)

            };
        }

        public override void Enter(State previousState) => _backToPoint.FindPathToRedPoint(_waypointLayer, _wayPoints);

        public override void Update()
        {
            Transform target = _backToPoint.GetNextTarget(); 

            if (target != null)
            {
                _mover.Walk(target);
                _vision.LookAtTarget(target.position);
            }
        }

    public override void Exit(State nextState) => _backToPoint.ClearPath();
}

    class SeeTargetTransition : Transition
    {
        private EnemyVision _vision;
        private LayerMask _waypointLayer;

        public SeeTargetTransition(StateMachine stateMachine, EnemyVision vision, LayerMask waypointLayer) : base(stateMachine)
        {
            _waypointLayer = waypointLayer;
            _vision = vision;
        }

        public override bool IsNeedTransit() => _vision.TrySeeTarget(out Transform _, _waypointLayer);

        public override void Transit() => StateMachine.ChacgeState<FollowState>();
    }

    class LostTargetTransition : Transition
    {
        private EnemyVision _vision;
        private LayerMask _waypointLayer;
        private float _endFindTime;
        private float _tryFindTime = 1;

    public LostTargetTransition(StateMachine stateMachine, EnemyVision vision, LayerMask waypointLayer) : base(stateMachine)
        {
            _waypointLayer = waypointLayer;
            _vision = vision;
        }

    public override bool IsNeedTransit()
    {
        if (_vision.TrySeeTarget(out Transform _, _waypointLayer))
        {
            _endFindTime = Time.time + _tryFindTime;
        }
        else if (_endFindTime < Time.time)
        {
            return true;
        }

        return false;
    }

    public override void Transit() => StateMachine.ChacgeState<ReturnState>();
    }

    class EndIdleTransition : Transition
    {
        private IdleState _idleState;

        public EndIdleTransition(StateMachine stateMachine, IdleState idleState) : base(stateMachine) => _idleState = idleState;

        public override bool IsNeedTransit() => _idleState.IsEndWait;

        public override void Transit() => StateMachine.ChacgeState<PatrolState>();
    }

    class TargetReachedTransition : Transition
    {
        private PatrolState _patrolState;
        private Transform _transform;
        private float _maxSqrDistance;

        public TargetReachedTransition(StateMachine stateMachine, PatrolState patrolState, float maxSqrDistance, Transform transform) : base(stateMachine)
        {
            _transform = transform;
            _patrolState = patrolState;
            _maxSqrDistance = maxSqrDistance;
        }

        public override bool IsNeedTransit()
        {
            float sqeDistance = (_transform.position - _patrolState.Target.position).sqrMagnitude;

            return sqeDistance < _maxSqrDistance;
        }

        public override void Transit() => StateMachine.ChacgeState<IdleState>();
    }

    class ReturnedTansition : Transition
    {
        private BackToPoint _backToPoint;

        public ReturnedTansition(StateMachine stateMachine, BackToPoint backToPoint) : base(stateMachine) => _backToPoint = backToPoint;

        public override bool IsNeedTransit() => _backToPoint.IsReturned();

        public override void Transit() => StateMachine.ChacgeState<IdleState>();
    }

