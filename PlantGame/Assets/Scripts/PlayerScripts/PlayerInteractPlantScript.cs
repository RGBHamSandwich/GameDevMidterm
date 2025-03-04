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
        public bool _hasPlant = false;
        private Rigidbody2D _playerRigidbody;
        private bool _isPlantNearby = false;
        private bool _canInteractPlant = true;
        public GameObject _plant;
        private static GameObject _currentPlantPrefab;

        ///// COROUTINES /////
        private IEnumerator _pickUpCoroutine;

        ///// DELEGATES /////
        public delegate void OnPlayerInteractPlant();
        public static OnPlayerInteractPlant EOnPlayerInteractPlant;

        ///// METHODS /////
        private void OnDrawGizmosSelected()
        {
            if(plantCheckPivot == null) return;

            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(plantCheckPivot.position, 
                Vector3.one * plantCheckSize
                );
        }
        
        void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();

            if(_currentPlantPrefab != null)
            {
                _plant = Instantiate(_currentPlantPrefab, transform.position, Quaternion.identity);
                PlantScript plantScript = _plant.GetComponent<PlantScript>();
                plantScript.PickMeUp();
                _hasPlant = true;
            }
        }

        void Update()
        {
            CheckIfPlantNearby();
            HandleInteract();
        }

        private void HandleInteract() 
        {
            if((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightControl)) && !_hasPlant)
            {
                if(_isPlantNearby && _canInteractPlant)
                {
                    _canInteractPlant = false;
                    StartCoroutine(PickUpCoroutine(rateOfInteract));

                    PlantScript plantScript = _plant.GetComponent<PlantScript>();
                    plantScript.PickMeUp();
                    _currentPlantPrefab = _plant;

                    _hasPlant = true;

                    EOnPlayerInteractPlant?.Invoke();
                }

                if(AudioManager.instance != null) AudioManager.instance.HandlePickUp();
                
            }
            else if((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightControl)) && _hasPlant && _canInteractPlant)
            {
                bool teleporterNearby = GetComponent<TeleportHomeScript>()._isTeleporterNearby;
                if (teleporterNearby) return;
                // if (greenhouse space is nearby) 
                // { 
                    // put plant in greenhouse plant spot 
                // }
                // else 
                // { 
                    _canInteractPlant = false;
                    StartCoroutine(PickUpCoroutine(rateOfInteract));

                    PlantScript plantScript = _plant.GetComponent<PlantScript>();
                    plantScript.PutMeDown();
                    _currentPlantPrefab = null;

                    _hasPlant = false;

                    Debug.Log("No greenhouse nearby; putting plant on ground"); 
                // }
            }
        }

        private IEnumerator PickUpCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _canInteractPlant = true;
        }

        private void CheckIfPlantNearby()
        {

            Collider2D[] _nearbyPlants = Physics2D.OverlapBoxAll(
                plantCheckPivot.position, 
                Vector3.one * plantCheckSize, 
                0, 
                plantLayer
                );
            
            if(_nearbyPlants.Length > 0)
            {
                _isPlantNearby = true;
                _plant = _nearbyPlants[0].gameObject;
            }
            else
            {
                _isPlantNearby = false;
            }
        }
    }
}