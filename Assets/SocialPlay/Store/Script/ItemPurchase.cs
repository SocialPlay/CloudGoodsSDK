using UnityEngine;
using System.Collections;
using System;

public class ItemPurchase : MonoBehaviour
{

    public static event Action<string> OnPurchasedItem;

	UIStoreItem itemInfo;

    public UILabel itemNameDisplay;
    public UILabel itemCreditCostDisplay;
    public UILabel itemCoinCostDisplay;

    public UILabel itemQuantityAmount;

    public GameObject increaseQuantityButton;
    public GameObject decreaseQuantityButton;

    public PurchaseButtonDisplay creditPurchaseButton;
    public PurchaseButtonDisplay coinPurchaseButton;

    public UITexture itemTexture;

    void OnEnable()
    {
        UIEventListener.Get(increaseQuantityButton).onClick += IncreaseQuantityAmount;
        UIEventListener.Get(decreaseQuantityButton).onClick += DecreaseQuantityAmount;
        UIEventListener.Get(creditPurchaseButton.ActiveButton.gameObject).onClick += PurchaseItemWithCredits;
        UIEventListener.Get(coinPurchaseButton.ActiveButton.gameObject).onClick += PurchaseItemWithCoins;

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
        coinPurchaseButton.SetState(itemCoinCost <= SP.freeCurrency);
        creditPurchaseButton.SetState(itemCreditCost <= SP.paidCurrency);
    }

    public void DisplayItemPurchasePanel(UIStoreItem item)
    {
        itemInfo = item;
		itemNameDisplay.text = item.storeItem.itemName;
		itemCreditCostDisplay.text = item.storeItem.creditValue.ToString();
        itemCoinCostDisplay.text = item.storeItem.coinValue.ToString();
        itemQuantityAmount.text = "1";

        itemTexture.mainTexture = item.gameObject.GetComponentInChildren<UITexture>().mainTexture;

        ChangePurchaseButtonDisplay(item.storeItem.creditValue, item.storeItem.coinValue);
    }

    void PurchaseItemWithCredits(GameObject button)
    {
        SP.StoreItemPurchase(itemInfo.storeItem.itemID, int.Parse(itemQuantityAmount.text), CurrencyType.Credits, 0, OnReceivedItemPurchaseConfirmation);
        ClosePanel();
    }

    void PurchaseItemWithCoins(GameObject button)
    {
		SP.StoreItemPurchase(itemInfo.storeItem.itemID, int.Parse(itemQuantityAmount.text), CurrencyType.Coins, 0, OnReceivedItemPurchaseConfirmation);
        ClosePanel();
    }
	
    void OnReceivedItemPurchaseConfirmation(string msg)
    {
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


    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
