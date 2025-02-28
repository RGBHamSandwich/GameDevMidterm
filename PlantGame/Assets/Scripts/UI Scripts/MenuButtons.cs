using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject MenuPanel;
    public SceneFadeManager sceneFadeManager;

    public void ShowLevelPanel()
     {
        sceneFadeManager.FadeToScene("LevelSelect");
     }
 
     
}
