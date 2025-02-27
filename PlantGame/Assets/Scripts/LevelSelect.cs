using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public void startTutorial(){
        Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene("Tutorial Level", LoadSceneMode.Single);
        FindObjectOfType<SceneFadeManager>().FadeToScene("Tutorial Level");
     }

     public void startForest(){
        Scene currentScene = SceneManager.GetActiveScene();
         //SceneManager.LoadScene("ForestLevel", LoadSceneMode.Single);
         FindObjectOfType<SceneFadeManager>().FadeToScene("ForestLevel");
     }

     public void startGreenhouse(){
        Scene currentScene = SceneManager.GetActiveScene();
      //   SceneManager.LoadScene("greenhouse", LoadSceneMode.Single);
        FindObjectOfType<SceneFadeManager>().FadeToScene("greenhouse");
     }
}