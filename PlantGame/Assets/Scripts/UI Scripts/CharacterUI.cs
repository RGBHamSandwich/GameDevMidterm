using UnityEngine;
using TMPro;
using System.Collections;

public class CharacterUI : MonoBehaviour
{
    public GameObject CharacterTextBox;
    void Start()
    {
       CharacterTextBox.SetActive(false); 
    }
    public void DisplayHandsFullMessage(){
        CharacterTextBox.SetActive(true); 
        CharacterTextBox.GetComponentInChildren<TextMeshProUGUI>().text = "My hands are full! I should put this plant down first";
        StartCoroutine(HideTextAfterSeconds(3f)); 
    }

    public void DisplayTravelMessage(){
        CharacterTextBox.SetActive(true); 
        CharacterTextBox.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to teleport!";
    }

    public void DisplayShopMessage(){
        CharacterTextBox.SetActive(true); 
        CharacterTextBox.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to open shop!";
    }
    
    public IEnumerator HideTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CharacterTextBox.SetActive(false); 
    }

    public void HideTextBox(){
        CharacterTextBox.SetActive(false); 
    }
}
