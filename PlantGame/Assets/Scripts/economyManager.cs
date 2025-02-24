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
    public ShopItem[] itemsForSale;

    void Start()
    {
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

    private void Buy(String item){
        ShopItem itemToBuy;
        foreach (ShopItem i in itemsForSale){
            if (i.name == item){
                itemToBuy = i;
                int price = GetPrice(item);
                if (PlayerMoneyManagerScript.playerBalance >= price){
                    PlayerMoneyManagerScript.playerBalance -= price;
                    PlaceFurniture(itemToBuy);
                }
                break;
            }
        }
    }

    private void PlaceFurniture(ShopItem item){
        item.furniture.SetActive(true);
    }

}
