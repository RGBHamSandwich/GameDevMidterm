using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;

[System.Serializable]
public class PlantForSale{
    public GameObject plant;
    public int price;
    public String name;
}

public class PlantSeller : MonoBehaviour
{
    public GameObject balance;
    public PlantForSale[] plants;

    void Start()
    {
        updateBalance();
    }

    public int GetPrice(String item){
        int price = 0;
        foreach (PlantForSale i in plants){
            if (i.name == item){
                price = i.price;
            }
        }
        return price;
    }

    public void Sell(String item){
        PlantForSale plantToSell;
        foreach (PlantForSale i in plants){
            if (i.name == item){
                plantToSell = i;
                int price = plantToSell.price;
                PlayerMoneyManagerScript.playerBalance += price;
                break;
            }
        }
        updateBalance();
    }

    private void updateBalance(){
        balance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = PlayerMoneyManagerScript.playerBalance.ToString();
    }

}
