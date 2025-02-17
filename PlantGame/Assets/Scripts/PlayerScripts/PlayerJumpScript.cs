using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Jumping")]
    [Tooltip("How much force is applied to the player when they jump?")]
    public float thrust = 5f;
    [Tooltip("How long does the player have to wait before jumping again?")]
    public float jumpWaitTime = 1f;
    [Header("Ground Checking")]
    [SerializeField] private Transform boxCheckPivot;
    [SerializeField] private float boxCheckSize = 0.6f;
    [SerializeField] private LayerMask groundLayer;

    ///// PRIVATE VARIABLES /////
    private Rigidbody2D _playerRigidbody;

    ///// COROUTINES /////
    private IEnumerator _jumpCoroutine;
    private bool _isGrounded = true;

    ///// METHODS /////

    private void OnDrawGizmosSelected(){
        if(boxCheckPivot == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCheckPivot.position, Vector3.one * boxCheckSize);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        HandleJump();
    }

    private void HandleJump()
    {
        if((Input.GetKeyDown(KeyCode.W) | (Input.GetKeyDown(KeyCode.Space))) && _isGrounded) 
        {
            _isGrounded = false;
            StartCoroutine(JumpCoroutine(jumpWaitTime));
            _playerRigidbody.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            
        }
    }

    private IEnumerator JumpCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isGrounded = true;
    }

    private void CheckIfGrounded()
    {
        _isGrounded = Physics2D.OverlapBox(boxCheckPivot.position, Vector3.one * boxCheckSize, 0, groundLayer);
    }

}
