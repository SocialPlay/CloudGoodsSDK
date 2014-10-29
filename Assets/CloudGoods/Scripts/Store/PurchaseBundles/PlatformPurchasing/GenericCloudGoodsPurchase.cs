using UnityEngine;
using System.Collections;
using System;

public class GenericCloudGoodsPurchase : IPlatformPurchaser
{

    public event Action<GameObject> OKCallBack;
    public event Action<GameObject> CancelCallBack;
    public event Action<string> RecievedPurchaseResponse;
    public event Action<string> OnPurchaseErrorEvent;

    string currentitemID = "";
    int currentAmount = 0;
    string currentUserID = "";

    public void Purchase(PremiumBundle bundleItem, int amount, string userID)
    {
        //if (GatherGameInfo.allInfo.platformID == 4)
        //{
        //         //MessagePop.Instance.PopMessage("Unable to purchase items with premium currency during beta");
        //    return;
        //}

        OKCallBack += new Action<GameObject>(FaceBookPurchaser_OKCallBack);
        CancelCallBack += new Action<GameObject>(FaceBookPurchaser_CancelCallBack);

        currentitemID = bundleItem.BundleID;
        currentAmount = amount;
        currentUserID = userID;

        //MessagePop.Instance.PopMessage("Purchase Confirmation", "Are you sure you want to purchase this item?", OKCallBack, CancelCallBack, "Ok", "Cancel");
    }

    void FaceBookPurchaser_CancelCallBack(GameObject obj)
    {
        OKCallBack -= new Action<GameObject>(FaceBookPurchaser_OKCallBack);
        CancelCallBack -= new Action<GameObject>(FaceBookPurchaser_CancelCallBack);
    }

    void FaceBookPurchaser_OKCallBack(GameObject obj)
    {
        OKCallBack -= new Action<GameObject>(FaceBookPurchaser_OKCallBack);
        CancelCallBack -= new Action<GameObject>(FaceBookPurchaser_CancelCallBack);
        SocialPlay.ServiceClient.Open.StoreItemPurchase(OnReceivedPurchaseResponse, currentUserID, currentitemID, currentAmount, "Credits", CloudGoods.GuidAppID.ToString());
    }


    public void OnReceivedPurchaseResponse(string data)
    {
        // RESET CONTAINER ITEMS HERE

        string parsedData = Newtonsoft.Json.Linq.JToken.Parse(data).ToString();

        Debug.Log(parsedData);

        if (RecievedPurchaseResponse != null)
        {
            Console.WriteLine("Purchase Response");
            RecievedPurchaseResponse(parsedData);
        }
    }
}
