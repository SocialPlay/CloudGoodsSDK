using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class iOSConnect : MonoBehaviour {
	
    static public Action<string> onReceivedMessage;

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

	public void ReceivedMessageFromXCode(string message)
	{
		Debug.Log ("Message received from ios: " + message);
		iOSConnect.onReceivedMessage (message);
	}

}
