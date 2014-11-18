using UnityEngine;
using System.Collections.Generic;
using System;
using LitJson;
using Newtonsoft.Json;

public class AndroidPremiumCurrencyPurchaser : MonoBehaviour, IPlatformPurchaser
{
    public int currentBundleID = 0;
    public string currentProductID = "";

#if UNITY_ANDROID
    public AndroidJavaObject cls_StorePurchaser;
#endif

    public event Action<string> RecievedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;

    void Start()
    {
        gameObject.name = "AndroidCreditPurchaser";
        initStore();
    }

    void initStore()
    {
#if UNITY_ANDROID
        if (string.IsNullOrEmpty(CloudGoodsSettings.AndroidKey))
        {
            Debug.LogError("No Android key has been set, cannot initialize premium bundle store");
            return;
        }

        cls_StorePurchaser = new AndroidJavaClass("com.storetest.StorePurchaser");

        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_StorePurchaser.CallStatic("initStore", obj_Activity, CloudGoodsSettings.AndroidKey);
            }
        }
#endif
    }

    public void Purchase(PremiumBundle bundleItem, int amount, string userID)
    {
#if UNITY_ANDROID
        if (string.IsNullOrEmpty(CloudGoodsSettings.AndroidKey))
        {
            Debug.LogError("No Android key has been set, cannot purchase from premium store");
            return;
        }

        currentBundleID = int.Parse(bundleItem.BundleID);
        currentProductID = bundleItem.ProductID;

        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                Debug.Log("Attempting to purchase Bundle Item: " + bundleItem.ProductID);
                cls_StorePurchaser.CallStatic("makePurchase", obj_Activity, bundleItem.ProductID);
            }
        }
#endif
    }

    void OnErrorCodeFromAndroidPurchase(string responseCode)
    {
		#if UNITY_ANDROID
        if (OnPurchaseErrorEvent != null)
            OnPurchaseErrorEvent(responseCode);

        if (responseCode.Remove(1, responseCode.Length - 1) == "7")
        {
            ConsumeOwneditem();
        }
#endif
    }
	    private void ConsumeOwneditem()
    {
		#if UNITY_ANDROID
        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_StorePurchaser.CallStatic("consumeitem", obj_Activity, currentProductID);
            }
        }
#endif
    }

    private void ConsumeCurrentPurchase()
    {
		#if UNITY_ANDROID
        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_StorePurchaser.CallStatic("ConsumeCurrentPurchase", obj_Activity);
            }
        }
#endif
    }

    void RecieveFromJava(string message)
    {
		#if UNITY_ANDROID
        Debug.Log("Received from java message: " + message);

        if (message != "Fail")
        {
            BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
            bundlePurchaseRequest.BundleID = currentBundleID;
            bundlePurchaseRequest.UserID = CloudGoods.user.userID.ToString();
            bundlePurchaseRequest.ReceiptToken = message;

            //TODO implement platform check for platform premium currency bundle purchase
            bundlePurchaseRequest.PaymentPlatform = 3;

            string bundleJsonString = JsonConvert.SerializeObject(bundlePurchaseRequest);

            CloudGoods.PurchaseCreditBundles(bundleJsonString, OnReceivedPurchaseResponse);
        }
        else
        {
            OnPurchaseErrorEvent(message);
        }
#endif
    }

    void DebugFromJava(string message)
    {
        Debug.Log("Debug from Java: " + message);
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        Debug.Log("On Received purchase response: " + data);

        JsonData purchaseResponseObj = LitJson.JsonMapper.ToObject(data);

        if (int.Parse(purchaseResponseObj["StatusCode"].ToString()) == 1)
        {
            ConsumeCurrentPurchase();

            if (RecievedPurchaseResponse != null)
                RecievedPurchaseResponse("Purchase Successful");
        }
        else
        {
            Debug.Log("Purchase was not authentic, consuming Item");

            if(OnPurchaseErrorEvent != null)
                OnPurchaseErrorEvent("Purchase Was not Authentic");
        }
    }

}