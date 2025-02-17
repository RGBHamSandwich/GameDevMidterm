using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerAnimator _playerAnimator;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PlayerScript.EOnPlayerMovement += HandlePlayerMovement;
        PlayerScript.EOnPickup += HandlePickup;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void HandlePickup(){
        // _playerAnimator.SetTrigger("Pickup");
    }

}
