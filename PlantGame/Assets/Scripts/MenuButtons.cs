using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject MenuPanel;
    
    void Start()
    {

        //ShowMenuPanel();
    }

    public void ShowLevelPanel()
     {
        FindObjectOfType<SceneFadeManager>().FadeToScene("Level Select");
     }
 
     
}
