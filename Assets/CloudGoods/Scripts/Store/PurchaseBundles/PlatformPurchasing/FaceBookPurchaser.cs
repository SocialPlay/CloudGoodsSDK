using System;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using LitJson;

public class FaceBookPurchaser : MonoBehaviour, IPlatformPurchaser
{
    public event Action<string> RecievedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;
    public int currentBundleID = 0;

    public IFacebookPurchase FacebookPurchasing;

    public void Purchase(PremiumBundle bundleItem, int amount, string userID)
    {

        if (Type.GetType("FacebookPurchasing") != null)
        {
            FacebookPurchasing = this.gameObject.AddComponent(Type.GetType("FacebookPurchasing")) as IFacebookPurchase;
        }

        if (FacebookPurchasing == null)
        {
            Debug.LogError("Facebook purchase not found. Please add the FacebookPurchase script from the CloudGoodsFacebookAddon folder to this object and drag it as the public reference to the facebookPurchase variable in the inspector");
            return;
        }

        currentBundleID = int.Parse(bundleItem.BundleID);
        Console.WriteLine("Credit bundle purchase:  ID: " + bundleItem.BundleID + " Amount: " + amount);
        Debug.Log("ID: " + bundleItem.BundleID + "\nAmount: " + amount + "\nUserID: " + userID);
        FacebookPurchasing.Purchase(bundleItem, amount, OnReceivedPurchaseResponse);
    }

    public void OnReceivedPurchaseResponse(string data)
    {
        Debug.Log("data: " + data);
        JsonMapper.ToObject(data);

        //JsonData parsedData = JsonMapper.ToObject(data);
        Newtonsoft.Json.Linq.JToken parsedData = Newtonsoft.Json.Linq.JToken.Parse(data);

        /*for(int i = 0, imax = parsedData.Count; i<imax; i++)
        {
            parsedData[i].
        }*/

        if (parsedData["error_message"] != null)
        {
            if (OnPurchaseErrorEvent != null) OnPurchaseErrorEvent(parsedData["error_message"].ToString());
            return;
        }

        Debug.Log("parsedData: " + parsedData.ToString());

        BundlePurchaseRequest bundlePurchaseRequest = new BundlePurchaseRequest();
        bundlePurchaseRequest.BundleID = currentBundleID;
        bundlePurchaseRequest.UserID = CloudGoods.user.userID.ToString();
        bundlePurchaseRequest.ReceiptToken = parsedData["payment_id"].ToString();

        //TODO implement platform check for platform premium currency bundle purchase
        bundlePurchaseRequest.PaymentPlatform = 1;

        string bundleJsonString = JsonConvert.SerializeObject(bundlePurchaseRequest);

        CloudGoods.PurchaseCreditBundles(bundleJsonString, OnPurchaseCreditsCallback);

        if (RecievedPurchaseResponse != null)
            RecievedPurchaseResponse(data);

    }

    void OnPurchaseCreditsCallback(string data)
    {
        CloudGoods.GetPremiumCurrencyBalance(null);
    }
}

