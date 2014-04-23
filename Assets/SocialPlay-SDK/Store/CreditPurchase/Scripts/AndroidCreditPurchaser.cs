using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SocialPlay;

public class AndroidCreditPurchaser : MonoBehaviour, IPlatformPurchaser {

    public AndroidJavaObject cls_StorePurchaser;
    public event Action<string> RecievedPurchaseResponse;

    public string publicAndroidKey;

	// Use this for initialization
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

    public void Purchase(string purchaseID, int amount, string userID)
    {
        using (AndroidJavaClass cls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject obj_Activity = cls.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_StorePurchaser.CallStatic("makePurchase", obj_Activity, purchaseID);
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
        OnReceivedPurchaseResponse(message);

        WWW www = new WWW("http://192.168.0.197/webservice/cloudgoods/cloudgoodsservice.svc/insertLog?message=" +message);

        StartCoroutine(OnWebServiceCallback(www));
    }

    IEnumerator OnWebServiceCallback(WWW www)
    {
        yield return www;
    }



    public void OnReceivedPurchaseResponse(string data)
    {
        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);
    }

}
