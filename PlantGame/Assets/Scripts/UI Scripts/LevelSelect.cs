using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

   public SceneFadeManager sceneFadeManager;
    public void StartTutorial(){
         AudioManager.instance.HandleButtonClick();
         sceneFadeManager.FadeToScene("TutorialLevel");
     }

     public void StartForest(){
         AudioManager.instance.HandleButtonClick();
         sceneFadeManager.FadeToScene("ForestLevel");
     }

     public void StartGreenhouse(){
         AudioManager.instance.HandleButtonClick();
         sceneFadeManager.FadeToScene("Greenhouse");
     }
}