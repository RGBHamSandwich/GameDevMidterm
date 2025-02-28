using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

   public SceneFadeManager sceneFadeManager;
    public void StartTutorial(){
        sceneFadeManager.FadeToScene("TutorialLevel");
     }

     public void StartForest(){
         sceneFadeManager.FadeToScene("ForestLevel");
     }

     public void StartGreenhouse(){
        sceneFadeManager.FadeToScene("Greenhouse");
     }
}