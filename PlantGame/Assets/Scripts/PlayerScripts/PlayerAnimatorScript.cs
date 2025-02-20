using UnityEngine;

namespace PlantGame.Player 
{
    public class PlayerAnimatorScript : MonoBehaviour
    {
        private Animator _playerAnimator;
        private SpriteRenderer _playerSpriteRenderer;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerSpriteRenderer = GetComponent<SpriteRenderer>();

            PlayerMovementScript.EOnPlayerMovement += UpdateMovement;
            PlayerJumpScript.EOnPlayerJump += TriggerJump;
            PlayerInteractPlantScript.EOnPlayerInteractPlant += TriggerPickUp;   
        }

        void OnDestroy()    // use this instead of onDisable?
        {
            // Unsubscribe to prevent memory leaks
            PlayerMovementScript.EOnPlayerMovement -= UpdateMovement;
            PlayerJumpScript.EOnPlayerJump -= TriggerJump;
            PlayerInteractPlantScript.EOnPlayerInteractPlant -= TriggerPickUp;
        }

        // Update is called once per frame
        void Update()
        {

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

    }
}




// # move bushes closer - test jumping
// # fix ground detection (smaller width)
