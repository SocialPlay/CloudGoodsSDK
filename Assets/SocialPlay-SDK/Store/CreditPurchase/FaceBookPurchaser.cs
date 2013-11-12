using System;
using UnityEngine;

class FaceBookPurchaser : IPlatformPurchaser
{
    public event Action<string> RecievedPurchaseResponse;

    public void Purchase(string id, int amount, string userID)
    {
        Console.WriteLine("Credit bundle purchase:  ID: " + id + " Amount: " + amount);

        CallBackBrowserHook.CreateExternalCall(OnRecievedPurchaseResponse, "FacebookPurchaseBrowserHook", "FacebookPurchase", id, amount, "Credits");

        //Application.ExternalCall("FacebookPurchase", id, amount, "Credits");
        //purchaserBrowserHook = CallBackBrowserHook.RegisterCallBack(OnRecievedPurchaseResponse, "FacebookPurchaseBrowserHook");
    }

    public void OnRecievedPurchaseResponse(string data)
    {
        string parsedData = Newtonsoft.Json.Linq.JToken.Parse(data).ToString();

        Debug.Log(parsedData);

        Console.WriteLine("OnRecievedPurchaseResponse");
        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);

        //put any facebook specific logic in here.
    }
}

