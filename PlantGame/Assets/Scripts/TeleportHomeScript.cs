using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO.Compression;

public class TeleportHomeScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public static TeleportHomeScript instance;
    [Header("Greenhouse Teleporting")]
    [Tooltip("Where should the player appear in the Greenhouse scene?")]
    public Vector3 teleportTargetPosition = new Vector3(0, 0, 0);

    public string GreenhouseSceneName = "greenhouse";
    [SerializeField] private Transform teleporterCheckPivot;
    [SerializeField] private float teleporterCheckSize = 1.5f;
    [SerializeField] private LayerMask teleporterLayer;

    ///// PRIVATE VARIRABLES  /////
    private bool _canTeleport = true;
    private bool _isTeleporterNearby = false;
    private Rigidbody2D _playerRigidbody;

    ///// DELEGATES /////
    public delegate void OnPlayerInteractTeleporter();
    public static OnPlayerInteractTeleporter EOnPlayerInteractTeleporter;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        CheckIfTeleporterNearby();
        HandleTeleport();
    }

    private void HandleTeleport()
    {
        if(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightControl))
        {
            if(_canTeleport && _isTeleporterNearby)
            {
                StartCoroutine(TeleportCoroutine());
                StartCoroutine(LoadSceneCoroutine());

                Debug.Log("Player is teleporting home");

                EOnPlayerInteractTeleporter?.Invoke();

                _canTeleport = false;
            }
        }
    }

    private IEnumerator TeleportCoroutine()
    {
        yield return new WaitForSeconds(5f);
        _canTeleport = true;   
    }

    private IEnumerator LoadSceneCoroutine()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(GreenhouseSceneName, LoadSceneMode.Single);
        this.transform.position = teleportTargetPosition;
        SceneManager.UnloadSceneAsync(currentScene); 

        yield return null;

        Debug.Log("Player has teleported; previous scene unloaded");

    }

    private void CheckIfTeleporterNearby()
    {
        _isTeleporterNearby = Physics2D.OverlapBox(
            teleporterCheckPivot.position, 
            Vector3.one * teleporterCheckSize, 
            0, 
            teleporterLayer);
    }
}
