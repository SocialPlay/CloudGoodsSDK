using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SocialPlay;
using Newtonsoft.Json;

public class AndroidCreditPurchaser : MonoBehaviour, IPlatformPurchaser {

    public List<string> androidProductNames;

    public AndroidJavaObject cls_StorePurchaser;
    public event Action<string> RecievedPurchaseResponse;

    public string publicAndroidKey;

    public int currentBundleID = 0;


	void Start () {
        gameObject.name = "AndroidCreditPurchaser";
        initStore();
	}

    void initStore()
    {
        cls_StorePurchaser = new AndroidJavaClass("com.storetest.StorePurchaser");
        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_StorePurchaser.CallStatic("initStore", obj_Activity, publicAndroidKey);
            }
        }
    }

    public void Purchase(string bundleID, int amount, string userID)
    {
        currentBundleID = int.Parse(bundleID);

        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_StorePurchaser.CallStatic("makePurchase", obj_Activity, GetProductIDFromBundleID(currentBundleID));
            }
        }
    }

    void outputDebugStringValue()
    {
        TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                string javaReturn = cls_StorePurchaser.CallStatic<string>("retrieveDebugValue");
                t.text = javaReturn;
            }
        }
    }

    void RecieveFromJava(string message)
    {
        if (message != "Fail")
        {
            BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
            bundlePurchaseRequest.BundleID = currentBundleID;
            bundlePurchaseRequest.UserID = ItemSystemGameData.UserID;
            bundlePurchaseRequest.ReceiptToken = message;
            
            //TODO implement platform check for platform credit bundle purchase
            bundlePurchaseRequest.PaymentPlatform = 3;

            string bundleJsonString = JsonConvert.SerializeObject(bundlePurchaseRequest);

            WebserviceCalls.webservice.PurchaseCreditBundles(new Guid(GameAuthentication.GetAppID()), bundleJsonString, OnReceivedPurchaseResponse);
        }
    }

    string GetProductIDFromBundleID(int ID)
    {
        switch (ID)
        {
            case 1:
                return androidProductNames[ID - 1];
            case 2:
                return androidProductNames[ID - 1];
            case 3:
                return androidProductNames[ID - 1];
            case 4:
                return androidProductNames[ID - 1];
            case 5:
                return androidProductNames[ID - 1];
            case 6:
                return androidProductNames[ID - 1];
            default:
                return null;
        }
    }

    public void OnReceivedPurchaseResponse(string data)
    {
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
