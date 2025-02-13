using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public Sprite[] MoveUp;
    public Sprite[] MoveDown;
    public Sprite[] Walk;
    public Sprite[] LayDown;

    // recycling a lot of the code from the first game :)
    [Tooltip("How fast is the player moving left to right?")]
    public float speed = 3;
	[Tooltip("How fast is the player able to pick up items?")]
    public float rateOfInteract = 3;


    private float _lastTimeInteract = 1;
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
        HandleMovement();
        HandleInteract();

    }
    
    private void HandleMovement(){

        // LAYING DOWN IS A NEW MECHANIC
        // LOAD A "MOVE DOWN" SPRITE AND LAYING DOWN SPRITES SEPARATELY
        // use _isLayingDown to determine if player is laying down
        
        // how do i tell the difference between falling and standing on ground
        
        float horizontal = Input.GetAxis("Horizontal");     // walking
        float vertical = Input.GetAxis("Vertical");         // jumping

        // jump timeout
        

        transform.position += Vector3.right * horizontal * speed * Time.deltaTime;
        transform.position += Vector3.up * vertical * speed * Time.deltaTime;

        // edit sprite based on movement
        if(vertical > 0){
            _playerSpriteRenderer.sprite = MoveUp[0];
        } else if(vertical < 0){
            _playerSpriteRenderer.sprite = MoveDown[0];
        } else if(horizontal > 0){
            // flip sprite
            _playerSpriteRenderer.flipX = false;
            _playerSpriteRenderer.sprite = Walk[0];
        } else if(horizontal < 0){
            _playerSpriteRenderer.sprite = Walk[0];
        }

    }

    private void HandleInteract(){
        // if interact button is pressed
        if(Input.GetKey(KeyCode.E) && Time.time - _lastTimeInteract > 1/rateOfInteract){
            _lastTimeInteract = Time.time;

            // if player is near an interactable -> interact

            if (_hasPlant){
                // drop plant
                _hasPlant = false;
            } else {
                // pick up plant
                _hasPlant = true;
            }
        
            // CUE INTERACTION SOUNDS (plant sounds or interact sound?)
        
        }
    }

}