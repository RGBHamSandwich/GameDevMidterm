using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject PausePanel;
    public GameObject ConfirmPanel;
    
    void Start()
    {
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
        PausePanel.SetActive(true);
        ConfirmPanel.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown("escape"))
        {
            ShowMenuPanel();
        }
    }

    public void ShowMenuPanel()
     {
         MenuPanel.SetActive(true);
         PausePanel.SetActive(false);
         ConfirmPanel.SetActive(false);
         Time.timeScale = 0;
     }

     public void exit(){
        Application.Quit();
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
        Time.timeScale = 1;
     }
}
