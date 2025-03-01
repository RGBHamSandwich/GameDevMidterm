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
            float yPlant = this.transform.position.y;
            float yPlayer = player.transform.position.y;
            float y = yPlant - yPlayer;
            transform.SetParent(player.transform);
            transform.localPosition = new Vector3(0.5f, y, 0f);
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
