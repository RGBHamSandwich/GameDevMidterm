using UnityEngine;
using PlantGame.Player; 

namespace PlantGame.Player 
{
    public class PlayerAnimatorScript : MonoBehaviour
    {
        private Animator _playerAnimator;
        private SpriteRenderer _playerSpriteRenderer;
        
        void Start()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerSpriteRenderer = GetComponent<SpriteRenderer>();

            PlayerMovementScript.EOnPlayerMovement += UpdateMovement;
            PlayerJumpScript.EOnPlayerJump += TriggerJump;
            PlayerInteractPlantScript.EOnPlayerInteractPlant += TriggerPickUp;
            TeleportHomeScript.EOnPlayerTeleport += TriggerTeleport;   
        }

        void OnDestroy()    
        {
            PlayerMovementScript.EOnPlayerMovement -= UpdateMovement;
            PlayerJumpScript.EOnPlayerJump -= TriggerJump;
            PlayerInteractPlantScript.EOnPlayerInteractPlant -= TriggerPickUp;
            TeleportHomeScript.EOnPlayerTeleport -= TriggerTeleport;
        }
            
        private void UpdateMovement(Vector2 movement)
        {
            float playerMagnitude = movement.magnitude;
            _playerAnimator.SetFloat("Movement", playerMagnitude);

            if(movement.x != 0)
            {
                _playerSpriteRenderer.flipX = movement.x < 0f;
            }
        }

        private void TriggerJump()
        {
            _playerAnimator.SetTrigger("JumpTrigger");
        }

        private void TriggerPickUp()
        {
            _playerAnimator.SetTrigger("PickUpTrigger");
        }

        private void TriggerTeleport()
        {
            _playerAnimator.SetTrigger("TeleportTrigger");
        }
    }
}

