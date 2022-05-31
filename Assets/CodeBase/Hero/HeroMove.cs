using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        /*References*/
        private CharacterController _characterController;
        private IInputService _inputService;

        [Header("Settings")]
        [SerializeField]private float MovementSpeed = 4.0f;

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if(playerProgress.WorldData.PositionOnLevel.Level == GetCurrentLevel())
            {
                Vector3Data savedPosition = playerProgress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                {                    
                    Warp(savedPosition);
                }                
            }
        }      

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                //Трансформируем экранныые координаты вектора в мировые
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }
        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityvector().AddY(_characterController.height);
            _characterController.enabled = true;// small kostulb
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevel(), transform.position.AsVectorData());
        }

        private static string GetCurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}