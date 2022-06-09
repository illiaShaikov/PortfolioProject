using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        private HeroAnimator _heroAnimator;
        private State _state;
        
        public event Action OnHealthChanged;
        public float CurrentHealth 
        { 
            get => _state.CurrentHealth;
            set
            {
                if (_state.CurrentHealth != value)
                {
                    _state.CurrentHealth = value;
                    OnHealthChanged?.Invoke();
                }
            } 
        }

        public float MaxHealth
        {
            get => _state.MaxHealth; 
            set => _state.MaxHealth = value;
        }

        

        private void Awake()
        {
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _state = playerProgress.HeroState;
            OnHealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.HeroState.CurrentHealth = CurrentHealth;
            playerProgress.HeroState.MaxHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0)
            {
                return;
            }
            CurrentHealth -= damage;
            _heroAnimator.PlayHit();
        }
    }
}

