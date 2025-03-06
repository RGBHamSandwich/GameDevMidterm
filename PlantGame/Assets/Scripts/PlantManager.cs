using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance; 

    public List<string> enabledPlants = new List<string>(); 

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        else
        {
            
            Destroy(gameObject); 
            return;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RestorePlants(); 
    }

    void RestorePlants()
    {
        GreenhousePlant[] plants = FindObjectsByType<GreenhousePlant>(FindObjectsSortMode.None);
        foreach (var plant in plants)
        {
            if (enabledPlants.Contains(plant.GetPlantID()))
            {
                plant.EnablePlant(); 
            }
        }
    }

    public void EnablePlant(string plantID)
    {
        if (!enabledPlants.Contains(plantID))
        {
            enabledPlants.Add(plantID); 
        }
    }

    public bool IsPlantEnabled(string plantID)
    {
        return enabledPlants.Contains(plantID);
    }

    public void ClearEnabledPlants()
    {
        enabledPlants.Clear(); 
    }
}