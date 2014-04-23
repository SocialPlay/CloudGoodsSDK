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

public class CreditBundleStore : MonoBehaviour
{
    public GameObject Grid;
    public UILabel CreditBalance;

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

    public void GetBundle()
    {
        //TODO change hard coded platformID
        WebserviceCalls.webservice.GetCreditBundles(GameAuthentication.GetAppID(), 3, OnPurchaseBundlesRecieved);
    }

    void OnPurchaseBundlesRecieved(string data)
    {
        Debug.Log(data);
        InitializeGridWithBundles(data);
    }

    public void InitializeGridWithBundles(string data)
    {
        Debug.Log(data);
        gridLoader = (IGridLoader)Grid.GetComponent(typeof(IGridLoader));
        gridLoader.ItemAdded += OnItemInGrid;
        gridLoader.LoadGrid(data);
    }

    void OnItemInGrid(JObject item, GameObject obj)
    {
        NGUIBundleItem nguiItem = obj.GetComponent<NGUIBundleItem>();
        nguiItem.Amount = item["CreditAmount"].ToString();
        nguiItem.Cost = item["Cost"].ToString();
        nguiItem.Id = item["ID"].ToString();
        nguiItem.CurrencyName = "$:";
        nguiItem.CurrencyIcon = creditBundleIcon.Get(nguiItem.Amount, nguiItem.CurrencyIcon);

        nguiItem.PurhcaseButtonClicked = OnPurchaseRequest;
    }

    void OnPurchaseRequest(GameObject obj)
    {
        string id = obj.transform.parent.GetComponent<NGUIBundleItem>().Id;
        Debug.Log(id);
        platformPurchasor.Purchase(id, 1, ItemSystemGameData.UserID.ToString());
    }

    void OnRecievedPurchaseResponse(string data)
    {
        Debug.Log("received purchase");

        if (purchaseResponseHandler)
        {
            if (data != null)
                purchaseResponseHandler.HandlePurchaseSuccess();
            else if (data == "false")
                purchaseResponseHandler.HandleGeneralPurchaseFail();
        }

    }

}
