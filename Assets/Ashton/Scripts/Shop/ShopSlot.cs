using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{

    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemPrice;

    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    public PlayerStats playerStats;

    void Start()
    {
        itemIconImage.sprite = itemIcon;
        itemNameText.text = itemName;
        if (itemDescriptionText != null)
        {
            itemDescriptionText.text = itemDescription;
        }
        itemPriceText.text = "Price: " + itemPrice.ToString();
    }

    public void Buy(GameObject button)
    {
        Debug.Log("buy_click 1");
        if (playerStats.GetGold() >= itemPrice /*pass the price of the item somehow?*/)
        {
            Debug.Log("buy_click 2");
            playerStats.SetGold(playerStats.GetGold() - itemPrice);
            switch (button.tag)
            {
                case "Buy_Booberry":
                    /* Damage++ */
                    Debug.Log("Buy clicked -- booberry");
                    break;
                case "Buy_PC":
                    /* MagSize++ */
                    Debug.Log("Buy clicked -- pc");
                    break;
                case "Buy_VideaGames":
                    /* ReloadSpeed++ */
                    Debug.Log("Buy clicked -- videagames");
                    break;
                case "Buy_Soda":
                    /* hp++ */
                    Debug.Log("Buy clicked -- soda");
                    break;
                case "Buy_Aries":
                    /* speed++ */
                    Debug.Log("Buy clicked -- aries");
                    break;
                case "Buy_C_Book":
                    /* gold_rate++ */
                    Debug.Log("Buy clicked -- c book");
                    break;
                case "Buy_Shotgun":
                    /* give player the shotgun */
                    Debug.Log("Buy clicked -- shotgun");
                    break;
                default:
                    playerStats.SetGold(playerStats.GetGold() + itemPrice);
                    Debug.Log("Button tag not recognized.");
                    break;
            }
        }
        else
        {
            Debug.Log("player does not have enough gold");
        }
    }
}
