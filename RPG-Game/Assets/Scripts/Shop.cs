using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Shop : MonoBehaviour {

    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public TMP_Text goldText;

    public string[] itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public TMP_Text buyItemName, buyItemDescription, buyItemValue;
    public TMP_Text sellItemName, sellItemDescription, sellItemValue;

    public Button sellButton;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.U) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }
	}

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyItemButtons[0].Press();

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {
        sellItemButtons[0].Press();

        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        ShowSellItems();
        
    }

    private void ShowSellItems()
    {
        GameManager.instance.SortItems();
        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item buyItem)
    {
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = "Value: " + selectedItem.value + "g";
    }

    public void SelectSellItem(Item sellItem)
    {
        if (sellItem != null) // Check if the sellItem is not null
        {
            selectedItem = sellItem;
            sellItemName.text = selectedItem.itemName;
            sellItemDescription.text = selectedItem.description;
            sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "g";

            sellButton.interactable = true; // Enable the sell button
        }
        else
        {
            // Handle the case where there are no items left to sell
            selectedItem = null;
            sellItemName.text = "No Item";
            sellItemDescription.text = "";
            sellItemValue.text = "";
            
            sellButton.interactable = false; // Disable the sell button
        }
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;

                GameManager.instance.AddItem(selectedItem.itemName);
            }
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void SellItem()
    {
        if (selectedItem != null && Array.Exists(GameManager.instance.itemsHeld, item => item == selectedItem.itemName))
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);

            GameManager.instance.RemoveItem(selectedItem.itemName);
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";

        ShowSellItems();
    }
}
