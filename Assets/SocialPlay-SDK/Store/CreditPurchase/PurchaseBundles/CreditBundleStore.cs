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

    public PurchaseResponseHandler purchaseResponseHandler;

    public string UserID { get; set; }

    IGridLoader gridLoader;
    IPlatformPurchaser platformPurchasor;
    CreditBundleIcon creditBundleIcon = new CreditBundleIcon();

    public bool isInitialized = false;

    void Start()
    {
        UserID = "69EE1B4D-2002-43FC-B2AE-59A4C17D7E50";

        Initialize();
    }

    public void Initialize()
    {
        try
        {
            //platformPurchasor = new KongregatePurchase();
            platformPurchasor = new FaceBookPurchaser();
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
        //SocialPlay.ServiceClient.Open.GetPurchaseBundles(OnPurchaseBundlesRecieved);
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
        nguiItem.Id = item["Id"].ToString();
        nguiItem.CurrencyName = "$:";
        nguiItem.CurrencyIcon = creditBundleIcon.Get(nguiItem.Amount, nguiItem.CurrencyIcon);

        //GameObject currencyIcon = NGUITools.AddChild(obj, GUIStorePlatform.Instance.currencyIcon);
        //currencyIcon.transform.localScale = new Vector3(17, 17, 1);
        //currencyIcon.transform.localPosition = new Vector3(9, 38, 0);
        nguiItem.PurhcaseButtonClicked = OnPurchaseRequest;
    }

    void OnPurchaseRequest(GameObject obj)
    {
        string id = obj.transform.parent.GetComponent<NGUIBundleItem>().Id;
        platformPurchasor.Purchase(id, 1, "CAD63AD6-4D75-48D9-86AB-99A28E2BA004");
    }

    void OnRecievedPurchaseResponse(string data)
    {

        if (data == "true")
            purchaseResponseHandler.HandlePurchaseSuccess();
        else if (data == "false")
            purchaseResponseHandler.HandleGeneralPurchaseFail();

    }

}
