using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject LevelSelectPanel;
    
    void Start()
    {
        MenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ShowLevelPanel()
     {
         MenuPanel.SetActive(false);
         LevelSelectPanel.SetActive(true);
     }
 
    public void ShowMenuPanel()
     {
         MenuPanel.SetActive(true);
         LevelSelectPanel.SetActive(false);
     }

     public void startTutorial(){
        SceneManager.LoadScene("Tutorial Level");
     }

     public void startForest(){
        SceneManager.LoadScene("Game");
     }
}
