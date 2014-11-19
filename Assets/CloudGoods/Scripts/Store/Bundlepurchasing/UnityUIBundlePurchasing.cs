using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UnityUIBundlePurchasing : MonoBehaviour {

    public static Action<string> OnPurchaseSuccessful;

    public GameObject PremiumCurrencyPurchaseWindowHalf;
    public GameObject PremiumCurrencyPurchaseWindowFull;
    public GameObject StandardCurrencyPurchaseWindowHalf;
    public GameObject StandardCurrencyPurchaseWindowFull;

    public GameObject bundleItemDisplayPrefab;

    public Text BundleName;

    public int purchaseContainerLocation = 0;

    public ItemBundle currentItemBundle;

    List<GameObject> bundleObjects = new List<GameObject>();

    public GameObject bundleGrid;


    public void ClosePurchaseWindow()
    {
        gameObject.SetActive(false);
    }

    public void SetupBundlePurchaseDetails(ItemBundle bundle)
    {
        CloudGoodsBundle bundlePriceState;

        if (bundle.CoinPrice <= 0 && bundle.CreditPrice <= 0)
            bundlePriceState = CloudGoodsBundle.Free;
        else if (bundle.CoinPrice <= 0)
            bundlePriceState = CloudGoodsBundle.CreditPurchasable;
        else if (bundle.CreditPrice <= 0)
            bundlePriceState = CloudGoodsBundle.CoinPurchasable;
        else
            bundlePriceState = CloudGoodsBundle.CreditCoinPurchaseable;

        ChangePurchaseButtonDisplay(bundle.CreditPrice, bundle.CoinPrice, bundlePriceState);

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
        Debug.Log("State: " + state.ToString());
        switch (state)
        {
            case CloudGoodsBundle.CreditPurchasable:
                StandardCurrencyPurchaseWindowFull.SetActive(false);
                StandardCurrencyPurchaseWindowHalf.SetActive(false);
                PremiumCurrencyPurchaseWindowFull.SetActive(true);
                PremiumCurrencyPurchaseWindowHalf.SetActive(false);

                UnityUIPurchaseButtonDisplay premiumButtonDisplay = PremiumCurrencyPurchaseWindowFull.GetComponent<UnityUIPurchaseButtonDisplay>();
                premiumButtonDisplay.SetState(itemCreditCost);
                break;
            case CloudGoodsBundle.CoinPurchasable:
                StandardCurrencyPurchaseWindowFull.SetActive(true);
                StandardCurrencyPurchaseWindowHalf.SetActive(false);
                PremiumCurrencyPurchaseWindowFull.SetActive(false);
                PremiumCurrencyPurchaseWindowHalf.SetActive(false);

                UnityUIPurchaseButtonDisplay standardButtonDisplay = StandardCurrencyPurchaseWindowFull.GetComponent<UnityUIPurchaseButtonDisplay>();
                standardButtonDisplay.SetState(itemCoinCost);
                break;
            case CloudGoodsBundle.Free:
                StandardCurrencyPurchaseWindowFull.SetActive(false);
                StandardCurrencyPurchaseWindowHalf.SetActive(false);
                PremiumCurrencyPurchaseWindowFull.SetActive(true);
                PremiumCurrencyPurchaseWindowHalf.SetActive(false);

                UnityUIPurchaseButtonDisplay standardButtonDisplayFree = StandardCurrencyPurchaseWindowFull.GetComponent<UnityUIPurchaseButtonDisplay>();
                standardButtonDisplayFree.SetState(itemCoinCost);
                break;
            default:
                StandardCurrencyPurchaseWindowFull.SetActive(false);
                StandardCurrencyPurchaseWindowHalf.SetActive(true);
                PremiumCurrencyPurchaseWindowFull.SetActive(false);
                PremiumCurrencyPurchaseWindowHalf.SetActive(true);

                UnityUIPurchaseButtonDisplay standardButtonDisplayDefault = StandardCurrencyPurchaseWindowHalf.GetComponent<UnityUIPurchaseButtonDisplay>();
                standardButtonDisplayDefault.SetState(itemCoinCost);

                UnityUIPurchaseButtonDisplay PremiumButtonDisplayDefault = PremiumCurrencyPurchaseWindowHalf.GetComponent<UnityUIPurchaseButtonDisplay>();
                PremiumButtonDisplayDefault.SetState(itemCoinCost);
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
