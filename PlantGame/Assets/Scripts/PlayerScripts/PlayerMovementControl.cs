using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    public MonoBehaviour PlayerMovementScript;

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