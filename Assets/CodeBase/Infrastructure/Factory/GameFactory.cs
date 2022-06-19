using Assets.CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawner;
using CodeBase.Services.Randomizer;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;
        
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameobject { get; set; }

        public GameObject HUDGameobject { get; private set; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IRandomService randomService, IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticDataService;
            _randomService = randomService;
            _persistentProgressService = progressService;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameobject =  InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

            return HeroGameobject;
        }

        public GameObject CreateHud()
        {
            HUDGameobject = InstantiateRegistered(AssetPath.HudPath);
            HUDGameobject.GetComponentInChildren<LootCounter>().Construct(_persistentProgressService.PlayerProgress.WorldData);
            
            return HUDGameobject;
        }


        public GameObject CreateMonster(MonsterTypeID monsterTypeID, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeID);
            GameObject monster =  UnityEngine.Object.Instantiate(monsterData.MonsterPrefab, parent.position, Quaternion.identity, parent);
            var health = monster.GetComponent<IHealth>();
            health.CurrentHealth = monsterData.Hp;
            health.MaxHealth = monsterData.Hp;
            
            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameobject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            
            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameobject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;
            attack.AttackCooldown = monsterData.AttackCooldown;
            
            return monster;
        }

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(AssetPath.Loot)
                .GetComponent<LootPiece>();
            lootPiece.Construct(_persistentProgressService.PlayerProgress.WorldData);
            return lootPiece;
        }

        public void CreateSpawner(string id, MonsterTypeID MonsterTypeID, Vector3 position)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, position).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.Id = id;
            spawner.MonsterTypeID = MonsterTypeID;
        }


        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string path, Vector3 position)
        {
            GameObject gameObject = _assetProvider.Instantiate(path, position);
            RegisterProgressWatchers(gameObject);

            return gameObject;
            
        }

        private GameObject InstantiateRegistered(string path)
        {
            GameObject gameObject = _assetProvider.Instantiate(path);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            ProgressReaders.Add(progressReader);
        }
    }
}
