using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ShopItem{
    public GameObject furniture;
    public int price;
}

public class economyManager : MonoBehaviour
{
    public ShopItem[] itemsForSale;

    // public int getPrice(String item){
    //     foreach (ShopItem i in itemsForSale){
    //         if (i.CompareTag(item)){

    //         }
    //     }
    // }

    // int balance;
    
    // // public float balance(){
    // //     get { return balance;};
    // //     set { balance = value;};
    // // }

    // // Dictionary<string, int> shopCosts = new Dictionary<string, int>(){
    // //     { "couch", 50 },
    // //     { "bed", 100 },
    // //     { "table", 30 },
    // //     { "chair", 10 },
    // //     { "rug", 20 },
    // //     { "bookshelf", 30 },
    // //     { "toy", 10 },
    // //     { "pet", 100 },
    // //     { "bedside table", 20 },
    // //     { "armchair", 30 },
    // //     { "wallpaper", 50 },
    // //     { "painting", 40}
    // // };

}
