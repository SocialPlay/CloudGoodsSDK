using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class iOSConnect : MonoBehaviour {
	
    static public Action<string> onReceivedMessage;
	static public Action<string> onReceivedSandboxString;

	static public Action<string> onReceivedErrorOnPurchase;
	static public Action<string> onItemPurchaseCancelled;

    [DllImport("__Internal")]
    private static extern void _PrintMessageFromUnity(string message);

    public static void RequestInAppPurchase(string productID)
    {

		Debug.Log ("Request purchase produtct ID: " + productID);
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
			Debug.Log("Not editor platform");
			_PrintMessageFromUnity(productID);
        }
    }

	public void ReceivedReceiptFromIOS(string receiptToken)
	{
		Debug.Log ("Receipt Token received from ios: " + receiptToken);
		iOSConnect.onReceivedMessage (receiptToken);
	}

	public void ReceivedReceiptFromSandbox(string receiptToken)
	{
		iOSConnect.onReceivedSandboxString (receiptToken);
	}

	public void ReceivedCancelPurchase(string cancelString)
	{
		Debug.LogWarning ("Cancelled Purchase");

		if(onItemPurchaseCancelled != null)
			onItemPurchaseCancelled ("Cancelled Purchase");
	}

	public void OnErrorFromIOS(string error)
	{
		Debug.LogError ("Error on iOS: " + error);

		if(onReceivedErrorOnPurchase != null)
			onReceivedErrorOnPurchase (error);
	}

}
