
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
    public GameObject icon;
   

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        plantRenderer = GetComponent<SpriteRenderer>();
        GameObject plant = GetComponent<GameObject>();
        playerScript = player.GetComponent<PlayerInteractPlantScript>();
        distanceIcon = GetComponent<SpriteRenderer>();
        disablePlant();
        //disableIcons();
        //if (distanceIcon != null){
        //    distanceIcon.enabled = false;
        //}
    }

    void Update()
    {
        //checkDistance();
        placePlant();
    }

    void placePlant(){
        //hi this is kendall!! please put the following line in at some point 
        //so that placing the plant also increases the bank balance
        //
        Debug.Log("hasPlant is: " + playerScript._hasPlant);
        if (playerScript._hasPlant){
            if (Vector2.Distance(player.transform.position, plant.transform.position) < 1.5f && Vector2.Distance(player.transform.position, icon.transform.position) <= 1.5f){
                distanceIcon.enabled = true;
                if (Input.GetKey(KeyCode.E)){
                    distanceIcon.enabled = false;
                    Debug.Log("Pressing E");
                    PlayerMoneyManagerScript.playerBalance += 5;
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

    //void checkDistance(){
    //    float distance = Vector2.Distance(player.transform.position, icon.transform.position);

    //    if (distance <= 1.5f){
    //        distanceIcon.enabled = true;
    //    }
    //    else{
    //        distanceIcon.enabled = false;
    //    }    
    //}
    
}
