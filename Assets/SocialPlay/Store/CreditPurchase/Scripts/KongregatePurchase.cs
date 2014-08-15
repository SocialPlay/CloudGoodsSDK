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
    public event Action<string> OnPurchaseErrorEvent;

    public void Purchase(NGUIBundleItem bundleItem, int amount, string appID)
    {
        CallBackBrowserHook.CreateExternalCall(OnReceivedPurchaseResponse, "KongregatePurchaseBrowserHook", "KongregatePurchase", bundleItem.BundleID, amount, appID);

        //Application.ExternalCall("KongregatePurchase", id, amount, "Credits");
        //purchaserBrowserHook = CallBackBrowserHook.RegisterCallBack(OnRecievedPurchaseResponse, "KongregatePurchaseBrowserHook");
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        Console.WriteLine("OnRecievedPurchaseResponse");

        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);

        //put any kongregate specific logic in here.
    }
}

