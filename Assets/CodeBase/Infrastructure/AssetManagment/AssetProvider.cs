using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 spawnPoint)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, spawnPoint, Quaternion.identity);
        }
    }
}
