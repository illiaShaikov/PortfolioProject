using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickUpVFXPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickUpPopUp;
        
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;
        
        private float _destroyCooldown = 1.5f;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }
        
        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }

        private void PickUp()
        {
            if (_picked)
            {
                return;
            }
            _picked = true;
            
            UpdateWorldData();
            HideSkull();
            PlayPickUpVFX();
            ShowPopUp();
            StartCoroutine(StartDestroyTimer(_destroyCooldown));
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideSkull()
        {
            _skull.SetActive(false);
        }

        private void PlayPickUpVFX()
        {
            Instantiate(_pickUpVFXPrefab, transform.position, Quaternion.identity);
        }

        private void ShowPopUp()
        {
            _lootText.text = $"{_loot.Value}";
            _pickUpPopUp.SetActive(true);
        }

        private IEnumerator StartDestroyTimer(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            Destroy(gameObject);
        }
    }
}