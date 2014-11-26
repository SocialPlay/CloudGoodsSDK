using UnityEngine;
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
		iOSConnect.onItemPurchaseCancelled += OnItemPurchaseCancelled;
		iOSConnect.onReceivedErrorOnPurchase += OnItemPurchaseError;
	}
	
	public void Purchase(PremiumBundle bundleItem, int amount, string userID)
	{
		Debug.Log ("Purchase ios called");
		currentBundleID = int.Parse (bundleItem.BundleID);
		iOSConnect.RequestInAppPurchase (bundleItem.ProductID);
	}
	
	public void OnReceivedPurchaseResponse(string data)
	{
    	SendReceiptTokenForVerification (data, 4);
	}
	
	void OnItemPurchaseCancelled(string cancelledString)
	{
		OnPurchaseErrorEvent ("Cancelled");
	}

	void OnItemPurchaseError(string errorMessage)
	{
		OnPurchaseErrorEvent ("Error has occured on purchase: " + errorMessage);
	}

	void SendReceiptTokenForVerification (string data, int platform)
	{
		BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest ();
		bundlePurchaseRequest.BundleID = currentBundleID;
		bundlePurchaseRequest.UserID = CloudGoods.user.userID.ToString ();
		bundlePurchaseRequest.ReceiptToken = data;
		bundlePurchaseRequest.PaymentPlatform = platform;
		string bundleJsonString = JsonMapper.ToJson (bundlePurchaseRequest);

		Debug.Log ("Sending bundle purchase: " + bundleJsonString);

		CloudGoods.PurchaseCreditBundles (bundleJsonString, OnReceivedSocialplayCreditsResponse);
	}


    void OnReceivedSocialplayCreditsResponse(string data)
    {
		Debug.Log ("received credit response: " + data);
		RecievedPurchaseResponse("Success");
        CloudGoods.GetPremiumCurrencyBalance(null);
    }
	
}
