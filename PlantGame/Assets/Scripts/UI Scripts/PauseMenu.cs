using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject PausePanel;
    public GameObject ConfirmPanel;

    public GameObject ControlsPanel;
    public SceneFadeManager sceneFadeManager;
    
    void Start()
    {
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
        PausePanel.SetActive(true);
        ConfirmPanel.SetActive(false);
        ControlsPanel.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown("escape"))
        {
            if(MenuPanel.activeSelf == false){
                ShowMenuPanel();
            }
            else{
                close();
            }
        }
    }

    public void ShowMenuPanel()
     {
        AudioManager.instance.HandleButtonClick();
         MenuPanel.SetActive(true);
         PausePanel.SetActive(false);
         ConfirmPanel.SetActive(false);
         ControlsPanel.SetActive(false);
         Time.timeScale = 0;
     }

     public void exit(){
        AudioManager.instance.HandleButtonClick();
        ConfirmPanel.SetActive(false);
        PausePanel.SetActive(false);
        sceneFadeManager.FadeToScene("OpeningMenu");
     }


    public void AreYouSure(){
        AudioManager.instance.HandleButtonClick();
        ConfirmPanel.SetActive(true);
    }

    public void notSure(){
        AudioManager.instance.HandleButtonClick();
        ConfirmPanel.SetActive(false);
    }

     public void close(){
        AudioManager.instance.HandleButtonClick();
        MenuPanel.SetActive(false);
        PausePanel.SetActive(true);
        ConfirmPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        Time.timeScale = 1;
     }

     public void openControls(){
        AudioManager.instance.HandleButtonClick();
        ControlsPanel.SetActive(true);
     }

     public void closeControls(){
        AudioManager.instance.HandleButtonClick();
        ControlsPanel.SetActive(false);
     }
}
