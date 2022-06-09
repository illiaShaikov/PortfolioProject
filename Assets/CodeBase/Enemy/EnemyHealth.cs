using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public float CurrentHealth
        {
            get => _currentHealth; 
            set => _currentHealth = value;
        }

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        private EnemyAnimator _enemyAnimator;

        public event Action OnHealthChanged;

        private void Awake()
        {
            _enemyAnimator = GetComponent<EnemyAnimator>();
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= damage;
                _enemyAnimator.PlayHit();
                OnHealthChanged?.Invoke();
            }
        }
    }
}

