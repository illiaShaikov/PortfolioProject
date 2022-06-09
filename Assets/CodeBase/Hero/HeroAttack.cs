using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Logic;
using CodeBase.Services.Input;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private HeroAnimator _heroAnimator;
        private CharacterController _controller;
        private IInputService _inputService;
        private Stats _stats;
        
        private float _radius = 2f;

        private static int _layerMask;
        private Collider[] _hits = new Collider[3];

        private void Awake()
        {
            _heroAnimator = GetComponent<HeroAnimator>();
            _controller = GetComponent<CharacterController>();
            _inputService = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
            {
                _heroAnimator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _stats.DamageRadius, 10.0f);
            for (int i = 0; i < Hit(); ++i)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress playerProgress) => _stats = playerProgress.HeroStats;

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() => 
            new Vector3(transform.position.x, _controller.center.y / 2f, transform.position.z);
    }
}

