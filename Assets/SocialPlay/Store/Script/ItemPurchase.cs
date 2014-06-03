using UnityEngine;
using System.Collections;
using System;

public class ItemPurchase : MonoBehaviour
{

    public static event Action<string> OnPurchasedItem;

    StoreItem itemInfo;

    public CurrencyBalance currencyBalance;

    public UILabel itemNameDisplay;
    public UILabel itemCreditCostDisplay;
    public UILabel itemCoinCostDisplay;

    public UILabel itemQuantityAmount;

    public GameObject increaseQuantityButton;
    public GameObject decreaseQuantityButton;

    public PurchaseButtonDisplay creditPurchaseButton;
    public PurchaseButtonDisplay coinPurchaseButton;

    public GameObject purchaseConfirmationPanel;
    public GameObject purchaseConfirmationButton;

    public UITexture itemTexture;

    void OnEnable()
    {
        UIEventListener.Get(increaseQuantityButton).onClick += IncreaseQuantityAmount;
        UIEventListener.Get(decreaseQuantityButton).onClick += DecreaseQuantityAmount;
        UIEventListener.Get(creditPurchaseButton.ActiveButton.gameObject).onClick += PurchaseItemWithCredits;
        UIEventListener.Get(coinPurchaseButton.ActiveButton.gameObject).onClick += PurchaseItemWithCoins;
        UIEventListener.Get(purchaseConfirmationButton).onClick += OnPurchaseConfirmationButtonPressed;

    }

    void OnDisable()
    {
        UIEventListener.Get(increaseQuantityButton).onClick -= IncreaseQuantityAmount;
        UIEventListener.Get(decreaseQuantityButton).onClick -= DecreaseQuantityAmount;
        UIEventListener.Get(creditPurchaseButton.ActiveButton.gameObject).onClick -= PurchaseItemWithCredits;
        UIEventListener.Get(coinPurchaseButton.ActiveButton.gameObject).onClick -= PurchaseItemWithCoins;

    }

    void IncreaseQuantityAmount(GameObject increaseButton)
    {
        int quantityAmount = int.Parse(itemQuantityAmount.text);
        int itemCreditCost = int.Parse(itemCreditCostDisplay.text);
        int itemCoinCost = int.Parse(itemCoinCostDisplay.text);

        itemCreditCost = itemCreditCost / quantityAmount;
        itemCoinCost = itemCoinCost / quantityAmount;

        quantityAmount++;

        ChangeAmountDisplay(quantityAmount, ref itemCreditCost, ref itemCoinCost);


    }

    void DecreaseQuantityAmount(GameObject decreaseButton)
    {
        int quantityAmount = int.Parse(itemQuantityAmount.text);
        int itemCreditCost = int.Parse(itemCreditCostDisplay.text);
        int itemCoinCost = int.Parse(itemCoinCostDisplay.text);

        itemCreditCost = itemCreditCost / quantityAmount;
        itemCoinCost = itemCoinCost / quantityAmount;

        if (quantityAmount > 1)
            quantityAmount--;

        ChangeAmountDisplay(quantityAmount, ref itemCreditCost, ref itemCoinCost);
    }

    private void ChangeAmountDisplay(int quantityAmount, ref int itemCreditCost, ref int itemCoinCost)
    {
        itemCreditCost = itemCreditCost * quantityAmount;
        itemCoinCost = itemCoinCost * quantityAmount;

        itemCreditCostDisplay.text = itemCreditCost.ToString();
        itemCoinCostDisplay.text = itemCoinCost.ToString();
        itemQuantityAmount.text = quantityAmount.ToString();

        ChangePurchaseButtonDisplay(itemCreditCost, itemCoinCost);
    }

    private void ChangePurchaseButtonDisplay(int itemCreditCost, int itemCoinCost)
    {
        coinPurchaseButton.SetState(itemCoinCost <= CurrencyBalance.freeCurrency);
        creditPurchaseButton.SetState(itemCreditCost <= CurrencyBalance.paidCurrency);
    }

    public void DisplayItemPurchasePanel(StoreItem item)
    {
        itemInfo = item;
        itemNameDisplay.text = item.storeItemInfo.itemName;
        itemCreditCostDisplay.text = item.storeItemInfo.creditValue.ToString();
        itemCoinCostDisplay.text = item.storeItemInfo.coinValue.ToString();
        itemQuantityAmount.text = "1";

        itemTexture.mainTexture = item.gameObject.GetComponentInChildren<UITexture>().mainTexture;

        ChangePurchaseButtonDisplay(item.storeItemInfo.creditValue, item.storeItemInfo.coinValue);
    }

    void PurchaseItemWithCredits(GameObject button)
    {
        SP.StoreItemPurchase("http://socialplaywebservice.azurewebsites.net/publicservice.svc/", ItemSystemGameData.UserID, itemInfo.storeItemInfo.itemID, int.Parse(itemQuantityAmount.text), "Credits", currencyBalance.AccessLocation, OnReceivedItemPurchaseConfirmation);
        ClosePanel();
    }

    void PurchaseItemWithCoins(GameObject button)
    {
        SP.StoreItemPurchase("http://socialplaywebservice.azurewebsites.net/publicservice.svc/", ItemSystemGameData.UserID, itemInfo.storeItemInfo.itemID, int.Parse(itemQuantityAmount.text), "Coins", currencyBalance.AccessLocation, OnReceivedItemPurchaseConfirmation);
        ClosePanel();
    }

    void OnReceivedItemPurchaseConfirmation(string msg)
    {
        purchaseConfirmationPanel.SetActive(true);
        currencyBalance.GetCurrencyBalance("");
        ReloadContainerItems();

        if(OnPurchasedItem != null)
            OnPurchasedItem(msg);
    }

    void ReloadContainerItems()
    {
        foreach (LoadItemsForContainer loader in GameObject.FindObjectsOfType(typeof(LoadItemsForContainer)))
        {
            if (loader.sourceLocation == 0)
            {
                loader.transform.parent.GetComponent<ItemContainer>().Clear();
                loader.LoadItems();
            }
        }
    }

    void OnPurchaseConfirmationButtonPressed(GameObject button)
    {
        purchaseConfirmationPanel.SetActive(false);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
