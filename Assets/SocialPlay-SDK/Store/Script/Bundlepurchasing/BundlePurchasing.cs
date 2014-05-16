using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BundlePurchasing : MonoBehaviour {

    public GameObject PurchaseConfirmationWindow;

    public PurchaseButtonDisplay creditPurchaseButton;
    public PurchaseButtonDisplay coinPurchaseButton;

    public GameObject bundleItemDisplayPrefab;

    public UILabel BundleName;
    public UILabel CreditAmount;
    public UILabel CoinAmount;

    public UIGrid BundleDisplayGrid;
    public Transform targetScroll;
    public UIPanel clippedPanel;

    public int purchaseContainerLocation = 0;

    ItemBundle currentItemBundle;

    List<GameObject> bundleObjects = new List<GameObject>();

    public void ClosePurchaseWindow()
    {
        gameObject.SetActive(false);
    }

    public void SetupBundlePurchaseDetails(ItemBundle bundle)
    {
        ChangePurchaseButtonDisplay(bundle.CreditPrice, bundle.CoinPrice, bundle.State);

        currentItemBundle = bundle;

        BundleName.text = bundle.Name;

        SetUpBundleItemsDisplay(bundle.bundleItems);
    }

    void SetUpBundleItemsDisplay(List<BundleItem> bundleItems)
    {
        ClearGrid(BundleDisplayGrid);

        foreach (BundleItem bundleItem in bundleItems)
        {
            GameObject bundleItemObj = (GameObject)GameObject.Instantiate(bundleItemDisplayPrefab);

            bundleItemObj.transform.parent = BundleDisplayGrid.transform;
            bundleItemObj.transform.localScale = new Vector3(1, 1, 1);
            bundleItemObj.transform.localPosition = new Vector3(0, 0, 0);

            BundleItemInfo bundleInfo = bundleItemObj.GetComponent<BundleItemInfo>();
            bundleInfo.SetupBundleItemDisplay(bundleItem);

            UIDragObject dragobject = bundleItemObj.GetComponent<UIDragObject>();
            dragobject.target = targetScroll;
            dragobject.contentRect = clippedPanel;

            bundleObjects.Add(bundleItemObj);
        }

        BundleDisplayGrid.repositionNow = true;
    }

    private void ChangePurchaseButtonDisplay(int itemCreditCost, int itemCoinCost, int state)
    {
        switch (state)
        {
            case 2:
                creditPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                creditPurchaseButton.SetState(itemCreditCost <= CurrencyBalance.paidCurrency);
                coinPurchaseButton.InsufficientFundsLabel.text = "Credit Purchase Only";
                coinPurchaseButton.SetState(false);

                CreditAmount.text = itemCreditCost.ToString();
                CoinAmount.text = "N/A";
                break;
            case 3:
                coinPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                coinPurchaseButton.SetState(itemCoinCost <= CurrencyBalance.freeCurrency);
                creditPurchaseButton.InsufficientFundsLabel.text = "Coin Purchase Only";
                creditPurchaseButton.SetState(false);

                CreditAmount.text = "N/A";
                CoinAmount.text = itemCoinCost.ToString();
                break;
            case 4:
                CreditAmount.text = "Free";
                CoinAmount.text = "Free";
                coinPurchaseButton.SetState(true);
                creditPurchaseButton.SetState(true);
                break;
            default:
                coinPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                creditPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                coinPurchaseButton.SetState(itemCoinCost <= CurrencyBalance.freeCurrency);
                creditPurchaseButton.SetState(itemCreditCost <= CurrencyBalance.paidCurrency);
                CreditAmount.text = itemCreditCost.ToString();
                CoinAmount.text = itemCoinCost.ToString();
                break;
        }
    }

    private void ClearGrid(UIGrid gridObj)
    {
        gridObj.transform.DetachChildren();

        foreach (GameObject bundleObject in bundleObjects)
        {
            Destroy(bundleObject);
        }

        bundleObjects.Clear();

        gridObj.repositionNow = true;
    }

    public void PurchaseBundleWithCoin()
    {
        WebserviceCalls.webservice.PurchaseItemBundles(ItemSystemGameData.AppID, ItemSystemGameData.UserID, currentItemBundle.ID, "Coins", purchaseContainerLocation, OnReceivedPurchaseCallback);
        ClosePurchaseWindow();
    }

    public void PurchaseBundleWithCredit()
    {
        WebserviceCalls.webservice.PurchaseItemBundles(ItemSystemGameData.AppID, ItemSystemGameData.UserID, currentItemBundle.ID, "Credits", purchaseContainerLocation, OnReceivedPurchaseCallback);
        ClosePurchaseWindow();
    }

    void OnReceivedPurchaseCallback(string data)
    {
        //TODO handle callback for success and error
        PurchaseConfirmationWindow.SetActive(true);
    }

    public void ClosePurchaseStatusWindow()
    {
        PurchaseConfirmationWindow.SetActive(false);
    }
}
