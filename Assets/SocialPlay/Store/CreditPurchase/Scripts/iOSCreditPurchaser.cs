using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using LitJson;

public class iOSCreditPurchaser : MonoBehaviour, IPlatformPurchaser {

	public event Action<string> RecievedPurchaseResponse;
	public int currentBundleID = 0;

	void Start()
	{
		iOSConnect.onReceivedMessage += OnReceivedPurchaseResponse;
	}
	
	public void Purchase(NGUIBundleItem bundleItem, int amount, string userID)
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

            //TODO implement platform check for platform credit bundle purchase
            bundlePurchaseRequest.PaymentPlatform = 4;

			Debug.Log("Current budle ID: : " + currentBundleID + "  UserID: " + bundlePurchaseRequest.UserID + "   Recipt token: " + bundlePurchaseRequest.ReceiptToken);

			string bundleJsonString = JsonMapper.ToJson(bundlePurchaseRequest);

            SP.PurchaseCreditBundles(bundleJsonString, OnReceivedSocialplayCreditsResponse);
        }
        else if (data == "Failed")
        {

        }
	}

    public void OnReceivedSocialplayCreditsResponse(string data)
    {
        Debug.Log("Socialplay give credits callback: " + data);
        SP.GetPaidCurrencyBalance(null);
    }
	
}
