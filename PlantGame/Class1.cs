using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform player; // Reference to the player
    private GameObject pickedObject; // Reference to the picked object

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (pickedObject == null)
            {
                // Try to pick up an object
                RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.up, 1.0f);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Interactable"))
                {
                    pickedObject = hit.collider.gameObject;
                    pickedObject.transform.SetParent(player);
                    pickedObject.transform.localPosition = new Vector3(0, 1, 0); // Adjust the position as needed
                }
            }
            else
            {
                // Drop the object
                pickedObject.transform.SetParent(null);
                pickedObject = null;
            }
        }
    }
}