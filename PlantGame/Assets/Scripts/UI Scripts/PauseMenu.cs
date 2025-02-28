using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject PausePanel;
    public GameObject ConfirmPanel;

    public GameObject ControlsPanel;
    
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
         MenuPanel.SetActive(true);
         PausePanel.SetActive(false);
         ConfirmPanel.SetActive(false);
         ControlsPanel.SetActive(false);
         Time.timeScale = 0;
     }

     public void exit(){
        SceneManager.LoadScene("OpeningMenu");
     }


    public void AreYouSure(){
        ConfirmPanel.SetActive(true);
    }

    public void notSure(){
        ConfirmPanel.SetActive(false);
    }

     public void close(){
        MenuPanel.SetActive(false);
        PausePanel.SetActive(true);
        ConfirmPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        Time.timeScale = 1;
     }

     public void openControls(){
        ControlsPanel.SetActive(true);
     }

     public void closeControls(){
        ControlsPanel.SetActive(false);
     }
}
