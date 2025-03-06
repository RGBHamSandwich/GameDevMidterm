using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public GameObject plantPrefab;

    private SpriteRenderer _plantSpriteRenderer;

    void Start()
    {
        _plantSpriteRenderer = GetComponent<SpriteRenderer>();
        _plantSpriteRenderer.sortingOrder = 100; 
    }
    public void PickMeUp()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            float yPlayer = player.transform.position.y;
            float xPlayer = player.transform.position.x;

            float yPlant = this.transform.position.y;
            // float xPlant = player.transform.position.x

            // bound plant position with player position
            float y = Mathf.Max(yPlant - yPlayer, yPlayer - 1f);
            y = Mathf.Min(y, yPlayer + 1f);
            float x = xPlayer + 0.6f;

            // float yPlant = yPlayer + 0.4f;
            // float xPlant = xPlayer - 0.6f;

            transform.SetParent(player.transform);
            transform.localPosition = new Vector3(0.6f, y, 0f);
        }   
    }

    public void PutMeDown()
    {
        transform.SetParent(null);
        
        GameObject plantHolder = GameObject.Find("PlantHolder");
        if (plantHolder != null)
        {
            transform.SetParent(plantHolder.transform);
        }
    }
}
