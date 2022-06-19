using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HPBar _hpBar;
        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnHealthChanged += UpdateHPBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
            {
                Construct(health);
            }
        }

        public void UpdateHPBar()
        {
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
        }

        private void OnDestroy()
        {
            //_health.OnHealthChanged -= UpdateHPBar;
        }
    }
}