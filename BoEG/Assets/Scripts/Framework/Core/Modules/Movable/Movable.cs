using Framework.Types;
using Framework.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace Framework.Core.Modules
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movable : Module, IMovable, IInstantiable<IMovableData>, ISpawnable
    {
        [System.Serializable]
        public struct DebugData
        {
            public float MoveSpeed;        
            public float TurnSpeed;
            public bool HasReachedDestenation;
            public void Update(IMovable movable)
            {
                MoveSpeed = movable.MoveSpeed;
                TurnSpeed = movable.TurnSpeed;
                HasReachedDestenation = movable.HasReachedDestenation;
            }
        }

        [SerializeField]
        private DebugData _debug;

        protected override void LateUpdate()
        {
            _debug.Update(this);
        }
        
        private NavMeshAgent _agent;


        protected override void PreStep(float deltaStep)
        {
            base.PreStep(deltaStep);
            if (IsSpawned)
                UpdateAgent();
        }


        void IInstantiable.Instantiate()
        {
            IsInitialized = true;
        }

        private IMovableData _data;
        protected bool IsInitialized { get; private set; }

        void IInstantiable<IMovableData>.Instantiate(IMovableData data)
        {
            _agent = GetComponent<NavMeshAgent>();
            _data = data;
            IsInitialized = true;
        }

        void IInstantiable.Terminate()
        {
            IsInitialized = false;
        }

        protected bool IsSpawned { get; private set; }

        void ISpawnable.Spawn()
        {
            IsSpawned = true;
            UpdateAgent();
        }

        void ISpawnable.Despawn()
        {
            IsSpawned = false;
        }

        public float MoveSpeed
        {
            get { return IsInitialized ? _data.MoveSpeed : 0f; }
        }

        public float TurnSpeed
        {
            get { return IsInitialized ? _data.TurnSpeed : 0f; }
        }

        private void UpdateAgent()
        {
            if (!IsSpawned)
                return;


            _agent.acceleration = TurnSpeed / 360f * MoveSpeed * 1000;
            _agent.speed = MoveSpeed;
            _agent.angularSpeed = TurnSpeed;
            if (HasReachedDestenation)
                CancelMovement();
        }

        public bool HasReachedDestenation
        {
            get
            {
                if (!IsSpawned)
                    return true;
                if (_agent.pathPending)
                    return false;
                return (_agent.pathEndPosition - transform.position).sqrMagnitude.SafeEquals(0f);
            }
        }

        public void MoveTo(Vector3 destenation)
        {
            if (IsSpawned)
                _agent.SetDestination(destenation);
        }

        public void WarpTo(Vector3 destenation)
        {
            if (IsSpawned)
                _agent.Warp(destenation);
        }

        public void Push(Vector3 direction)
        {
            if (IsSpawned)
                _agent.Move(direction);
        }

        public void CancelMovement()
        {
            if (IsSpawned)
                _agent.ResetPath();
        }

        public void StopMovement()
        {
            if (IsSpawned)
                _agent.isStopped = true;
        }

        public void StartMovement()
        {
            if (IsSpawned)
                _agent.isStopped = false;
        }
    }
}