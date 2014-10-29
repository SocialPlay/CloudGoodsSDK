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

public class PremiumCurrencyBundleStore : MonoBehaviour
{
    public PlatformPurchase platformPurchase = PlatformPurchase.Facebook;
    public GameObject Grid;
    [HideInInspector]
    public bool isInitialized = false;

    IGridLoader gridLoader;
    IPlatformPurchaser platformPurchasor;
    bool isPurchaseRequest = false;

    string domain;

    public CurrencyType type = CurrencyType.Standard;



    void Awake()
    {
        CloudGoods.OnRegisteredUserToSession += OnRegisteredUserToSession;
    }




#if UNITY_WEBPLAYER
    void Start()
    {
        Application.ExternalEval("UnityObject2.instances[0].getUnity().SendMessage(\"" + name + "\", \"ReceiveURL\", document.URL);");
        //Application.ExternalEval("kongregateUnitySupport.getUnityObject().SendMessage(\"" + name + "\", \"ReceiveURL\", document.URL);");
    }

    public void ReceiveURL(string url)
    {
        // this will include the full URL, including url parameters etc.
        domain = CloudGoods.GetDomain(url);
        if (CloudGoods.isLogged && !isInitialized) Initialize();
    }
#endif

    void OnRegisteredUserToSession(string obj)
    { 
        if (!isInitialized) Initialize();
    }

    public void Initialize()
    {
        int currentplatform = 1;

        if (platformPurchase == PlatformPurchase.Automatic)
        {

#if UNITY_WEBPLAYER
            if (!string.IsNullOrEmpty(domain) && (domain.StartsWith("fbsbx") || domain.StartsWith("facebook")))
            {
                currentplatform = 1;
                platformPurchasor = gameObject.AddComponent<FaceBookPurchaser>();
            }
            else// if(domain.StartsWith("kongregate"))
            {
                currentplatform = 2;
                platformPurchasor = gameObject.AddComponent<KongregatePurchase>();
            }
#endif

#if UNITY_ANDROID
            platformPurchasor = gameObject.AddComponent<AndroidPremiumCurrencyPurchaser>();
            currentplatform = 3;
#endif

#if UNITY_IPHONE
            platformPurchasor = gameObject.AddComponent<iOSPremiumCurrencyPurchaser>();
            GameObject o = new GameObject("iOSConnect");
            o.AddComponent<iOSConnect>();
            currentplatform = 4;
#endif
        }
        else
        {
            switch (platformPurchase)
            {
                case PlatformPurchase.Facebook:
                    currentplatform = 1;
                    platformPurchasor = gameObject.AddComponent<FaceBookPurchaser>();
                    break;
                case PlatformPurchase.Kongergate:
                    currentplatform = 2;
                    platformPurchasor = gameObject.AddComponent<KongregatePurchase>();
                    break;
                case PlatformPurchase.Android:
                    currentplatform = 3;
                    platformPurchasor = gameObject.AddComponent<AndroidPremiumCurrencyPurchaser>();
                    break;
                case PlatformPurchase.IOS:
                    currentplatform = 4;
                    platformPurchasor = gameObject.AddComponent<iOSPremiumCurrencyPurchaser>();
                    break;
            }
        }
        platformPurchasor.RecievedPurchaseResponse += OnRecievedPurchaseResponse;
        platformPurchasor.OnPurchaseErrorEvent += platformPurchasor_OnPurchaseErrorEvent;
        CloudGoods.GetCreditBundles(currentplatform, OnPurchaseBundlesRecieved);

        isInitialized = true;
    }

    void OnDisable()
    {
        if (platformPurchasor != null) platformPurchasor.RecievedPurchaseResponse -= OnRecievedPurchaseResponse;
    }

    void OnPurchaseBundlesRecieved(List<PaidCurrencyBundleItem> data)
    {
        gridLoader = (IGridLoader)Grid.GetComponent(typeof(IGridLoader));
        gridLoader.ItemAdded += OnItemInGrid; 
        gridLoader.LoadGrid(data);
    }

    void OnItemInGrid(PaidCurrencyBundleItem item, GameObject obj)
    {
        PremiumBundle creditBundle = obj.GetComponent<PremiumBundle>();
        creditBundle.Amount = item.Amount.ToString();
        creditBundle.Cost = item.Cost.ToString();

        if (item.CreditPlatformIDs.ContainsKey("Android_Product_ID"))
        {
            creditBundle.ProductID = item.CreditPlatformIDs["Android_Product_ID"];
        }

        if (item.CreditPlatformIDs.ContainsKey("IOS_Product_ID"))
            creditBundle.ProductID = item.CreditPlatformIDs["IOS_Product_ID"].ToString();

        creditBundle.BundleID = item.ID.ToString();

        creditBundle.PremiumCurrencyName = "";
        creditBundle.Description = item.Description;

        //// This is temporal until its added on the portal
        //if (SocialPlaySettings.CreditBundlesDescription.Count != 0)
        //    creditBundle.Description = (item.ID - 1) <= SocialPlaySettings.CreditBundlesDescription.Count ? SocialPlaySettings.CreditBundlesDescription[item.ID - 1] : "";

        if (!string.IsNullOrEmpty(item.CurrencyIcon))
        {
            CloudGoods.GetItemTexture(item.CurrencyIcon, delegate(ImageStatus imageStatus, Texture2D texture)
            {
                creditBundle.SetIcon(texture);
            });
        }

        creditBundle.SetBundleName(item.BundleName);

        creditBundle.OnPurchaseRequest = OnPurchaseRequest;
    }

    void OnPurchaseRequest(PremiumBundle item)
    {
        if (!isPurchaseRequest)
        {
            isPurchaseRequest = true;
            platformPurchasor.Purchase(item, 1, CloudGoods.user.userID.ToString());
        }
    }

    void OnRecievedPurchaseResponse(string data)
    {
        Debug.Log("Received purchase response:  " + data);

        isPurchaseRequest = false;
    }

    void platformPurchasor_OnPurchaseErrorEvent(string obj)
    {
        Debug.Log("Purchase Platform Error: " + obj);

        isPurchaseRequest = false;
    }

}