using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            Debug.Log("Enter in GameLoopState");
        }

        public void Exit()
        {
            Debug.Log("Exit in GameLoopState");
        }
    }
}