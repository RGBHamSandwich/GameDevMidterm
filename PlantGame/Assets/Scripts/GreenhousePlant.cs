
using System;
using JetBrains.Annotations;
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
    private string plantID;
   

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //plantRenderer = GetComponent<SpriteRenderer>();
        //GameObject plant = GetComponent<GameObject>();
        playerScript = player.GetComponent<PlayerInteractPlantScript>();
        //distanceIcon = GetComponent<SpriteRenderer>();
        plantID = gameObject.name;
        RestorePlants();
        disableAllIcons();
    }

    void Update()
    {
        placePlant();
    }

    void placePlant(){

        //float distancetoPlant = Vector2.Distance(player.transform.position, plant.transform.position);
        float distancetoIcon = Vector2.Distance(player.transform.position, icon.transform.position);
        // Debug.Log("hasPlant is: " + playerScript._hasPlant);
        if (distancetoIcon <= 1.5f){
            distanceIcon.enabled = true;
            if (playerScript._hasPlant && Input.GetKeyDown(KeyCode.E)){
                distanceIcon.enabled = false;
                // Debug.Log("Pressing E");
                plantRenderer.enabled = true;
                IncreasePlayerBalance();
                
                if (playerScript._plant != null){
                    Destroy(playerScript._plant);
                    playerScript._hasPlant = false;
                    
                    PlantManager.Instance.enabledPlants.Add(plantID);
                }
            }
        }
        else {
            distanceIcon.enabled = false;
        }
        
    }

    void disableAllIcons(){
        
        if (plantRenderer != null){
            plantRenderer.enabled = false;
        }

        if (distanceIcon != null){
            distanceIcon.enabled = false;
        }
    }
    
    void IncreasePlayerBalance(){
        int initialBalance = PlayerMoneyManagerScript.playerBalance;
        PlayerMoneyManagerScript.playerBalance += 5;

        
        EconomyManager economyManager;
        economyManager = FindFirstObjectByType<EconomyManager>();

        if (economyManager != null)
        {
            StartCoroutine(economyManager.CountupRoutine(initialBalance,  PlayerMoneyManagerScript.playerBalance));
        }
    }

    public void RestorePlants()
    {
        if (PlantManager.Instance.IsPlantEnabled(plantID))
        {
            plantRenderer.enabled = true;
        }
        else
        {
            plantRenderer.enabled = false;
        }
    }
}
