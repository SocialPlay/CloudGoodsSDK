using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class AndroidCreditPurchaser : MonoBehaviour, IPlatformPurchaser
{
    public int currentBundleID = 0;

#if UNITY_ANDROID && !UNITY_EDITOR
	public AndroidJavaObject cls_StorePurchaser;
#endif
	public event Action<string> RecievedPurchaseResponse;

	void Start()
	{
		gameObject.name = "AndroidCreditPurchaser";

#if UNITY_ANDROID && !UNITY_EDITOR
        initStore();
#endif
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	void initStore()
	{
		cls_StorePurchaser = new AndroidJavaClass("com.storetest.StorePurchaser");
		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				cls_StorePurchaser.CallStatic("initStore", obj_Activity, SocialPlaySettings.AndroidKey);
			}
		}
	}
#endif


	public void Purchase(string bundleID, int amount, string userID)
	{
		currentBundleID = int.Parse(bundleID);
#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				cls_StorePurchaser.CallStatic("makePurchase", obj_Activity, GetProductIDFromBundleID(currentBundleID));
			}
		}
#endif
	}

	void outputDebugStringValue()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				string javaReturn = cls_StorePurchaser.CallStatic<string>("retrieveDebugValue");
				t.text = javaReturn;
			}
		}
#endif
	}

    void RecieveFromJava(string message)
    {
        if (message != "Fail")
        {
            BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
            bundlePurchaseRequest.BundleID = currentBundleID;
            bundlePurchaseRequest.UserID = SP.user.userID;
            bundlePurchaseRequest.ReceiptToken = message;
            
            //TODO implement platform check for platform credit bundle purchase
            bundlePurchaseRequest.PaymentPlatform = 3;

            string bundleJsonString = JsonConvert.SerializeObject(bundlePurchaseRequest);

            SP.PurchaseCreditBundles(bundleJsonString, OnReceivedPurchaseResponse);
        }
    }

    string GetProductIDFromBundleID(int ID)
    {
		return SocialPlaySettings.AndroidProductNames[ID - 1];
    }

    public void OnReceivedPurchaseResponse(string data)
    {
		SP.GetPaidCurrencyBalance(null);
		SP.GetFreeCurrencyBalance(0, null);

        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);
    }

}

public class BundlePurchaseRequest
{
    public int BundleID;
    public Guid UserID;
    public string ReceiptToken;
    public int PaymentPlatform;
}
