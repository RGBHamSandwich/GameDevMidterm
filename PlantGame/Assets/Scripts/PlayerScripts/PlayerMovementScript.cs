using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Movement")]
    public Sprite Neutral;
    public Sprite[] MoveUp;
    public Sprite[] MoveDown;
    public Sprite[] Walk;
    public Sprite[] LayDown;
    [Tooltip("How fast is the player moving left to right?")]
    public float speed = 3;
    
    ///// PRIVATE VARIABLES /////
    private bool _layingDown = false;
    private SpriteRenderer _playerSpriteRenderer;
    private Rigidbody2D _playerRigidbody;

    ///// DELEGATES /////
    public delegate void OnPlayerMovement(Vector2 movement);
    public static OnPlayerMovement EOnPlayerMovement;

    ///// METHODS /////

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
    }
    
    private void HandleMovement(){

        float horizontal = Input.GetAxis("Horizontal"); 
        transform.position += Vector3.right * horizontal * speed * Time.deltaTime;

        // if(vertical < 0){            // stretch goal: limit movement until player presses jump (to stand up)
            // _layingDown = true;
            // _playerSpriteRenderer.sprite = MoveDown[0];
            // HandleLayingDown();
        // } else if(horizontal > 0){
            // _playerSpriteRenderer.flipX = false;
            // _playerSpriteRenderer.sprite = Walk[0];
            // HandleWalk();
        // } else if(horizontal < 0){
            // _playerSpriteRenderer.flipX = true;
            // _playerSpriteRenderer.sprite = Walk[0];
            // HandleWalk();
        // }
    }

}
