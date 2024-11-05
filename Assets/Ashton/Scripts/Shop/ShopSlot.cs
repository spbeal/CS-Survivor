using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{

    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;

    public GameObject itemToBuy;
    public TextMeshProUGUI buyPriceText;

    void Start()
    {
        itemName.text = itemToBuy.GetComponent<Spawn>().itemName;
        itemImage.sprite = itemToBuy.GetComponent<Image>().sprite;
        buyPriceText.text = itemToBuy.GetComponentInChildren<Spawn>().itemPrice + " Gold";
    }

    public void Buy()
    {
        if (PlayerStats.instance.GetGold() >= itemToBuy.GetComponentInChildren<Spawn>().itemPrice)
        {
            // TODO: give player the weapon;
            Debug.Log("ttttt");
            PlayerStats.instance.SetGold(PlayerStats.instance.GetGold() - itemToBuy.GetComponentInChildren<Spawn>().itemPrice);
        }
        else
        {
            Debug.Log("player does not have enough gold");
        }
    }
}
