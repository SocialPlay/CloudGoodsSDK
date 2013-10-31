using UnityEngine;
using System;

public class CallBackBrowserHook : MonoBehaviour
{
    public Action<string> callback;

    public static void CreateExternalCall(Action<string> callback, string hookName, string externalCallFunctionName, params object[] args)
    {
        Application.ExternalCall(externalCallFunctionName, args);
        RegisterCallBack(callback, hookName);
    }

    static void RegisterCallBack(Action<string> callback, string hookName)
    {
        GameObject packetObject = new GameObject(hookName);
        CallBackBrowserHook browserHook = packetObject.AddComponent<CallBackBrowserHook>();
        browserHook.callback = callback;
    }

    public void ReceivedResponse(string data)
    {
        if (callback != null)
            callback(data);

        Destroy(this.gameObject);
    }
}
