using Assets.CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(GameObject at) => 
            InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

        public void CreateHud() =>
            InstantiateRegistered(AssetPath.HudPath);

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

      
        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            ProgressReaders.Add(progressReader);
        }
    }
}
