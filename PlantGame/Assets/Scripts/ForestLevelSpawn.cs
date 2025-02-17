using UnityEngine;

public class ForestLevelSpawn : MonoBehaviour
{
    public GameObject Bush;
    public GameObject Branch;
    private float _currentTimer;
    public float SpawnTimer;
    public float distanceMin;
    public float distanceMax;
    private float start = 34.8f;
    public GameObject Background;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void SpawnNewObstacles()
    {
        //Specific min for bushes and branches needed
        //Changing the range
        //Randomize the x position rather than the Y position

        Vector3 spawnPositionDown = transform.position + new Vector3 (1,0,0) * Random.Range(8,15);
        Vector3 spawnPositionUp = transform.position + new Vector3 (1,0,0) * Random.Range(8, 15);
        spawnPositionDown.z = 0;
        spawnPositionUp.z = 0;
        spawnPositionDown.y = Random.Range(0,1.5f);
        spawnPositionUp.y = Random.Range(4,7.5f);

        Instantiate(Bush, spawnPositionDown, Quaternion.identity);
        Instantiate(Branch, spawnPositionUp, Quaternion.identity);


    }

    private void SpawnBackground()
    {
        Vector3 spawnPosition;
        spawnPosition.z = 0;
        spawnPosition.x = start;
        spawnPosition.y = 0;
        Instantiate(Background, spawnPosition, Quaternion.identity);
}

    // Update is called once per frame
    void Update()
    {
        if( transform.position.x >= start-30)
        {
            SpawnNewObstacles();
            SpawnBackground();
            SpawnBackground();
            start = start + 28.8f;
        }
        
    }
}
