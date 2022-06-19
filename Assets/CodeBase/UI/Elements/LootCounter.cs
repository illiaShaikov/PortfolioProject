using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinCounter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCoinCounter;
        }

        private void Start()
        {
            UpdateCoinCounter();
        }

        private void UpdateCoinCounter()
        {
            coinCounter.text = $"{_worldData.LootData.Collected}";
        }
    }
}

