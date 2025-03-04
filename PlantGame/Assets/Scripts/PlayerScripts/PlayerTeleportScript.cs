using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO.Compression;

namespace PlantGame.Player 
{
    public class TeleportHomeScript : MonoBehaviour
    {
        ///// PUBLIC VARIABLES /////
        [Header("Teleporting Logistics")]
        public static TeleportHomeScript instance;
        [SerializeField] private Transform teleporterCheckPivot;
        [SerializeField] private float teleporterCheckSize = 1.5f;
        [SerializeField] private LayerMask teleporterLayer;
        [Header("Greenhouse Teleporting")]
        [Tooltip("Where should the player appear in the Greenhouse scene?")]
        public Vector3 GreenhouseTargetPosition = new Vector3(0, 0, 0);
        public string GreenhouseSceneName = "Greenhouse";
        [Header("Forest Teleporting")]
        [Tooltip("Where should the player appear in the Forest scene?")]
        public Vector3 ForestTargetPosition = new Vector3(0, 0, 0);
        public string ForestSceneName = "ForestLevel";

        public string LevelSelectSceneName = "Level Select";
        public bool _isTeleporterNearby = false;

        ///// PRIVATE VARIRABLES  /////
        private bool _canTeleport = true;
        private Rigidbody2D _playerRigidbody;

        ///// DELEGATES /////
        public delegate void OnPlayerTeleport();
        public static OnPlayerTeleport EOnPlayerTeleport;

        ///// COROUTINES /////
        private IEnumerator _teleportCoroutine;
        private IEnumerator _loadSceneCoroutine;

        ///// METHODS /////

        private void OnDrawGizmosSelected()
        {
            if(teleporterCheckPivot ==  null) 
            {
                Debug.Log("null teleporterCheckPivot");
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                teleporterCheckPivot.position, 
                Vector3.one * teleporterCheckSize
                );
        }

        void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();

            DontDestroyOnLoad(this.gameObject);

            if(instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        void Update()
        {
            int teleporterID = CheckIfTeleporterNearby();
            HandleTeleport(teleporterID);
        }

        private void HandleTeleport(int teleporterID)
        {
            if(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightControl))
            {
                if(_canTeleport && _isTeleporterNearby && teleporterID != -1)
                {
                    EOnPlayerTeleport?.Invoke();

                    StartCoroutine(TeleportCoroutine());
                    StartCoroutine(LoadSceneCoroutine(teleporterID));


                    _canTeleport = false;
                }
            }
        }

        private IEnumerator TeleportCoroutine()
        {
            yield return new WaitForSeconds(5f);
            _canTeleport = true;   
        }

        private IEnumerator LoadSceneCoroutine(int teleporterID)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if(teleporterID == 1)   // home
            {
                SceneManager.LoadScene(GreenhouseSceneName, LoadSceneMode.Single);
                this.transform.position = GreenhouseTargetPosition;
            }
            else if(teleporterID == 2)  // forest
            {
                SceneManager.LoadScene(ForestSceneName, LoadSceneMode.Single);
                this.transform.position = ForestTargetPosition;
            }
            else
            {
                Debug.Log("Teleporter ID not recognized");
            }

            yield return null;

        }

        private int CheckIfTeleporterNearby()
        {
            Collider2D[] teleportersNearby = Physics2D.OverlapBoxAll(
                teleporterCheckPivot.position, 
                Vector3.one * teleporterCheckSize, 
                0, 
                teleporterLayer);

            if(teleportersNearby.Length > 0)
            {
                _isTeleporterNearby = true;
                return teleportersNearby[0].GetComponent<TeleporterScript1>().teleporterID;      // if this process goofs, nothing happens
            }
            else
            {
                return -1;
            }
        }
    }
}