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

public class PaidCurrencyBundleStore : MonoBehaviour
{
    //public PlatformPurchase platformPurchase = PlatformPurchase.Facebook;
    public GameObject Grid;

    IGridLoader gridLoader;
    IPlatformPurchaser platformPurchasor;
    PaidCurrencyBundleIcon PaidCurrencyBundleIcon = new PaidCurrencyBundleIcon();
    bool isPurchaseRequest = false;

    public bool isInitialized = false;

    void Awake()
    {
        SP.OnRegisteredUserToSession += OnRegisteredUserToSession;
    }

    void OnRegisteredUserToSession(string obj)
    {
        Initialize();
    }

    public void Initialize()
    {
        try
        {      
            int currentplatform = 0;

            platformPurchasor = gameObject.AddComponent<FaceBookPurchaser>();

#if UNITY_EDITOR
            currentplatform = 1;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
            platformPurchasor = gameObject.AddComponent<AndroidPaidCurrencyPurchaser>();
            currentplatform = 3;
#endif

#if UNITY_IPHONE && !UNITY_EDITOR
            platformPurchasor = gameObject.AddComponent<iOSPaidCurrencyPurchaser>();
            currentplatform = 4;
#endif

            platformPurchasor.RecievedPurchaseResponse += OnRecievedPurchaseResponse;
            SP.GetCreditBundles(currentplatform, OnPurchaseBundlesRecieved);

            isInitialized = true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    void OnDisable()
    {
        platformPurchasor.RecievedPurchaseResponse -= OnRecievedPurchaseResponse;
    }

    void OnPurchaseBundlesRecieved(List<PaidCurrencyBundleItem> data)
    {
        gridLoader = (IGridLoader)Grid.GetComponent(typeof(IGridLoader));
        gridLoader.ItemAdded += OnItemInGrid;
        gridLoader.LoadGrid(data);
    }

    void OnItemInGrid(PaidCurrencyBundleItem item, GameObject obj)
    {
        NGUIBundleItem nguiItem = obj.GetComponent<NGUIBundleItem>();
        nguiItem.Amount = item.Amount.ToString();
        nguiItem.Cost = item.Cost.ToString();

        if (item.CreditPlatformIDs.ContainsKey("Android_Product_ID"))
            nguiItem.ProductID = item.CreditPlatformIDs["Android_Product_ID"];

        if (item.CreditPlatformIDs.ContainsKey("IOS_Product_ID"))
            nguiItem.ProductID = item.CreditPlatformIDs["IOS_Product_ID"].ToString();

        nguiItem.BundleID = item.ID.ToString();

        nguiItem.CurrencyName = item.CurrencyName;
        nguiItem.Description = item.Description;
        //nguiItem.CurrencyIcon = creditBundleIcon.Get(nguiItem.Amount, nguiItem.CurrencyIcon);

        // This is temporal until its added on the portal
        if (SocialPlaySettings.CreditBundlesDescription.Count != 0)
            nguiItem.Description = (item.ID - 1) <= SocialPlaySettings.CreditBundlesDescription.Count ? SocialPlaySettings.CreditBundlesDescription[item.ID - 1] : "";

        if (!string.IsNullOrEmpty(item.CurrencyIcon))
        {
            SP.GetItemTexture(item.CurrencyIcon, delegate(ImageStatus imageStatus, Texture2D texture)
            {
                nguiItem.SetCredtiBundleIcon(texture);
            });
        }

        nguiItem.OnPurchaseRequest = OnPurchaseRequest;
    }

    void OnPurchaseRequest(NGUIBundleItem item)
    {
        if (!isPurchaseRequest)
        {
            isPurchaseRequest = true;
            platformPurchasor.Purchase(item, 1, SP.user.userID.ToString());
        }
    }

    void OnRecievedPurchaseResponse(string data)
    {
        Debug.Log("Received purchase response");

        isPurchaseRequest = false;
    }

    /*IPlatformPurchaser GetPlatformPurchaser()
    {
        switch (platformPurchase)
        {
            case PlatformPurchase.Android:
                return gameObject.AddComponent<AndroidPaidCurrencyPurchaser>();
            case PlatformPurchase.Facebook:
                return gameObject.AddComponent<FaceBookPurchaser>();
            case PlatformPurchase.IOS:
                return gameObject.AddComponent<iOSPaidCurrencyPurchaser>();
            default:
                return null;
        }
    }*/

}