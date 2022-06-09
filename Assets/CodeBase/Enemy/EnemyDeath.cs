using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _deathVFX;
        [SerializeField] private float _destroyTime = 2f;

        private EnemyAnimator _enemyAnimator;
        private EnemyHealth _enemyHealth;
        private AgentMoveToPlayer _move;

        public event Action OnDeathHappened;

        private void Awake()
        {
            _enemyAnimator = GetComponent<EnemyAnimator>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _move = GetComponent<AgentMoveToPlayer>();
        }

        private void Start() => _enemyHealth.OnHealthChanged += HealthChanged;

        private void OnDestroy() => _enemyHealth.OnHealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_enemyHealth.CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _enemyHealth.OnHealthChanged -= HealthChanged;
            _move.enabled = false;            
            _enemyAnimator.PlayDeath();
            Instantiate(_deathVFX, transform.position, Quaternion.identity);
            StartCoroutine(DestroyTimer());

            OnDeathHappened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
        }
    }
}

