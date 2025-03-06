using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    public List<string> enabledPlants = new List<string>();

    void Awake()
    {
        Debug.Log($"Enabled Plants: {string.Join(", ", enabledPlants)}");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Restore all enabled plants when the scene is loaded
        RestorePlants();
    }

    void RestorePlants()
    {
        // Find all GreenhousePlant objects in the scene and restore their states
        GreenhousePlant[] plants = FindObjectsOfType<GreenhousePlant>();
        foreach (var plant in plants)
        {
            plant.RestorePlants();
        }
    }

    public void EnablePlant(string plantID)
    {
        if (!enabledPlants.Contains(plantID))
        {
            enabledPlants.Add(plantID);
            Debug.Log($"Plant {plantID} enabled.");
        }
    }

    public bool IsPlantEnabled(string plantID)
    {
        return enabledPlants.Contains(plantID);
    }

    // Clear all enabled plants (optional, for resetting the state)
    public void ClearEnabledPlants()
    {
        enabledPlants.Clear();
        Debug.Log("All enabled plants cleared.");
    }
}