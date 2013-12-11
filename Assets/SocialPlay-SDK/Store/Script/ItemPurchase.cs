using UnityEngine;
using System.Collections;

public class ItemPurchase : MonoBehaviour
{

    ItemInfo itemInfo;

    public CurrencyBalance currencyBalance;

    public UILabel itemNameDisplay;
    public UILabel itemCreditCostDisplay;
    public UILabel itemCoinCostDisplay;

    public UILabel itemQuantityAmount;

    public GameObject increaseQuantityButton;
    public GameObject decreaseQuantityButton;

    public GameObject closePanelButton;

    public PurchaseButtonDisaply creditPurchaseButton;
    public PurchaseButtonDisaply coinPurchaseButton;

    public GameObject purchaseConfirmationPanel;
    public GameObject purchaseConfirmationButton;

    public UITexture itemTexture;

    WebserviceCalls webserviceCalls;

    void Start()
    {
        webserviceCalls = GameObject.Find("Socialplay").GetComponent<WebserviceCalls>();
    }

    void OnEnable()
    {
        UIEventListener.Get(increaseQuantityButton).onClick += IncreaseQuantityAmount;
        UIEventListener.Get(decreaseQuantityButton).onClick += DecreaseQuantityAmount;
        UIEventListener.Get(creditPurchaseButton.ActiveButton.gameObject).onClick += PurchaseItemWithCredits;
        UIEventListener.Get(coinPurchaseButton.ActiveButton.gameObject).onClick += PurchaseItemWithCoins;
        UIEventListener.Get(closePanelButton).onClick += ClosePanel;
        UIEventListener.Get(purchaseConfirmationButton).onClick += OnPurchaseConfirmationButtonPressed;

    }

    void OnDisable()
    {
        UIEventListener.Get(increaseQuantityButton).onClick -= IncreaseQuantityAmount;
        UIEventListener.Get(decreaseQuantityButton).onClick -= DecreaseQuantityAmount;
        UIEventListener.Get(creditPurchaseButton.ActiveButton.gameObject).onClick -= PurchaseItemWithCredits;
        UIEventListener.Get(coinPurchaseButton.ActiveButton.gameObject).onClick -= PurchaseItemWithCoins;
        UIEventListener.Get(closePanelButton).onClick -= ClosePanel;

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
        //if (itemCoinCost > CurrencyBalance.freeCurrency)
        //{
        //    coinPurchaseButton.GetComponent<UIButton>().isEnabled = false;
        //    coinPurchaseButton.GetComponent<UIButton>().UpdateColor(false, true);
        //}
        //else
        //{
        //    coinPurchaseButton.GetComponent<UIButton>().isEnabled = true;
        //    coinPurchaseButton.GetComponent<UIButton>().UpdateColor(true, true);
        //}

        //if (itemCreditCost > CurrencyBalance.paidCurrency)
        //{
        //    creditPurchaseButton.GetComponent<UIButton>().isEnabled = false;
        //    creditPurchaseButton.GetComponent<UIButton>().UpdateColor(false, true);
        //}
        //else
        //{
        //    creditPurchaseButton.GetComponent<UIButton>().isEnabled = true;
        //    creditPurchaseButton.GetComponent<UIButton>().UpdateColor(true, true);
        //}
    }

    public void DisplayItemPurchasePanel(ItemInfo item)
    {
        itemInfo = item;
        itemNameDisplay.text = item.itemName;
        itemCreditCostDisplay.text = item.creditValue.ToString();
        itemCoinCostDisplay.text = item.coinValue.ToString();
        itemQuantityAmount.text = "1";

        itemTexture.mainTexture = item.gameObject.GetComponentInChildren<UITexture>().mainTexture;

        ChangePurchaseButtonDisplay(item.creditValue, item.coinValue);
    }

    void PurchaseItemWithCredits(GameObject button)
    {
        webserviceCalls.StoreItemPurchase("http://socialplaywebservice.azurewebsites.net/publicservice.svc/", ItemSystemGameData.UserID, itemInfo.itemID, int.Parse(itemQuantityAmount.text), "Credits", ItemSystemGameData.AppID, currencyBalance.AccessLocation, OnReceivedItemPurchaseConfirmation);
        ClosePanel(null);
    }

    void PurchaseItemWithCoins(GameObject button)
    {
        webserviceCalls.StoreItemPurchase("http://socialplaywebservice.azurewebsites.net/publicservice.svc/", ItemSystemGameData.UserID, itemInfo.itemID, int.Parse(itemQuantityAmount.text), "Coins", ItemSystemGameData.AppID, currencyBalance.AccessLocation, OnReceivedItemPurchaseConfirmation);
        ClosePanel(null);
    }

    void OnReceivedItemPurchaseConfirmation(string msg)
    {
        purchaseConfirmationPanel.SetActive(true);
        currencyBalance.GetCurrencyBalance("");
        ReloadContainerItems();
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

    void ClosePanel(GameObject button)
    {
        gameObject.SetActive(false);
    }
}
