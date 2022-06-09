using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1f;

        private NavMeshAgent _agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void Construct(Transform heroTransform) => _heroTransform = heroTransform;

        private void Update() => SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
            {
                _agent.destination = _heroTransform.position;
            }
        }
    }
}

