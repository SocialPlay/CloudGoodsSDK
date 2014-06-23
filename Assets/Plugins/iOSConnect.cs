using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class iOSConnect : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void _PrintMessageFromUnity(string message);

    public static void PrintMessageToXcode(string message)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            _PrintMessageFromUnity(message);
        }
    }

    [DllImport("__Internal")]
    private static extern void _SetBadgeNumber(int badgeNumer);

    public static void SetBadgeNumber(int badgeNumber)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            _SetBadgeNumber(badgeNumber);
        }
    }
}
