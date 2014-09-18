﻿using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using LitJson;

public class iOSPremiumCurrencyPurchaser : MonoBehaviour, IPlatformPurchaser
{

	public event Action<string> RecievedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;

	public int currentBundleID = 0;

	void Start()
	{
		iOSConnect.onReceivedMessage += OnReceivedPurchaseResponse;
	}
	
	public void Purchase(PremiumBundle bundleItem, int amount, string userID)
	{
		currentBundleID = int.Parse (bundleItem.BundleID);
		iOSConnect.RequestInAppPurchase (bundleItem.ProductID);
	}

	
	public void OnReceivedPurchaseResponse(string data)
	{
        if (data == "Success")
        {
            BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
            bundlePurchaseRequest.BundleID = currentBundleID;
            bundlePurchaseRequest.UserID = SP.user.userID.ToString();
            bundlePurchaseRequest.ReceiptToken = UnityEngine.Random.Range(1, 1000000).ToString();

            bundlePurchaseRequest.PaymentPlatform = 4;

			string bundleJsonString = JsonMapper.ToJson(bundlePurchaseRequest);

            SP.PurchaseCreditBundles(bundleJsonString, OnReceivedSocialplayCreditsResponse);
        }
        else if (data == "Failed" || data == "Cancelled")
        {
			RecievedPurchaseResponse("Cancelled");
        }
	}

    public void OnReceivedSocialplayCreditsResponse(string data)
    {

		RecievedPurchaseResponse("Success");
        SP.GetPaidCurrencyBalance(null);
    }
	
}
