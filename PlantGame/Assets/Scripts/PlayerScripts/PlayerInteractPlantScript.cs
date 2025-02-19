using UnityEngine;
using System.Collections;

namespace PlantGame.Player 
{
    public class PlayerInteractPlantScript : MonoBehaviour
    {
        ///// PUBLIC VARIABLES /////
        [Tooltip("How fast is the player able to pick up items?")]
        public float rateOfInteract = 1f;
        [Header("Plant Checking")]
        [SerializeField] private Transform plantCheckPivot;
        [SerializeField] private float plantCheckSize = 1.5f;
        [SerializeField] private LayerMask plantLayer;

        ///// PRIVATE VARIRABLES  /////
        private bool _hasPlant = false;
        private Rigidbody2D _playerRigidbody;

        ///// DELEGATES /////
        public delegate void OnPlayerInteractPlant();
        public static OnPlayerInteractPlant EOnPlayerInteractPlant;

        ///// COROUTINES /////
        private IEnumerator _pickUpCoroutine;
        private bool _isPlantNearby = false;
        private bool _canPlant = true;

        ///// DELEGATES /////
        public delegate void OnPickUp();
        public static OnPickUp EOnPickUp;

        ///// METHODS /////
        private void OnDrawGizmosSelected()
        {
            if(plantCheckPivot == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(plantCheckPivot.position, Vector3.one * plantCheckSize);
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfPlantNearby();
            HandleInteract();
        }

        private void HandleInteract() 
        {
            if(Input.GetKey(KeyCode.E) && !_hasPlant)
            {
                //////////////////////////////////////////////////////// HOW DO WE MAKE SURE THE PLAYER ONLY PICKS UP ONE PLANT IF TWO ARE NEARBY?
                if(_isPlantNearby && _canPlant)
                {
                    _canPlant = false;
                    StartCoroutine(PickUpCoroutine(rateOfInteract));

                    // pick up plant
                    _hasPlant = true;
                    Debug.Log("Player picked up a plant");

                    EOnPlayerInteractPlant?.Invoke();
                }
                else
                {
                    // Debug.Log("No plant nearby or player not ready to pick up again");
                }
            }
            else if(Input.GetKey(KeyCode.E) && _hasPlant && _canPlant)
            {
                // if (greenhouse space is nearby) 
                // { 
                    // put plant in greenhouse plant spot 
                // }
                // else 
                // { 
                    _canPlant = false;
                    StartCoroutine(PickUpCoroutine(rateOfInteract));

                    // put plant down
                    _hasPlant = false;

                    Debug.Log("No greenhouse nearby; putting plant on ground"); 
                // }
            }
        }

        private IEnumerator PickUpCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _canPlant = true;
        }

        private void CheckIfPlantNearby()
        {
            _isPlantNearby = Physics2D.OverlapBox(plantCheckPivot.position, Vector3.one * plantCheckSize, 0, plantLayer);
        }
    }
}