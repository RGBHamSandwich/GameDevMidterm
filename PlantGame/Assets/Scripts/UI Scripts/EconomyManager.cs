using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;

[System.Serializable]
public class ShopItem{
    public GameObject furniture;
    public int price;
    public String name;
}

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;
    public GameObject ShopUI;
    public GameObject Balance;
    public ShopItem[] ItemsForSale;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            DontDestroyOnLoad(ShopUI);
        }
        else
        {
            Destroy(gameObject); 
            Destroy(ShopUI);
        }
    }

    void Start(){
        Time.timeScale = 1;
        ShopUI.SetActive(false);
        foreach (ShopItem i in ItemsForSale){
           if (i.furniture != null)
            {
                i.furniture.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(ShopUI.activeSelf != false){
                closeShop();
            }
        }
    }

    public void openShop(){
        ShopUI.SetActive(true);
        Time.timeScale = 0;
        UpdateShop();
    }

    public void closeShop(){
        ShopUI.SetActive(false);
        Time.timeScale = 1;
    }

    public int GetPrice(String item){
        int price = 0;
        foreach (ShopItem i in ItemsForSale){
            if (i.name == item){
                price = i.price;
            }
        }
        return price;
    }

    public void Buy(String item){
        ShopItem itemToBuy;
        int initialBalance = PlayerMoneyManagerScript.playerBalance;
        foreach (ShopItem i in ItemsForSale){
            if (i.name == item){
                itemToBuy = i;
                int price = GetPrice(item);
                if (initialBalance >= price){
                    PlayerMoneyManagerScript.playerBalance -= price;
                    PlaceFurniture(itemToBuy);
                }
                break;
            }
        }
        StartCoroutine(CountdownRoutine(initialBalance, PlayerMoneyManagerScript.playerBalance));
        
    }

    private IEnumerator CountdownRoutine(int start, int end)
    {
        AudioManager.instance.HandleMoney();
        float elapsedTime = 0f;
        int current = start;

        while (current > end)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / 1f;
            current = Mathf.RoundToInt(Mathf.Lerp(start, end, t));
            Balance.GetComponent<TMPro.TextMeshProUGUI>().text = current.ToString();
            yield return null; 
        }

        Balance.GetComponent<TMPro.TextMeshProUGUI>().text = end.ToString(); 
        UpdateShop();
    }

    private void PlaceFurniture(ShopItem item){
        item.furniture.SetActive(true);
    }

    private void UpdateShop(){
        Balance.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerMoneyManagerScript.playerBalance.ToString();
        foreach (ShopItem i in ItemsForSale){
            if (PlayerMoneyManagerScript.playerBalance < i.price){
                i.furniture.GetComponent<Button>().interactable = false;
            }
        }
    }

}
