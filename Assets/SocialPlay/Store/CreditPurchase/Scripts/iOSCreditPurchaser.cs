using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;

public class iOSCreditPurchaser : MonoBehaviour, IPlatformPurchaser {

	public event Action<string> RecievedPurchaseResponse;
	public int currentBundleID = 0;

	void Start()
	{
		iOSConnect.onReceivedMessage += OnReceivedPurchaseResponse;
	}
	
	public void Purchase(NGUIBundleItem bundleItem, int amount, string userID)
	{
		iOSConnect.RequestInAppPurchase (bundleItem.ProductID);
	}

	
	public void OnReceivedPurchaseResponse(string data)
	{
        if (data == "Success")
        {
            BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
            bundlePurchaseRequest.BundleID = currentBundleID;
            bundlePurchaseRequest.UserID = SP.user.userID;
            bundlePurchaseRequest.ReceiptToken = UnityEngine.Random.Range(1, 1000000).ToString();

            //TODO implement platform check for platform credit bundle purchase
            bundlePurchaseRequest.PaymentPlatform = 4;

            string bundleJsonString = JsonConvert.SerializeObject(bundlePurchaseRequest);

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
