using PlantGame.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class GreenhousePlant : MonoBehaviour
{
    private PlayerInteractPlantScript _hasplant;
    private PlayerInteractPlantScript _plant;
    private GameObject player;
    public SpriteRenderer plantRenderer;
    public GameObject plant;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        SpriteRenderer plantRenderer = GetComponent<SpriteRenderer>();
        GameObject plant = GetComponent<GameObject>();
        disablePlant();
    }

    // Update is called once per frame
    void Update()
    {
       placePlant();
    }

    void placePlant(){
        if (player != null){
        //if (_hasplant == true){
            if (player.transform.position.x == plant.transform.position.x){
                if (Input.GetKey(KeyCode.E)){
                    plantRenderer.enabled = true;
                    //Destroy(_plant);
                }
            }
        }
    }

    void disablePlant(){
        
        if (plantRenderer != null){
            plantRenderer.enabled = false;
        }
    }
    
}
