using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;

[System.Serializable]
public class ShopItem{
    public GameObject furniture;
    public GameObject button;
    public int price;
    public String name;
    public bool AlreadyPurchased = false;
}

public class EconomyManager : MonoBehaviour
{
    public GameObject BalanceOnScreen;
    public static EconomyManager instance;
    public GameObject ShopUI;
    public GameObject BalanceInShop;
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
           if (i.furniture != null && i.AlreadyPurchased == false)
            {
                i.furniture.SetActive(false);
            }
        }
        BalanceOnScreen.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerMoneyManagerScript.playerBalance.ToString(); 
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
                    i.AlreadyPurchased = true;
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
            BalanceInShop.GetComponent<TMPro.TextMeshProUGUI>().text = current.ToString();
            yield return null; 
        }

        BalanceInShop.GetComponent<TMPro.TextMeshProUGUI>().text = end.ToString(); 
        UpdateShop();
    }

    public IEnumerator CountupRoutine(int start, int end)
    {
        AudioManager.instance.HandleMoney();
        float elapsedTime = 0f;
        int current = start;

        while (current < end)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 1f;
            current = Mathf.RoundToInt(Mathf.Lerp(start, end, t));
            BalanceOnScreen.GetComponent<TMPro.TextMeshProUGUI>().text = current.ToString();
            yield return null; 
        }

        BalanceOnScreen.GetComponent<TMPro.TextMeshProUGUI>().text = end.ToString(); 
    }

    private void PlaceFurniture(ShopItem item){
        item.furniture.SetActive(true);
    }

    private void UpdateShop(){
        BalanceInShop.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerMoneyManagerScript.playerBalance.ToString();
        foreach (ShopItem i in ItemsForSale){
            if (PlayerMoneyManagerScript.playerBalance < i.price){
                i.button.GetComponent<Button>().interactable = false;
            }
            if (i.AlreadyPurchased==true){
                i.button.GetComponent<Button>().interactable = false;
            }
        }
    }

}
