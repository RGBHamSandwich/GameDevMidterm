using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public Sprite Neutral;
    public Sprite[] MoveUp;
    public Sprite[] MoveDown;
    public Sprite[] Walk;
    public Sprite[] LayDown;
    public float framesPerSecond = 4;
    [Tooltip("How much force is applied to the player when they jump?")]
    public float thrust = .01f;
    [Tooltip("How long does the player have to wait before jumping again?")]
    public float jumpWaitTime = 1f;
    float frameTimer = 0;
    int currentFrameIndex = 0;

    // recycling a lot of the code from the first game :)
    [Tooltip("How fast is the player moving left to right?")]
    public float speed = 3;
	[Tooltip("How fast is the player able to pick up items?")]
    public float rateOfInteract = 3;
    [Header("Ground Checking")]
    [SerializeField] private Transform boxCheckPivot;
    [SerializeField] private float boxCheckSize = 1;
    [SerializeField] private LayerMask groundLayer;
    private float _lastTimeInteract = 1;
    private float _lastTimeJump = 1;
    private bool _hasPlant = false;
    private bool _layingDown = false;
    private SpriteRenderer _playerSpriteRenderer;
    private Rigidbody2D _playerRigidbody;

    ///// COROUTINES /////
    private IEnumerator _jumpCoroutine;
    private bool _isGrounded = true;

    ///// DELEGATES /////
    public delegate void OnPlayerMovement(Vector2 movement);
    public static OnPlayerMovement EOnPlayerMovement;
    public delegate void OnPickup();
    public static OnPickup EOnPickup;

    ///// METHODS /////

    private void OnDrawGizmosSelected(){
        if(boxCheckPivot == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCheckPivot.position, Vector3.one * boxCheckSize);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();

        // StartCoroutine(_jumpCoroutine(jumpWaitTime));
    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;

        CheckIfGrounded();

        HandleMovement();
        HandleJump();
        HandleInteract();

    }
    
    private void HandleMovement(){

        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        transform.position += Vector3.right * horizontal * speed * Time.deltaTime;

        if(vertical > 0 && _isGrounded){  // jump as long as we haven't jumped recently
            // _playerSpriteRenderer.sprite = MoveUp[0];
            // transform.position += Vector3.up * vertical * speed * Time.deltaTime;
            HandleJump();
        } else if(vertical < 0){            // stretch goal: limit movement until player presses jump (to stand up)
            _layingDown = true;
            // _playerSpriteRenderer.sprite = MoveDown[0];
            HandleLayingDown();
        } else if(horizontal > 0){
            _playerSpriteRenderer.flipX = false;
            // _playerSpriteRenderer.sprite = Walk[0];
            HandleWalk();
        } else if(horizontal < 0){
            _playerSpriteRenderer.flipX = true;
            // _playerSpriteRenderer.sprite = Walk[0];
            HandleWalk();
        }

        if(Input.GetKey(KeyCode.E) && Time.time - _lastTimeInteract > 1/rateOfInteract){
            _lastTimeInteract = Time.time;
            if(EOnPickup != null) EOnPickup.Invoke();
        }

    }

    private void HandleJump(){  // stolen from explosion.cs in Shootemup
        if(Input.GetKeyDown(KeyCode.W) && _isGrounded){
            _isGrounded = false;
            _jumpCoroutine = JumpCoroutine(jumpWaitTime);

            _lastTimeJump = Time.time;
            _playerRigidbody.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            if (frameTimer <= 0){
                currentFrameIndex++;
                if (currentFrameIndex >= MoveUp.Length){
                    currentFrameIndex = 0;
                    _playerSpriteRenderer.sprite = Neutral;
                }
                frameTimer = (1f / framesPerSecond);
                _playerSpriteRenderer.sprite = MoveUp[currentFrameIndex];
            }
        }
    }

    private void HandleWalk() {
        if (frameTimer <= 0){
            currentFrameIndex++;
            if (currentFrameIndex >= Walk.Length){
                currentFrameIndex = 0;
                _playerSpriteRenderer.sprite = Neutral;
            }
            frameTimer = (1f / framesPerSecond);
            _playerSpriteRenderer.sprite = Walk[currentFrameIndex];
        }
    }

    private void HandleLayingDown() {
        if (frameTimer <= 0){
            currentFrameIndex++;
            if (currentFrameIndex >= LayDown.Length){
                currentFrameIndex = 0;
                _playerSpriteRenderer.sprite = Neutral;
            }
            frameTimer = (1f / framesPerSecond);
            _playerSpriteRenderer.sprite = LayDown[currentFrameIndex];
        }
    }

    private void HandleInteract(){
        // if interact button is pressed
        if(Input.GetKey(KeyCode.E) && Time.time - _lastTimeInteract > 1/rateOfInteract){
            _lastTimeInteract = Time.time;


            if (_hasPlant){
                // drop plant
                _hasPlant = false;
            } else {
                // if player is near a plant
                // pick up plant
                _hasPlant = true;
            }

            if(EOnPickup != null) EOnPickup.Invoke();
        
            // CUE INTERACTION SOUNDS (plant sounds or interact sound?)
        
        }
    }

    private void CheckIfGrounded(){
        _isGrounded = Physics2D.OverlapBox(boxCheckPivot.position, Vector3.one * boxCheckSize, 0, groundLayer);
    }

    private IEnumerator JumpCoroutine(float waitTime){
        yield return new WaitForSeconds(waitTime);
        _isGrounded = true;
    }

}
