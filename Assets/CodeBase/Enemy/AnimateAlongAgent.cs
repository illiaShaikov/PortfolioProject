using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private EnemyAnimator _animator;

        private const float MinimalVelocity = 0.1f;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<EnemyAnimator>();
        }

        private void Update()
        {
            if(ShouldMove())
            {
                _animator.Move(_agent.velocity.magnitude);
            }
            else
            {
                _animator.StopMoving();
            }            
        }

        private bool ShouldMove() => _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}
