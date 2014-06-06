using System;
using UnityEngine;

class FaceBookPurchaser : IPlatformPurchaser
{
    public event Action<string> RecievedPurchaseResponse;

    public void Purchase(string id, int amount, string userID)
    {
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
        string parsedData = Newtonsoft.Json.Linq.JToken.Parse(data).ToString();

        Debug.Log(parsedData);
        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);

    }
}

