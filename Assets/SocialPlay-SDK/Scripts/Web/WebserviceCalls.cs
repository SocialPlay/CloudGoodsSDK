using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
public class WebserviceCalls : MonoBehaviour
{

    public static WebserviceCalls webservice = null;

    string cloudGoodsURL = "https://SocialPlayWebService.azurewebsites.net/cloudgoods/cloudgoodsservice.svc/";


    void Awake()
    {
        if (webservice == null)
        {
            webservice = this;
        }

        if (this.GetComponent<WebServiceUrlSwitcher>())
        {
            WebServiceUrlSwitcher switcher = this.GetComponent<WebServiceUrlSwitcher>();
            if (switcher.isUsed)
            {
                cloudGoodsURL = switcher.preferredURL;
                Debug.Log("Changed Preffered URL to " + cloudGoodsURL);
            }
        }
    }

    public class UserGuid
    {
        public string userGuid = "";
        public bool isNewUserToWorld = false;
        public string userName = "";
        public string userEmail = "";

        public UserGuid(string newUserGuid, string newUserName, string newUserEmail)
        {
            userGuid = newUserGuid;
            userName = newUserName;
            userEmail = newUserEmail;
        }
    }

    public void OnReceivedGeneratedItems(string generatedItemsJson)
    {
        Debug.Log("JSON: " + generatedItemsJson);
    }

    public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid GameID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, string ANDTags, string ORTags, Action<string> callback)
    {
        string url = cloudGoodsURL + "GenerateItemsAtLocation?OwnerID=" + OwnerID + "&OnwerType=" + OwnerType + "&Location=" + Location + "&GameID=" + GameID + "&MinimumEnergyOfItems=" + MinimumEnergyOfItem + "&TotalEnergyToGenerateFor=" + TotalEnergyToGenerate + "&ANDTags=" + "" + "&ORTags=" + "";
        Debug.Log(url);
        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetOwnerItems(string ownerID, string ownerType, int location, string gameGuid, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetOwnerItems?ownerID=" + ownerID + "&ownerType=" + ownerType + "&location=" + location + "&gameGuid=" + gameGuid;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, int DestinationGameID, int DestinationLocation, Action<string> callback)
    {
        string url = cloudGoodsURL + "MoveItemStack?StackToMove=" + StackToMove + "&MoveAmount=" + MoveAmount + "&DestinationOwnerID=" + DestinationOwnerID + "&DestinationOwnerType=" + DestinationOwnerType + "&DestinationGameID=" + DestinationGameID + "&DestinationLocation=" + DestinationLocation;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetUserFromWorld(Guid appID, int platformID, string platformUserID, string userName, string userEmail, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetUserFromWorld?appID=" + appID + "&platformID=" + platformID + "&platformUserID=" + platformUserID + "&userName=" + WWW.EscapeURL(userName) + "&loginUserEmail=" + userEmail;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetStoreItems(string appID, Action<string> callback)
    {
        string url = cloudGoodsURL + "LoadStoreItems?appID=" + appID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetFreeCurrencyBalance(string userID, string appID, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetFreeCurrencyBalance?userID=" + userID + "&appID=" + appID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetPaidCurrencyBalance(string userID, string appID, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetPaidCurrencyBalance?userID=" + userID + "&appID=" + appID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void RegisterGameSession(Guid userID, int instanceID, Action<string> callback)
    {
        string url = cloudGoodsURL + "RegisterSession?UserId=" + userID + "&InstanceId=" + instanceID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetGameRecipes(string AppID, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetRecipes?appID=" + AppID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void StoreItemPurchase(string URL, Guid userID, int itemID, int amount, string paymentType, Guid appID, Action<string> callback)
    {
        string url = URL + "StoreItemPurchase?UserID=" + userID + "&ItemID=" + itemID + "&Amount=" + amount + "&PaymentType=" + paymentType + "&AppID=" + appID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));

    }

    public void GetCreditBundles(string URL, Action<string> callback)
    {
        string url = URL + "GetPurchaseBundles";

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    IEnumerator OnWebServiceCallback(WWW www, Action<string> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            callback(www.text);
        }
        else
        {
            callback("WWW Error: " + www.error);
            //callback("Error has occured");
        }
    }
}
