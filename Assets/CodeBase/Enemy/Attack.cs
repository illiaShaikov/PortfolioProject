using System.Linq;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        /*Properties*/
        public float EffectiveDistance
        {
            get => _effectiveDistance;
            set => _effectiveDistance = value;
        }

        public float Cleavage
        {
            get => _cleavage;
            set => _cleavage = value;
        }

        public float AttackCooldown
        {
            get =>  attackCoolDown;
            set =>  attackCoolDown = value;
        }

        public float Damage 
        {
            get =>  _damage;
            set =>  _damage = value;
        }
        
        /*Private Fields*/
        private float _effectiveDistance;
        private float _cleavage;
        private float attackCoolDown;
        private float _damage;
        private bool _attackIsActive;
        private float _attackCoolDown;
        private bool _isAttacking;
        private int _layerMask;
        
        /*References*/
        private EnemyAnimator _enemyAnimator;
        private Transform _heroTransform;
        
        /*Collections*/
        private Collider[] _hits = new Collider[1];


        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Awake()
        {
            _enemyAnimator = GetComponent<EnemyAnimator>();
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        void Update()
        {
            UpdateCooldown();
            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _cleavage, 1.0f);
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        private void OnAttackEnded()
        {
            _attackCoolDown = attackCoolDown;
            _isAttacking = false;
        }

        public void EnableAttack() => _attackIsActive = true;

        public void DisableAttack() => _attackIsActive = false;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _attackCoolDown -= Time.deltaTime;
            }
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _enemyAnimator.PlayAttack();

            _isAttacking = true;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * _effectiveDistance;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            
            return hitsCount > 0;
        }
        
        private bool CanAttack() => _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() => _attackCoolDown <= 0;
    }
}


