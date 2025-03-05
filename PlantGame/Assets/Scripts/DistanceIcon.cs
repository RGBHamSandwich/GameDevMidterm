using UnityEngine;

public class DistanceIcon : MonoBehaviour
{
    public SpriteRenderer distanceIcon;
    private GameObject player;
    public GameObject icon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //GameObject icon = GetComponent<GameObject>();
        distanceIcon = GetComponent<SpriteRenderer>();

        if (distanceIcon == null){
            Debug.LogError("distanceIcon is null");
        }
        if (icon == null){
            Debug.LogError("icon is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance();
    }

    void checkDistance(){

        float distance = Vector2.Distance(player.transform.position, icon.transform.position);

        if (distance <= 1.5f){
            distanceIcon.enabled = true;
        }
        else{
            distanceIcon.enabled = false;
        }
    }
}
