using System;
using JetBrains.Annotations;
using PlantGame.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class GreenhousePlant : MonoBehaviour
{
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
        playerScript = player.GetComponent<PlayerInteractPlantScript>();
        plantID = GeneratePlantID(); // Generate a unique ID for the plant
        DisableAllIcons();
    }

    void Update()
    {
        PlacePlant();
    }

    void PlacePlant()
    {
        float distancetoIcon = Vector2.Distance(player.transform.position, icon.transform.position);
        if (distancetoIcon <= 1.5f)
        {
            distanceIcon.enabled = true;
            if (playerScript._hasPlant && Input.GetKeyDown(KeyCode.E))
            {
                distanceIcon.enabled = false;
                plantRenderer.enabled = true;
                IncreasePlayerBalance();
                PlantManager.Instance.EnablePlant(plantID);
                
                if (playerScript._plant != null)
                {
                    Destroy(playerScript._plant);
                    playerScript._hasPlant = false;
                }
            }
        }
        else
        {
            distanceIcon.enabled = false;
        }
    }

    void DisableAllIcons()
    {
        if (plantRenderer != null)
        {
            plantRenderer.enabled = false;
        }

        //if (distanceIcon != null)
        //{
        //    distanceIcon.enabled = false;
        //}
    }

    void IncreasePlayerBalance()
    {
        int initialBalance = PlayerMoneyManagerScript.playerBalance;
        PlayerMoneyManagerScript.playerBalance += 5;

        EconomyManager economyManager;
        economyManager = FindFirstObjectByType<EconomyManager>();

        if (economyManager != null)
        {
            StartCoroutine(economyManager.CountupRoutine(initialBalance, PlayerMoneyManagerScript.playerBalance));
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

    public string GetPlantID()
    {
        return plantID;
    }

    private string GeneratePlantID()
    {
        return $"{gameObject.name}";
    }

    public void EnablePlant()
{
    Debug.Log($"Enabling plant visually: {plantID}");
    if (plantRenderer != null)
    {
        plantRenderer.enabled = true;
    }
}
}