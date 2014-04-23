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

    void Awake()
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
        WebserviceCalls webserviceCalls = GameObject.Find("Socialplay").GetComponent<WebserviceCalls>();
        webserviceCalls.GetCreditBundles("http://socialplaywebservice.azurewebsites.net/publicservice.svc/", OnPurchaseBundlesRecieved);
    }

    void OnPurchaseBundlesRecieved(string data)
    {
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
        nguiItem.Amount = item["Amount"].ToString();
        nguiItem.Cost = item["Cost"].ToString();
        nguiItem.Id = GetProductIDFromBundleID(int.Parse(item["Id"].ToString()));
        nguiItem.CurrencyName = "$:";
        nguiItem.CurrencyIcon = creditBundleIcon.Get(nguiItem.Amount, nguiItem.CurrencyIcon);

        nguiItem.PurhcaseButtonClicked = OnPurchaseRequest;
    }

    string GetProductIDFromBundleID(int ID)
    {
        switch (ID)
        {
            case 1:
                return "socialplay_item.1";
            case 2:
                return "socialplay_item.2";
            case 3:
                return "socialplay_item.3";
            case 4:
                return "socialplay_item.4";
            case 5:
                return "socialplay_item.5";
            case 6:
                return "socialplay_item.6";
            default:
                return null;
        }
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
