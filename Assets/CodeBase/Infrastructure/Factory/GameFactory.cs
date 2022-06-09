using Assets.CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameobject { get; set; }

        public GameObject HUDGameobject { get; private set; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticData = staticDataService;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameobject =  InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

            return HeroGameobject;
        }

        public GameObject CreateHud()
        {
            HUDGameobject = InstantiateRegistered(AssetPath.HudPath);
            return HUDGameobject;
        }


        public GameObject CreateMonster(MonsterTypeID monsterTypeID, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ToMonster(monsterTypeID);
            GameObject monster =  UnityEngine.Object.Instantiate(monsterData.MonsterPrefab, parent.position, Quaternion.identity, parent);
            var health = monster.GetComponent<IHealth>();
            health.CurrentHealth = monsterData.Hp;
            health.MaxHealth = monsterData.Hp;
            
            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameobject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameobject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;
            attack.AttackCooldown = monsterData.AttackCooldown;
            
            return monster;
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
