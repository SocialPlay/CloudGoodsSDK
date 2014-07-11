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
    public PlatformPurchase platformPurchase = PlatformPurchase.Facebook;
    public GameObject Grid;

    IGridLoader gridLoader;
    IPlatformPurchaser platformPurchasor;
    CreditBundleIcon creditBundleIcon = new CreditBundleIcon();
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
            platformPurchasor = GetPlatformPurchaser();
            platformPurchasor.RecievedPurchaseResponse += OnRecievedPurchaseResponse;

			int currentplatform = 0;

			#if UNITY_EDITOR
				currentplatform = 1;
			#endif

			#if UNITY_ANDROID && !UNITY_EDITOR
				currentplatform = 3;
			#endif

			#if UNITY_IPHONE && !UNITY_EDITOR
				currentplatform = 4;
			#endif

            SP.GetCreditBundles(currentplatform, OnPurchaseBundlesRecieved);

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

    void OnPurchaseBundlesRecieved(List<CreditBundleItem> data)
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
		if (item.CreditPlatformIDs.ContainsKey("Product_ID"))
		    nguiItem.ProductID = item.CreditPlatformIDs ["Product_ID"].ToString ();

		if (item.CreditPlatformIDs.ContainsKey("IOS_Product_ID"))
			nguiItem.ProductID = item.CreditPlatformIDs ["IOS_Product_ID"].ToString ();

        nguiItem.BundleID = item.ID.ToString();

        nguiItem.CurrencyName = item.CurrencyName;
        //nguiItem.CurrencyIcon = creditBundleIcon.Get(nguiItem.Amount, nguiItem.CurrencyIcon);

        // This is temporal until its added on the portal
        if (SocialPlaySettings.CreditBundlesDescription.Count != 0)
            nguiItem.Description = (item.ID - 1) <= SocialPlaySettings.CreditBundlesDescription.Count ? SocialPlaySettings.CreditBundlesDescription[item.ID - 1] : "";

        nguiItem.OnPurchaseRequest = OnPurchaseRequest;
    }

    void OnPurchaseRequest(NGUIBundleItem item)
    {
		if (!isPurchaseRequest) {
			isPurchaseRequest = true;
						platformPurchasor.Purchase (item, 1, SP.user.userID.ToString ());
				}
    }

    void OnRecievedPurchaseResponse(string data)
    {
		Debug.Log ("Received purchase response");

		isPurchaseRequest = false;

        /*JToken dataToken = JToken.Parse(data);
        JToken dataObj = JToken.Parse(dataToken.ToString());


        if (dataObj["StatusCode"].ToString() == "1")
        {
            NGUITools.Broadcast(SocialPlayMessage.OnPurchaseSuccess.ToString());
        }
        else
        {
			NGUITools.Broadcast(SocialPlayMessage.OnPurchaseFail.ToString());
        }*/

    }

    IPlatformPurchaser GetPlatformPurchaser()
    {
        switch (platformPurchase)
        {
            case PlatformPurchase.Android:
                return gameObject.AddComponent<AndroidCreditPurchaser>();
            case PlatformPurchase.Facebook:
                return gameObject.AddComponent<FaceBookPurchaser>();
			case PlatformPurchase.IOS:
				return gameObject.AddComponent<iOSCreditPurchaser>();
            default:
                return null;
        }
    }

}