// ----------------------------------------------------------------------
// <copyright file="CreditBundleStore.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir
// Date: 11/2/2012
// Description: This is a store that sells only credit bundles, to allow from native currency to our currency, if we choose not to support direct buy with platforms native currency.
// ------------------------------------------------------------------------
using UnityEngine;
using SocialPlay.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class CreditBundleStore : MonoBehaviour
{
    public GameObject Grid;
    public CurrencyBalance currencyBalance;
    public PurchaseResponsePopupHandler purchaseResponseHandler;

    IGridLoader gridLoader;
    public GameObject platformPurchaserObj;
    IPlatformPurchaser platformPurchasor;
    CreditBundleIcon creditBundleIcon = new CreditBundleIcon();

    public bool isInitialized = false;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        try
        {
            platformPurchasor = (IPlatformPurchaser)platformPurchaserObj.GetComponent(typeof(IPlatformPurchaser));
            platformPurchasor.RecievedPurchaseResponse += OnRecievedPurchaseResponse;

            GetBundle();

            isInitialized = true;
        }
        catch (System.Exception ex )
        {
            Debug.LogError(ex.Message);
        }
    }

    void OnDisable()
    {
        platformPurchasor.RecievedPurchaseResponse -= OnRecievedPurchaseResponse;
    }

    public void GetBundle()
    {
        //TODO change hard coded platformID
        WebserviceCalls.webservice.GetCreditBundles(GameAuthentication.GetAppID(), 3, OnPurchaseBundlesRecieved);
    }

    void OnPurchaseBundlesRecieved(List<CreditBundleItem> data)
    {
        InitializeGridWithBundles(data);
    }

    public void InitializeGridWithBundles(List<CreditBundleItem> data)
    {
        gridLoader = (IGridLoader)Grid.GetComponent(typeof(IGridLoader));
        gridLoader.ItemAdded += OnItemInGrid;
        gridLoader.LoadGrid(data);
    }

    void OnItemInGrid(CreditBundleItem item, GameObject obj)
    {
        NGUIBundleItem nguiItem = obj.GetComponent<NGUIBundleItem>();
        nguiItem.Amount = item.Amount.ToString();
        nguiItem.Cost = item.Cost.ToString();
        nguiItem.Id = item.ID.ToString();
        nguiItem.CurrencyName = item.CurrencyName;
        nguiItem.CurrencyIcon = creditBundleIcon.Get(nguiItem.Amount, nguiItem.CurrencyIcon);

        nguiItem.PurhcaseButtonClicked = OnPurchaseRequest;
    }

    void OnPurchaseRequest(GameObject obj)
    {
        string id = obj.transform.parent.GetComponent<NGUIBundleItem>().Id;
        platformPurchasor.Purchase(id, 1, ItemSystemGameData.UserID.ToString());
    }

    void OnRecievedPurchaseResponse(string data)
    {

        JToken dataToken = JToken.Parse(data);
        JToken dataObj = JToken.Parse(dataToken.ToString());


        if (dataObj["StatusCode"].ToString() == "1")
        {
            currencyBalance.SetItemPaidCurrency(dataObj["Balance"].ToString());
            purchaseResponseHandler.HandlePurchaseSuccess();
        }
        else
        {
            purchaseResponseHandler.HandleGeneralPurchaseFail();
        }

    }

}

public class CreditBundleItem
{
    public int Amount = 0;
    public string Cost = "";
    public int ID = 0;
    public string CurrencyName = "";
    public string CurrencyIcon = "";
}
