using System;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using CodeBase.StaticData;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour,ISavedProgress
    {
        [SerializeField] private MonsterTypeID  MonsterTypeID;
        [SerializeField] private bool _isSlain;

        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueID>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
            {
                _isSlain = true;
            }
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(MonsterTypeID, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.OnDeathHappened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null) 
                _enemyDeath.OnDeathHappened -= Slay;
            
            _isSlain = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_isSlain)
            {
                playerProgress.KillData.ClearedSpawners.Add(_id);
            }
        }
    }
}

