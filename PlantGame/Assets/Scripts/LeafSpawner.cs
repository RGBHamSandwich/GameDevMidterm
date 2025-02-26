using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    public GameObject leafPrefab;
    private float spawnInterval = .5f;
    private float spawnTimer = 0.0f;
    public Transform cameraTarget;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0.0f;
            Vector3 randomPosition = new Vector3(
                Random.Range(cameraTarget.position.x - 10, cameraTarget.position.x + 10),
                16,
                0
            );
            Instantiate(leafPrefab, randomPosition, Quaternion.identity);
            if (randomPosition.x < -5) { Destroy(leafPrefab); }
            spawnTimer = 0.0f;
        }
    }
}