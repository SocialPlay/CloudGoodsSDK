using System;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

class FaceBookPurchaser : IPlatformPurchaser
{
    public event Action<string> RecievedPurchaseResponse;
    public int currentBundleID = 0;

    public void Purchase(string id, int amount, string userID)
    {
        currentBundleID = int.Parse(id);
        //Console.WriteLine("Credit bundle purchase:  ID: " + id + " Amount: " + amount);
        Debug.Log("ID: " + id + "\nAmount: " + amount + "\nUserID: " + userID);

        FB.Canvas.Pay(product: "https://socialplay-staging.azurewebsites.net/CreditBundleDataFacebook?BundleID=" + id,
                      quantity: amount,
                      callback: delegate(FBResult response)
                      {
                          OnReceivedPurchaseResponse(response.Text);
                          Console.WriteLine("Purchase Response: " + response.Text);
                      }
        );
    }



    public void OnReceivedPurchaseResponse(string data)
    {
        Newtonsoft.Json.Linq.JToken parsedData = Newtonsoft.Json.Linq.JToken.Parse(data);

        Debug.Log(parsedData.ToString());

        BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
        bundlePurchaseRequest.BundleID = currentBundleID;
        bundlePurchaseRequest.UserID = SP.user.userID;
        bundlePurchaseRequest.ReceiptToken = parsedData["payment_id"].ToString();

        //TODO implement platform check for platform credit bundle purchase
        bundlePurchaseRequest.PaymentPlatform = 1;

        string bundleJsonString = JsonConvert.SerializeObject(bundlePurchaseRequest);

        SP.PurchaseCreditBundles(bundleJsonString, OnPurchaseCreditsCallback);

        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);

    }

    void OnPurchaseCreditsCallback(string data)
    {
        SP.GetPaidCurrencyBalance(null);
    }
}

