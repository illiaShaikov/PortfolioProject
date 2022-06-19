using CodeBase.Infrastructure.Factory;
using CodeBase.CameraLogic;
using CodeBase.Enemy;
using CodeBase.Hero;
using UnityEngine;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        /*References*/
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticData;

        /*Constants*/
        private const string EnemySpawnerTag = "EnemySpawner";
        private const string InitialPointTag = "InitialPoint";

        public LoadLevelState(GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingScreen loadingScreen,
            IGameFactory gameFactory,
            IPersistentProgressService persistentProgressService,
            IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();
            GameObject hero = InitHero();
            InitHUD(hero);

            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            
            foreach (EnemySpawnerData enemySpawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(enemySpawnerData.Id, enemySpawnerData.MonsterTypeID, enemySpawnerData.position);
            }
        }

        private GameObject InitHero()
        {
            return _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(InitialPointTag));
        }

        private void InitHUD(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());

        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
        }
        public void Exit()
        {
            _loadingScreen.Hide();
        }
    }
}