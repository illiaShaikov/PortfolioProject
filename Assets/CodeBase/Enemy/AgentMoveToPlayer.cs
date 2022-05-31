using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1f;

        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;
        //private IGameFactory _gameFactory;
       
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            //_gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        private void Update()
        {
            if (HeroNotReached())
            {
                _agent.destination = _heroTransform.position;
            }              
        }

        private bool HeroNotReached()
        {
            return Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
        }
    }
}

