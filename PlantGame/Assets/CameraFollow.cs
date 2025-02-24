using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;
    public GameObject leafPrefab; // Add this line
    public float spawnInterval = .5f; // Time interval between spawns
    private float spawnTimer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void FollowCameraTargetHorizontally()
    {
        if (cameraTarget.position.x < 2)
        {
            return;
        }

        Vector3 targetPosition = transform.position;
        if ((cameraTarget.position.x != targetPosition.x))
        {
            targetPosition.x = cameraTarget.position.x;
            transform.position = targetPosition;
        }
        else
        { transform.position = targetPosition; }

    }

    public void spawnLeaves()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0.0f;
            Vector3 randomPosition = new Vector3(
                Random.Range(cameraTarget.position.x - 10, cameraTarget.position.x + 10),
                17,
                0
            );
            Instantiate(leafPrefab, randomPosition, Quaternion.identity);
            if (randomPosition.x < -5) { Destroy(leafPrefab); }
            spawnTimer = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnLeaves();
        FollowCameraTargetHorizontally();
    }
}