using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UnityUIBundlePurchasing : MonoBehaviour {

    public static Action<string> OnPurchaseSuccessful;

    public UnityUIPurchaseButtonDisplay PremiumCurrencyPurchaseButton;
    public UnityUIPurchaseButtonDisplay StandardCurrencyPurchaseButton;

    public GameObject bundleItemDisplayPrefab;

    public Text BundleName;
    public Text PremiumCurrencyAmount;
    public Text StandardCurrencyAmount;

    public int purchaseContainerLocation = 0;

    public ItemBundle currentItemBundle;

    List<GameObject> bundleObjects = new List<GameObject>();

    public GameObject bundleGrid;

    //public GameObject bundleGrid;

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
        ClearGrid(bundleGrid);

        foreach (BundleItem bundleItem in bundleItems)
        {
            GameObject bundleItemObj = (GameObject)GameObject.Instantiate(bundleItemDisplayPrefab);

            bundleItemObj.transform.parent = bundleGrid.transform;

            UnityUIBundleItemInfo bundleInfo = bundleItemObj.GetComponent<UnityUIBundleItemInfo>();
            bundleInfo.SetupBundleItemDisplay(bundleItem);

            bundleObjects.Add(bundleItemObj);
        }
    }

    private void ChangePurchaseButtonDisplay(int itemCreditCost, int itemCoinCost, CloudGoodsBundle state)
    {
        switch (state)
        {
            case CloudGoodsBundle.CreditPurchasable:
                PremiumCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                PremiumCurrencyPurchaseButton.SetState(itemCreditCost);
                StandardCurrencyPurchaseButton.InsufficientFundsLabel.text = "Credit Purchase Only";
                StandardCurrencyPurchaseButton.SetState(-1);

                PremiumCurrencyAmount.text = itemCreditCost.ToString();
                StandardCurrencyAmount.text = "N/A";
                break;
            case CloudGoodsBundle.CoinPurchasable:
                StandardCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                StandardCurrencyPurchaseButton.SetState(itemCoinCost);
                PremiumCurrencyPurchaseButton.InsufficientFundsLabel.text = "Coin Purchase Only";
                PremiumCurrencyPurchaseButton.SetState(-1);

                PremiumCurrencyAmount.text = "N/A";
                StandardCurrencyAmount.text = itemCoinCost.ToString();
                break;
            case CloudGoodsBundle.Free:
                PremiumCurrencyAmount.text = "Free";
                StandardCurrencyAmount.text = "Free";
                StandardCurrencyPurchaseButton.SetState(0);
                PremiumCurrencyPurchaseButton.SetState(0);
                break;
            default:
                StandardCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                PremiumCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                StandardCurrencyPurchaseButton.SetState(itemCoinCost);
                PremiumCurrencyPurchaseButton.SetState(itemCreditCost);
                PremiumCurrencyAmount.text = itemCreditCost.ToString();
                StandardCurrencyAmount.text = itemCoinCost.ToString();
                break;
        }
    }

    private void ClearGrid(GameObject gridObj)
    {
        gridObj.transform.DetachChildren();

        foreach (GameObject bundleObject in bundleObjects)
        {
            Destroy(bundleObject);
        }

        bundleObjects.Clear();
    }

    public void PurchaseBundleWithStandardCurrency()
    {
        CloudGoods.PurchaseItemBundles(currentItemBundle.ID, CurrencyType.Standard, purchaseContainerLocation, OnReceivedPurchaseCallback);
        ClosePurchaseWindow();
    }

    public void PurchaseBundleWithPremiumCurrency()
    {
        CloudGoods.PurchaseItemBundles(currentItemBundle.ID, CurrencyType.Premium, purchaseContainerLocation, OnReceivedPurchaseCallback);
        ClosePurchaseWindow();
    }

    void OnReceivedPurchaseCallback(string data)
    {
        Debug.Log("OnReceivedPurchaseCallback " + data);

        if (OnPurchaseSuccessful != null)
            OnPurchaseSuccessful(data);
    }
}
