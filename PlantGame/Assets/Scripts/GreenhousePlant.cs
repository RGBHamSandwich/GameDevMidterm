using PlantGame.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class GreenhousePlant : MonoBehaviour
{
    private PlayerInteractPlantScript _hasplant;
    private PlayerInteractPlantScript _plant;
    private GameObject player;
    public SpriteRenderer plant;
    public GameObject plant_pos;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        SpriteRenderer plant = GetComponent<SpriteRenderer>();
        GameObject plant_pos = GetComponent<GameObject>();
        disablePlant();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasplant == true){
            if (player.transform.position.x == plant_pos.transform.position.x){
                if (Input.GetKey(KeyCode.E)){
                    plant.enabled = true;
                    Destroy(_plant);
                }
            }
        }
    }

    void disablePlant(){
        plant.enabled = false;
    }
    
}
