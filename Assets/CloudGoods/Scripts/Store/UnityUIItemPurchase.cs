using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityUIItemPurchase : MonoBehaviour {

    public static event Action<string> OnPurchasedItem;

    UnityUIStoreItem itemInfo;

    public Text itemNameDisplay;
    public Text itemCreditCostDisplay;
    public Text itemCoinCostDisplay;
    public Text itemDetailsDisplay;

    public Text itemQuantityAmount;

    public GameObject increaseQuantityButton;
    public GameObject decreaseQuantityButton;

    public UnityUIPurchaseButtonDisplay PremiumCurrencyPurchaseButton;
    public UnityUIPurchaseButtonDisplay StandardCurrencyPurchaseButton;

    public RawImage itemTexture;

    public void IncreaseQuantityAmount()
    {
        int quantityAmount = int.Parse(itemQuantityAmount.text);
        int itemCreditCost = int.Parse(itemCreditCostDisplay.text);
        int itemCoinCost = int.Parse(itemCoinCostDisplay.text);

        if (itemCreditCost >= 0)
            itemCreditCost = itemCreditCost / quantityAmount;

        if (itemCoinCost >= 0)
            itemCoinCost = itemCoinCost / quantityAmount;

        quantityAmount++;

        ChangeAmountDisplay(quantityAmount, ref itemCreditCost, ref itemCoinCost);


    }

    public void DecreaseQuantityAmount()
    {
        int quantityAmount = int.Parse(itemQuantityAmount.text);
        int itemCreditCost = int.Parse(itemCreditCostDisplay.text);
        int itemCoinCost = int.Parse(itemCoinCostDisplay.text);

        if (itemCreditCost >= 0)
            itemCreditCost = itemCreditCost / quantityAmount;

        if (itemCoinCost >= 0)
            itemCoinCost = itemCoinCost / quantityAmount;

        if (quantityAmount > 1)
            quantityAmount--;

        ChangeAmountDisplay(quantityAmount, ref itemCreditCost, ref itemCoinCost);
    }

    private void ChangeAmountDisplay(int quantityAmount, ref int itemCreditCost, ref int itemCoinCost)
    {
        if (itemCreditCost >= 0)
            itemCreditCost = itemCreditCost * quantityAmount;
        else
            itemCreditCost = -1;

        if (itemCoinCost >= 0)
            itemCoinCost = itemCoinCost * quantityAmount;
        else
            itemCoinCost = -1;

        itemCreditCostDisplay.text = itemCreditCost.ToString();
        itemCoinCostDisplay.text = itemCoinCost.ToString();

        itemQuantityAmount.text = quantityAmount.ToString();

        ChangePurchaseButtonDisplay(itemCreditCost, itemCoinCost);
    }

    private void ChangePurchaseButtonDisplay(int itemCreditCost, int itemCoinCost)
    {
        StandardCurrencyPurchaseButton.SetState(itemCoinCost);
        PremiumCurrencyPurchaseButton.SetState(itemCreditCost);
    }

    public void DisplayItemPurchasePanel(UnityUIStoreItem item)
    {
        itemInfo = item;
        itemNameDisplay.text = item.storeItem.itemName;
        itemCreditCostDisplay.text = item.storeItem.premiumCurrencyValue.ToString();
        itemCoinCostDisplay.text = item.storeItem.standardCurrencyValue.ToString();
        itemQuantityAmount.text = "1";
        SetItemDetailDisplay(item);

        itemTexture.texture = item.gameObject.GetComponentInChildren<RawImage>().texture;

        ChangePurchaseButtonDisplay(item.storeItem.premiumCurrencyValue, item.storeItem.standardCurrencyValue);
    }

    void SetItemDetailDisplay(UnityUIStoreItem storeItem)
    {
        string statusText = "";

        foreach (StoreItemDetail detail in storeItem.storeItem.itemDetail)
        {
            statusText += detail.propertyName + " : " + detail.propertyValue + "\n";
        }

        itemDetailsDisplay.text = statusText;
    }

    public void PurchaseItemWithPremiumCurrency()
    {
        Debug.Log(int.Parse(itemQuantityAmount.text));
        CloudGoods.StoreItemPurchase(itemInfo.storeItem.itemID, int.Parse(itemQuantityAmount.text), CurrencyType.Premium, 0, OnReceivedItemPurchaseConfirmation);
        ClosePanel();
    }

    public void PurchaseItemWithStandardCurrency()
    {
        Debug.Log(int.Parse(itemQuantityAmount.text));
        CloudGoods.StoreItemPurchase(itemInfo.storeItem.itemID, int.Parse(itemQuantityAmount.text), CurrencyType.Standard, 0, OnReceivedItemPurchaseConfirmation);
        ClosePanel();
    }

    void OnReceivedItemPurchaseConfirmation(string msg)
    {
        ReloadContainerItems();

        if (OnPurchasedItem != null)
            OnPurchasedItem(msg);
    }

    void ReloadContainerItems()
    {
        foreach (PersistentItemContainer loader in GameObject.FindObjectsOfType(typeof(PersistentItemContainer)))
        {
            if (loader.Location == 0)
            {
                loader.transform.parent.GetComponent<ItemContainer>().Clear();
                loader.LoadItems();
            }
        }
    }


    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
