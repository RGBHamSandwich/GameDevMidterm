using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public Sprite Neutral;
    public Sprite[] MoveUp;
    public Sprite[] MoveDown;
    public Sprite[] Walk;
    public Sprite[] LayDown;
    public float framesPerSecond = 4;
    float frameTimer = 0;
    int currentFrameIndex = 0;

    // recycling a lot of the code from the first game :)
    [Tooltip("How fast is the player moving left to right?")]
    public float speed = 3;
	[Tooltip("How fast is the player able to pick up items?")]
    public float rateOfInteract = 3;
    private float _lastTimeInteract = 1;
    private float _lastTimeJump = 1;
    private bool _hasPlant = false;
    private bool _layingDown = false;
    private SpriteRenderer _playerSpriteRenderer;
    private Rigidbody2D _playerRigidbody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;

        // if(_layingDown){
        //     // check for stand up
        //     if (Input.GetAxis("Vertical") > 0) {
        //         _layingDown = false;
        //         _playerSpriteRenderer.sprite = Neutral;
        //     }
        //     else {
                HandleMovement();
                HandleInteract();
            // }
        // }
    }
    
    private void HandleMovement(){

        // LAYING DOWN IS A NEW MECHANIC
        // LOAD A "MOVE DOWN" SPRITE AND LAYING DOWN SPRITES SEPARATELY
        // use _isLayingDown to determine if player is laying down
        
        // how do i tell the difference between falling and standing on ground
        
        // if(_layingDown){
        //     _playerSpriteRenderer.sprite = LayDown[0];
        //     return;
        // }

        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        Debug.Log("horizontal: " + horizontal);
        transform.position += Vector3.right * horizontal * speed * Time.deltaTime;

        // edit sprite based on movement
        if(vertical > 0 && Time.time - _lastTimeJump > 1/rateOfInteract){  // jump as long as we haven't jumped recently
            // _playerSpriteRenderer.sprite = MoveUp[0];
            transform.position += Vector3.up * vertical * speed * Time.deltaTime;               //////////////////////////////////////////////// Can I put this here????
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

    }

    private void HandleJump(){  // stolen from explosion.cs in Shootemup
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
        
            // CUE INTERACTION SOUNDS (plant sounds or interact sound?)
        
        }
    }

}
