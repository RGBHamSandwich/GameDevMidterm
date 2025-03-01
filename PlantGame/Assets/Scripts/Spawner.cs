using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 10;
    public float horizontalJumpDistance = 5f; // The maximum distance the player can jump horizontally
    public float verticalJumpDistance = 3f; // The maximum distance the player can jump vertically

    private void Start()
    {
       
    }

    public void SpawnPlatforms(float Start)
    {
        Vector3 spawnPosition = new Vector3(Start, 2, 0);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            
            // Randomize the next platform's position within the jumpable range
            float xOffset = Random.Range(-horizontalJumpDistance, horizontalJumpDistance);
            float yOffset = Random.Range(2, verticalJumpDistance);
            if (spawnPosition.y + yOffset > 10)
            {
                yOffset = -yOffset;
            }
            spawnPosition += new Vector3(xOffset, yOffset, 0);

            // Instantiate the platform at the calculated position
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
