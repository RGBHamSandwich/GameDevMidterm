using UnityEngine;
using System.Collections;

namespace PlantGame.Player 
{
    public class PlayerMovementScript : MonoBehaviour
    {
        ///// PUBLIC VARIABLES /////
        [Header("Movement")]
        [Tooltip("How fast is the player moving left to right?")]
        public float speed = 3;
        
        ///// PRIVATE VARIABLES /////
        // private bool _layingDown = false;
        private SpriteRenderer _playerSpriteRenderer;
        private Rigidbody2D _playerRigidbody;

        ///// DELEGATES /////
        public delegate void OnPlayerMovement(Vector2 movement);
        public static OnPlayerMovement EOnPlayerMovement;

        ///// METHODS /////
        void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
            _playerSpriteRenderer = GetComponent<SpriteRenderer>();
            _playerSpriteRenderer.sortingOrder = 101; 
        }

        void Update()
        {
            HandleMovement();
        }
        
        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal"); 
            transform.position += Vector3.right * horizontal * speed * Time.deltaTime;
            
            EOnPlayerMovement?.Invoke(Vector2.right * horizontal);
        }
    }
}