using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance; // Singleton instance

    public List<string> enabledPlants = new List<string>(); // List of enabled plants

    void Awake()
    {
        Debug.Log("PlantManager Awake called.");

        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
        else
        {
            Debug.LogWarning("Duplicate PlantManager detected. Destroying the duplicate.");
            Destroy(gameObject); // Destroy the duplicate
            return; // Exit to avoid further execution
        }
    }

    void OnDestroy()
    {
        Debug.Log("PlantManager is being destroyed.");
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}. Restoring plants...");
        Debug.Log($"Enabled Plants: {string.Join(", ", enabledPlants)}");
        RestorePlants(); // Restore plants in the new scene
    }

    void RestorePlants()
    {
        Debug.Log("Restoring plants...");
        GreenhousePlant[] plants = FindObjectsByType<GreenhousePlant>(FindObjectsSortMode.None);
        foreach (var plant in plants)
        {
            Debug.Log($"Checking plant: {plant.GetPlantID()}");
            if (enabledPlants.Contains(plant.GetPlantID()))
            {
                Debug.Log($"Enabling plant: {plant.GetPlantID()}");
                plant.EnablePlant(); // Enable the plant visually
            }
        }
    }

    public void EnablePlant(string plantID)
    {
        if (!enabledPlants.Contains(plantID))
        {
            enabledPlants.Add(plantID); // Add the plant ID to the enabled list
            Debug.Log($"Plant {plantID} enabled.");
            foreach (var item in enabledPlants)
            {
                Debug.Log(item.ToString()); // Log all enabled plants
            }
        }
    }

    public bool IsPlantEnabled(string plantID)
    {
        return enabledPlants.Contains(plantID); // Check if a plant is enabled
    }

    public void ClearEnabledPlants()
    {
        enabledPlants.Clear(); // Clear the list of enabled plants
        Debug.Log("All enabled plants cleared.");
    }
}