using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;

        public void SetValue(float current, float max)
        {
            _hpBar.fillAmount = current / max;
        }
    }
}
