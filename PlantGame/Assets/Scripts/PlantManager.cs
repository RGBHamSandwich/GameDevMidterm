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

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Restore all enabled plants when the scene is loaded
        RestorePlants();
    }

    void RestorePlants()
    {
        // Use FindObjectsByType to get all plants in the scene
        GreenhousePlant[] plants = FindObjectsByType<GreenhousePlant>(FindObjectsSortMode.None);
        foreach (var plant in plants)
        {
            // Check if the plant should be enabled based on the enabledPlants list
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