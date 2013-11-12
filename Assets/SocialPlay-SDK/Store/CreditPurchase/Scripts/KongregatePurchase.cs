// ----------------------------------------------------------------------
// <copyright file="KongregatePurchase.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir
// ------------------------------------------------------------------------
using System;

using UnityEngine;


public class KongregatePurchase : IPlatformPurchaser
{
    public event Action<string> RecievedPurchaseResponse;

    public void Purchase(string id, int amount, string appID)
    {
        CallBackBrowserHook.CreateExternalCall(OnRecievedPurchaseResponse, "KongregatePurchaseBrowserHook", "KongregatePurchase", id, amount, appID);

        //Application.ExternalCall("KongregatePurchase", id, amount, "Credits");
        //purchaserBrowserHook = CallBackBrowserHook.RegisterCallBack(OnRecievedPurchaseResponse, "KongregatePurchaseBrowserHook");
    }

    public void OnRecievedPurchaseResponse(string data)
    {
        Console.WriteLine("OnRecievedPurchaseResponse");

        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);

        //put any kongregate specific logic in here.
    }
}

