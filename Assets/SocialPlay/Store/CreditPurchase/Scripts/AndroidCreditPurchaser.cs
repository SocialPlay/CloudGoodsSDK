using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class AndroidCreditPurchaser : MonoBehaviour, IPlatformPurchaser
{
    public int currentBundleID = 0;

    public UILabel debugMessage;

	public AndroidJavaObject cls_StorePurchaser;

    public event Action<string> RecievedPurchaseResponse;

    void Start()
    {
        gameObject.name = "AndroidCreditPurchaser";
        debugMessage = GameObject.Find("DebugLabel").GetComponent<UILabel>();
        initStore();
    }


	void initStore()
	{
		cls_StorePurchaser = new AndroidJavaClass("com.storetest.StorePurchaser");

        debugMessage.text = "init store";

		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
            debugMessage.text = "found unityplayer";

			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
                debugMessage.text = "found current acticvity : " + obj_Activity.ToString();
                cls.CallStatic("UnitySendMessage", "CreditStore", "RecieveFromJava", "whoowhoo"); 
				cls_StorePurchaser.CallStatic("initStore", obj_Activity, SocialPlaySettings.AndroidKey);

                outputDebugStringValue();
			}
		}
	}



    public void Purchase(NGUIBundleItem bundleItem, int amount, string userID)
    {
        currentBundleID = int.Parse(bundleItem.BundleID);

        debugMessage.text = "Purchase item: " + bundleItem.ProductID;

		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				cls_StorePurchaser.CallStatic("makePurchase", obj_Activity, bundleItem.ProductID);
			}
		}

    }

    void outputDebugStringValue()
    {
		using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				string javaReturn = cls_StorePurchaser.CallStatic<string>("retrieveDebugValue");
				debugMessage.text = javaReturn;
			}
		}

    }

    void RecieveFromJava(string message)
    {
        if (message != "Fail")
        {
            BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
            bundlePurchaseRequest.BundleID = currentBundleID;
            bundlePurchaseRequest.UserID = SP.user.userID.ToString();
            bundlePurchaseRequest.ReceiptToken = message;

            //TODO implement platform check for platform credit bundle purchase
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
        debugMessage.text = message;
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);
    }

}