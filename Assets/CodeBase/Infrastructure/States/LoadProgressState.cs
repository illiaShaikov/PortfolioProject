using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistantProgress;
using System;

namespace CodeBase.Infrastructure.States
{
    internal class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_persistentProgressService.PlayerProgress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew()
        {
            _persistentProgressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress(); 
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress("Main");
            progress.HeroState.MaxHealth = 50f;
            progress.HeroStats.Damage = 10f;
            progress.HeroStats.DamageRadius = 2f;
            progress.HeroState.ResetHP();
            
            return progress;
        }
    }
}