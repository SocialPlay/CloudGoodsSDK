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
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
			_PrintMessageFromUnity(productID);
        }
    }

	public void ReceivedMessageFromXCode(string message)
	{
		iOSConnect.onReceivedMessage (message);
	}

}
