using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    public MonoBehaviour PlayerMovementScript; // Reference to the player's movement script

    public void DisablePlayerMovement()
    {
        if (PlayerMovementScript != null)
        {
            PlayerMovementScript.enabled = false;
        }
    }

    public void EnablePlayerMovement()
    {
        if (PlayerMovementScript != null)
        {
            PlayerMovementScript.enabled = true;
        }
    }
}