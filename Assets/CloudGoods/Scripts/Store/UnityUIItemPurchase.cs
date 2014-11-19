using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityUIItemPurchase : MonoBehaviour {

    public static event Action<string> OnPurchasedItem;

    UnityUIStoreItem itemInfo;

    public Text itemNameDisplay;
    public Text itemDetailsDisplay;

    public Text itemQuantityAmount;

    public GameObject increaseQuantityButton;
    public GameObject decreaseQuantityButton;

    public GameObject PremiumCurrencyHalfWindow;
    public GameObject StandardCurrencyHalfWindow;
    public GameObject PremiumCurrencyFullWindow;
    public GameObject StandardCurrencyFullWindow;

    public RawImage itemTexture;

    int premiumCurrencyCost = 0;
    int standardCurrencyCost = 0;

    public void IncreaseQuantityAmount()
    {
        int quantityAmount = int.Parse(itemQuantityAmount.text);

        if (premiumCurrencyCost >= 0)
            premiumCurrencyCost = premiumCurrencyCost / quantityAmount;

        if (standardCurrencyCost >= 0)
            standardCurrencyCost = standardCurrencyCost / quantityAmount;

        quantityAmount++;

        ChangeAmountDisplay(quantityAmount, ref premiumCurrencyCost, ref standardCurrencyCost);
    }

    public void DecreaseQuantityAmount()
    {
        int quantityAmount = int.Parse(itemQuantityAmount.text);

        if (quantityAmount > 1)
            quantityAmount--;

        ChangeAmountDisplay(quantityAmount, ref premiumCurrencyCost, ref standardCurrencyCost);
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

        itemQuantityAmount.text = quantityAmount.ToString();

        ChangePurchaseButtonDisplay(itemCreditCost, itemCoinCost);
    }

    private void ChangePurchaseButtonDisplay(int itemCreditCost, int itemCoinCost)
    {
        StandardCurrencyFullWindow.SetActive(false);
        StandardCurrencyHalfWindow.SetActive(false);
        PremiumCurrencyFullWindow.SetActive(false);
        PremiumCurrencyHalfWindow.SetActive(false);

        if (itemCreditCost < 0 && itemCreditCost < 0)
        {
            StandardCurrencyFullWindow.SetActive(true);

            UnityUIPurchaseButtonDisplay freeButtonDisplay = StandardCurrencyFullWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
            freeButtonDisplay.SetState(itemCoinCost);
        }
        else if (itemCreditCost < 0)
        {
            StandardCurrencyFullWindow.SetActive(true);

            UnityUIPurchaseButtonDisplay StandardOnlyButtonDisplay = StandardCurrencyFullWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
            StandardOnlyButtonDisplay.SetState(itemCoinCost);
        }
        else if (itemCoinCost < 0)
        {
            PremiumCurrencyFullWindow.SetActive(true);

            UnityUIPurchaseButtonDisplay PremiumOnlyButtonDisplay = PremiumCurrencyFullWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
            PremiumOnlyButtonDisplay.SetState(itemCreditCost);
        }
        else
        {
            PremiumCurrencyHalfWindow.SetActive(true);
            StandardCurrencyHalfWindow.SetActive(true);

            UnityUIPurchaseButtonDisplay PremiumButtonDisplay = PremiumCurrencyHalfWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
            UnityUIPurchaseButtonDisplay StandardButtonDisplay = StandardCurrencyHalfWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
            PremiumButtonDisplay.SetState(itemCreditCost);
            StandardButtonDisplay.SetState(itemCoinCost);
        }
    }

    public void DisplayItemPurchasePanel(UnityUIStoreItem item)
    {
        itemInfo = item;
        itemNameDisplay.text = item.storeItem.itemName;

        premiumCurrencyCost = item.storeItem.premiumCurrencyValue;
        standardCurrencyCost = item.storeItem.standardCurrencyValue;

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
