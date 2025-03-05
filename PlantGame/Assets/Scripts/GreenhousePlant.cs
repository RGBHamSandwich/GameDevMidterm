
using System;
using PlantGame.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class GreenhousePlant : MonoBehaviour
{
    //private bool _hasPlant;
    private PlayerInteractPlantScript _plant;
    private GameObject player;
    private PlayerInteractPlantScript playerScript;
    public SpriteRenderer plantRenderer;
    public GameObject plant;
    public SpriteRenderer distanceIcon;
   

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        plantRenderer = GetComponent<SpriteRenderer>();
        GameObject plant = GetComponent<GameObject>();
        playerScript = player.GetComponent<PlayerInteractPlantScript>();
        disablePlant();
        disableIcons();
    }

    void Update()
    {
       placePlant();
    }

    void placePlant(){
        //hi this is kendall!! please put the following line in at some point 
        //so that placing the plant also increases the bank balance
        //PlayerMoneyManagerScript.playerBalance += 5;
        Debug.Log("hasPlant is: " + playerScript._hasPlant);
        if (playerScript._hasPlant){
            if (Vector2.Distance(player.transform.position, plant.transform.position) < 1.5f){
                distanceIcon.enabled = true;
                if (Input.GetKey(KeyCode.E)){
                    distanceIcon.enabled = false;
                    Debug.Log("Pressing E");
                    plantRenderer.enabled = true;
                    Destroy(_plant);
                }
            }
        }
    }

    void disablePlant(){
        
        if (plantRenderer != null){
            plantRenderer.enabled = false;
        }
    }

    void disableIcons(){
        if (distanceIcon != null){
            distanceIcon.enabled = false;
        }
    }
    
}
