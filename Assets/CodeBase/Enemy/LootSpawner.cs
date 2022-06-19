using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        /*References*/
        private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private IRandomService _random;
        /*Fields*/
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }
        private void Awake()
        {
            _enemyDeath = GetComponentInParent<EnemyDeath>();
        }

        private void Start()
        {
            _enemyDeath.OnDeathHappened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            var lootItem = GenerateLoot();
            loot.Initialize(lootItem);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private Loot GenerateLoot()
        {
            var lootItem = new Loot()
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
            return lootItem;
        }
    }
}

