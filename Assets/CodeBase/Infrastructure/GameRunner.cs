using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper _gamebootstrapper;
        private void Awake()
        {
            var bootstraper = FindObjectOfType<GameBootstrapper>();

            if(bootstraper == null)
            {
                Instantiate(_gamebootstrapper);
            }
        }
    }
}