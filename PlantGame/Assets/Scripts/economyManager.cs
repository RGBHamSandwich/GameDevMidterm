using UnityEngine;
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

public class economyManager : MonoBehaviour
{
    public GameObject balance;
    public ShopItem[] itemsForSale;

    void Start()
    {
        updateBalance();
        foreach (ShopItem i in itemsForSale){
            i.furniture.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = i.price.ToString();
        }
    }

    public int GetPrice(String item){
        int price = 0;
        foreach (ShopItem i in itemsForSale){
            if (i.name == item){
                price = i.price;
            }
        }
        return price;
    }

    public void Buy(String item){
        ShopItem itemToBuy;
        foreach (ShopItem i in itemsForSale){
            if (i.name == item){
                itemToBuy = i;
                int price = GetPrice(item);
                if (PlayerMoneyManagerScript.playerBalance >= price){
                    PlayerMoneyManagerScript.playerBalance -= price;
                    PlaceFurniture(itemToBuy);
                }
                else {
                    Debug.Log("not enough money");
                }
                break;
            }
        }
        updateBalance();
    }

    private void PlaceFurniture(ShopItem item){
        // item.furniture.SetActive(true);
    }

    private void updateBalance(){
        balance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = PlayerMoneyManagerScript.playerBalance.ToString();
    }

}
