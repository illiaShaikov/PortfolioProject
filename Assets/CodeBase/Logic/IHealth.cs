using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        float CurrentHealth { get; set; }
        float MaxHealth { get; set; }
        event Action OnHealthChanged;
        void TakeDamage(float damage);
    }
}