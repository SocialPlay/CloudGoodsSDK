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

    public GameObject Grid;
    [HideInInspector]
    public bool isInitialized = false;
    private bool isWaitingForPlatform = false;

    IGridLoader gridLoader;
    public IPlatformPurchaser platformPurchasor;
    bool isPurchaseRequest = false;

    string domain;

    public CurrencyType type = CurrencyType.Standard;



    void Awake()
    {
        CloudGoods.OnRegisteredUserToSession += OnRegisteredUserToSession;
    }

    void Start()
    {
        this.gameObject.name = "PremiumCurrencyBundleStore";
        if (CloudGoods.isLogged && !isInitialized) Initialize();
    }


    void OnRegisteredUserToSession(string obj)
    {
        if (!isInitialized) Initialize();
    }

    public void Initialize()
    {
        switch (BuildPlatform.Platform)
        {
            case BuildPlatform.BuildPlatformType.Automatic:
                if (isWaitingForPlatform) return;
                isWaitingForPlatform = true;
                BuildPlatform.OnBuildPlatformFound += (platform) => {
                    Debug.Log("Recived new build platform");
                    Initialize();
                };
                return;
            case BuildPlatform.BuildPlatformType.Facebook:
                platformPurchasor = gameObject.AddComponent<FaceBookPurchaser>();
                break;
            case BuildPlatform.BuildPlatformType.Kongergate:
                platformPurchasor = gameObject.AddComponent<KongregatePurchase>();
                break;
            case BuildPlatform.BuildPlatformType.Android:
                platformPurchasor = gameObject.AddComponent<AndroidPremiumCurrencyPurchaser>();
                break;
            case BuildPlatform.BuildPlatformType.IOS:
                platformPurchasor = gameObject.AddComponent<iOSPremiumCurrencyPurchaser>();
                GameObject o = new GameObject("iOSConnect");
                o.AddComponent<iOSConnect>();
                break;
            case BuildPlatform.BuildPlatformType.CloudGoodsStandAlone:
                Debug.LogWarning("Cloud Goods Stand alone has not purchase method currently.");
                break;
            case BuildPlatform.BuildPlatformType.EditorTestPurchasing:
                platformPurchasor = gameObject.AddComponent<EditorPremiumCurrencyPurchaser>();
                break;
        }

        if (platformPurchasor == null)
        {            
            return;
        }

        platformPurchasor.RecievedPurchaseResponse += OnRecievedPurchaseResponse;
        platformPurchasor.OnPurchaseErrorEvent += platformPurchasor_OnPurchaseErrorEvent;

        if(BuildPlatform.Platform == BuildPlatform.BuildPlatformType.EditorTestPurchasing)
            CloudGoods.GetCreditBundles(1, OnPurchaseBundlesRecieved);
        else
            CloudGoods.GetCreditBundles((int)BuildPlatform.Platform, OnPurchaseBundlesRecieved);

        isInitialized = true;
    }

    void OnDisable()
    {
        if (platformPurchasor != null) platformPurchasor.RecievedPurchaseResponse -= OnRecievedPurchaseResponse;
    }

    void OnPurchaseBundlesRecieved(List<PaidCurrencyBundleItem> data)
    {
        Debug.Log("Got credit bundles");
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
        CloudGoods.GetPremiumCurrencyBalance(null);
    }

    void platformPurchasor_OnPurchaseErrorEvent(string obj)
    {
        Debug.Log("Purchase Platform Error: " + obj);

        isPurchaseRequest = false;
    }

}