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
		Debug.Log ("Called print message function");
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
			Debug.Log("In IOS: called print from unity");
			_PrintMessageFromUnity(productID);
        }
    }

	public void ReceivedMessageFromXCode(string message)
	{
		iOSConnect.onReceivedMessage (message);
	}

}
