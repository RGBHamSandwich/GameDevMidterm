using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public GameObject plantPrefab;

    private SpriteRenderer _plantSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _plantSpriteRenderer = GetComponent<SpriteRenderer>();
        // put sprite on top
        _plantSpriteRenderer.sortingOrder = 100;    // this will render the plant on top of everything
    }

    // Update is called once per frame
    void Update()
    {
        
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
