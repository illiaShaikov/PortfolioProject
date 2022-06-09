using System;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _deathVFX;
        
        private HeroHealth _heroHealth;
        private HeroMove _heroMove;
        private HeroAnimator _heroAnimator;
        private HeroAttack _heroAttack;
        
        private bool _isDead;

        private void Awake()
        {
            _heroHealth = GetComponent<HeroHealth>();
            _heroMove = GetComponent<HeroMove>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroAttack = GetComponent<HeroAttack>();
        }

        private void Start()
        {
            _heroHealth.OnHealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _heroHealth.OnHealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (_heroHealth.CurrentHealth <= 0 && !_isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            
            _heroMove.enabled = false;
            _heroAttack.enabled = false;
            
            _heroAnimator.PlayDeath();
            Instantiate(_deathVFX, transform.position, Quaternion.identity);
        }
    }
}

