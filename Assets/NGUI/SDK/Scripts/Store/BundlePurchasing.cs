using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BundlePurchasing : MonoBehaviour 
{
    public PurchaseButtonDisplay PremiumCurrencyPurchaseButton;
    public PurchaseButtonDisplay StandardCurrencyPurchaseButton;

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

    private void ChangePurchaseButtonDisplay(int itemCreditCost, int itemCoinCost, CloudGoodsBundle state)
    {
        switch (state)
        {
            case CloudGoodsBundle.CreditPurchasable:
                PremiumCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                PremiumCurrencyPurchaseButton.SetState(itemCreditCost);
                StandardCurrencyPurchaseButton.InsufficientFundsLabel.text = "Credit Purchase Only";
                StandardCurrencyPurchaseButton.SetState(-1);

                CreditAmount.text = itemCreditCost.ToString();
                CoinAmount.text = "N/A";
                break;
            case CloudGoodsBundle.CoinPurchasable:
                StandardCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
				StandardCurrencyPurchaseButton.SetState(itemCoinCost);
                PremiumCurrencyPurchaseButton.InsufficientFundsLabel.text = "Coin Purchase Only";
                PremiumCurrencyPurchaseButton.SetState(-1);

                CreditAmount.text = "N/A";
                CoinAmount.text = itemCoinCost.ToString();
                break;
            case CloudGoodsBundle.Free:
                CreditAmount.text = "Free";
                CoinAmount.text = "Free";
                StandardCurrencyPurchaseButton.SetState(0);
                PremiumCurrencyPurchaseButton.SetState(0);
                break;
            default:
                StandardCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                PremiumCurrencyPurchaseButton.InsufficientFundsLabel.text = "Insufficent Funds";
                StandardCurrencyPurchaseButton.SetState(itemCoinCost);
                PremiumCurrencyPurchaseButton.SetState(itemCreditCost);
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

    public void PurchaseBundleWithStandard()
    {
        CloudGoods.PurchaseItemBundles(currentItemBundle.ID, CurrencyType.Standard, purchaseContainerLocation, OnReceivedPurchaseCallback);
        ClosePurchaseWindow();
    }

    public void PurchaseBundleWithPremium()
    {
		CloudGoods.PurchaseItemBundles(currentItemBundle.ID, CurrencyType.Premium, purchaseContainerLocation, OnReceivedPurchaseCallback);
        ClosePurchaseWindow();
    }

    void OnReceivedPurchaseCallback(string data)
    {
		Debug.Log("OnReceivedPurchaseCallback " + data);
        //TODO handle callback for success and error
        //PurchaseConfirmationWindow.SetActive(true);
    }
}
