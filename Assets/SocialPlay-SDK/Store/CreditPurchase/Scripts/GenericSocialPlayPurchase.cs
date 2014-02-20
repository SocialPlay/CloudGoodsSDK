using UnityEngine;
using System.Collections;
using System;

public class GenericSocialPlayPurchase : IPlatformPurchaser
{

    public event Action<GameObject> OKCallBack;
    public event Action<GameObject> CancelCallBack;
    public event Action<string> RecievedPurchaseResponse;

    string currentitemID = "";
    int currentAmount = 0;
    string currentUserID = "";

    public void Purchase(string id, int amount, string userID)
    {
        //if (GatherGameInfo.allInfo.platformID == 4)
        //{
        //         //MessagePop.Instance.PopMessage("Unable to purchase items with credits during beta");
        //    return;
        //}

        OKCallBack += new Action<GameObject>(FaceBookPurchaser_OKCallBack);
        CancelCallBack += new Action<GameObject>(FaceBookPurchaser_CancelCallBack);

        currentitemID = id;
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
        SocialPlay.ServiceClient.Open.StoreItemPurchase(OnRecievedPurchaseResponse, currentUserID, currentitemID, currentAmount, "Credits", ItemSystemGameData.AppID.ToString());
    }


    public void OnRecievedPurchaseResponse(string data)
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
