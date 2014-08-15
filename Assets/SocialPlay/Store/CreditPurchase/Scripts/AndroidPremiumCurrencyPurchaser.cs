using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class AndroidPremiumCurrencyPurchaser : MonoBehaviour, IPlatformPurchaser
{
    public int currentBundleID = 0;

	public AndroidJavaObject cls_StorePurchaser;

    public event Action<string> RecievedPurchaseResponse;
    public event Action<string> ReceivedConsumeResponse;

    void Start()
    {
        gameObject.name = "AndroidCreditPurchaser";
        initStore();
    }

	void initStore()
	{
        if (string.IsNullOrEmpty(SocialPlaySettings.AndroidKey))
        {
            Debug.LogError("No Android key has been set, cannot initialize premium bundle store");
            return;
        }

		cls_StorePurchaser = new AndroidJavaClass("com.storetest.StorePurchaser");

		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				cls_StorePurchaser.CallStatic("initStore", obj_Activity, SocialPlaySettings.AndroidKey);
			}
		}
	}

    public void Purchase(NGUIBundleItem bundleItem, int amount, string userID)
    {
        if (string.IsNullOrEmpty(SocialPlaySettings.AndroidKey))
        {
            Debug.LogError("No Android key has been set, cannot purchase from premium store");
            return;
        }

        currentBundleID = int.Parse(bundleItem.BundleID);

		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				cls_StorePurchaser.CallStatic("makePurchase", obj_Activity, bundleItem.ProductID);
			}
		}

    }

    void OnErrorCodeFromAndroidPurchase(string responseCode)
    {
        OnReceivedPurchaseResponse(responseCode);
    }

    void RecieveFromJava(string message)
    {
        if (message != "Fail")
        {
            BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
            bundlePurchaseRequest.BundleID = currentBundleID;
            bundlePurchaseRequest.UserID = SP.user.userID.ToString();
            bundlePurchaseRequest.ReceiptToken = message;

            //TODO implement platform check for platform premium currency bundle purchase
            bundlePurchaseRequest.PaymentPlatform = 3;

            string bundleJsonString = JsonConvert.SerializeObject(bundlePurchaseRequest);

            SP.PurchaseCreditBundles(bundleJsonString, OnReceivedPurchaseResponse);
        }
        else
        {
            OnReceivedPurchaseResponse(message);
        }
    }

    void DebugFromJava(string message)
    {
        Debug.Log("Debug from Java: " + message);
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);
    }

}