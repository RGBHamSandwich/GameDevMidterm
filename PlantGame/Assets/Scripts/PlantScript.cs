using UnityEngine;

public class PlantScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    }
}
