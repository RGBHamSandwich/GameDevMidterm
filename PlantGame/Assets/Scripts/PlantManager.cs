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
        Debug.LogWarning("Duplicate PlantManager detected. Destroying the duplicate.");
        Destroy(gameObject); // Destroy the duplicate
        return; // Exit to avoid further execution
    }
}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    Debug.Log($"Scene loaded: {scene.name}. Restoring plants...");
    Debug.Log($"Enabled Plants: {string.Join(", ", enabledPlants)}");
    RestorePlants();
}

void OnDestroy()
{
    Debug.Log("PlantManager is being destroyed.");
    if (Instance == this)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
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
            foreach (var item in enabledPlants)
            {
                Debug.Log(item.ToString());
            }
        }
    }

    public bool IsPlantEnabled(string plantID)
    {
        return enabledPlants.Contains(plantID);
    }

    public void ClearEnabledPlants()
    {
        enabledPlants.Clear();
        Debug.Log("All enabled plants cleared.");
    }
}