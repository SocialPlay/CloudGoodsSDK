using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using LitJson;
using SocialPlay.Data;
using SocialPlay.Generic;
using System.Collections.Generic;

public class WebserviceCalls : MonoBehaviour, IServiceCalls
{

    public static IServiceCalls webservice = null;

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

    public class UserInfo
    {
        public string userGuid = "";
        public bool isNewUserToWorld = false;
        public string userName = "";
        public string userEmail = "";

        public UserInfo(string newUserGuid, string newUserName, string newUserEmail)
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


    //Working On
    public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<string> callback, string ANDTags = "", string ORTags = "")
    {
        string url = string.Format("{0}GenerateItemsAtLocation?OwnerID={1}&OwnerType={2}&Location={3}&AppID={4}&MinimumEnergyOfItem={5}&TotalEnergyToGenerate={6}&ANDTags={7}&ORTags={8}", cloudGoodsURL, OwnerID, OwnerType, Location, AppID, MinimumEnergyOfItem, TotalEnergyToGenerate, ANDTags, ORTags);
        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<string> callback)
    {
        string url = string.Format("{0}GetOwnerItems?ownerID={1}&ownerType={2}&location={3}&AppID={4}", cloudGoodsURL, ownerID, ownerType, location, AppID.ToString());
        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    {
        string url = string.Format("{0}MoveItemStack?StackToMove={1}&MoveAmount={2}&DestinationOwnerID={3}&DestinationOwnerType={4}&AppID={5}&DestinationLocation={6}", cloudGoodsURL, StackToMove, MoveAmount, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);
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

    public void GetFreeCurrencyBalance(string userID, int accessLocation, string appID, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetFreeCurrencyBalance?userID=" + userID + "&accessLocation=" + accessLocation + "&appID=" + appID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetPaidCurrencyBalance(string userID, string appID, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetPaidCurrencyBalance?userID=" + userID + "&appID=" + appID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void RegisterGameSession(Guid userID, string AppID, int instanceID, Action<string> callback)
    {
        string url = cloudGoodsURL + "RegisterSession?UserId=" + userID + "&AppID=" + AppID + "&InstanceId=" + instanceID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetGameRecipes(string appID, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetRecipes?appID=" + appID;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void StoreItemPurchase(string URL, Guid userID, int itemID, int amount, string paymentType, Guid appID, int saveLocation, Action<string> callback)
    {
        string url = URL + "StoreItemPurchase?UserID=" + userID + "&ItemID=" + itemID + "&Amount=" + amount + "&PaymentType=" + paymentType + "&AppID=" + appID + "&saveLocation=" + saveLocation;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));

    }

    public void GetCreditBundles(string URL, Action<string> callback)
    {
        string url = URL + "GetPurchaseBundles";

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetAccessPinFromGuid(string userPin, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetUserInfoFromPin?pin=" + userPin;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void GetAccessPinForUser(string UserId, Action<string> callback)
    {
        string url = cloudGoodsURL + "GetUserPin?UserId=" + UserId;

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }


    public void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    {
        string url = string.Format("{0}MoveItemStacks?stacks={1}&DestinationOwnerID={2}&DestinationOwnerType={3}&AppID={4}&DestinationLocation={5}", cloudGoodsURL, stacks, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }


    public void RemoveItemStack(Guid StackRemove, Action<string> callback)
    {
        string url = string.Format("{0}RemoveStackItem?stackID={1}", cloudGoodsURL, StackRemove);
        
         WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }


    public void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback)
    {
        string url = string.Format("{0}DeductStackAmount?stackID={1}&amount={2}", cloudGoodsURL, StackRemove, amount);
        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }


    public void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback)
    {
        RemoveMultipleItems infos = new RemoveMultipleItems();
        infos.stacks = StacksToRemove;
        string stacksInfo = JsonConvert.SerializeObject(infos);
        string url = string.Format("{0}RemoveStackItems?stacks={1}", cloudGoodsURL, stacksInfo);
        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void CompleteQueueItem(Guid gameID, int QueueID, int percentScore, int location, Action<string> callback)
    {
        string url = string.Format("{0}CompleteQueueItem?gameID={1}&QueueID={2}&percentScore={3}&location={4}", cloudGoodsURL, gameID, QueueID, percentScore, location);
        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void AddInstantCraftToQueue(Guid gameID, Guid UserID, int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback)
    {
        string url = string.Format("{0}AddInstantCraftToQueue?gameID={1}&UserID={2}&ItemID={3}&Amount={4}&ItemIngredients={5}", cloudGoodsURL, gameID, UserID, ItemID, Amount, WWW.EscapeURL(JsonConvert.SerializeObject(ItemIngredients)));

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }

    public void SPLogin_UserLogin(Guid gameID, string userEmail, string password, Action<string> callback)
    {
        string url = string.Format("{0}SPLoginUserLogin?gameID={1}&userEMail={2}&userPassword={3}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail), WWW.EscapeURL(password));

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }


    public void SPLogin_UserRegister(Guid gameID, string userEmail, string password, string userName, Action<string> callback)
    {
        string url = string.Format("{0}SPLoginUserRegister?gameID={1}&userEMail={2}&userPassword={3}&userName={4}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail), WWW.EscapeURL(password), WWW.EscapeURL(userName));

        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }


    public void SPLoginForgotPassword(Guid gameID, string userEmail, Action<string> callback)
    {
        string url = string.Format("{0}SPLoginForgotPassword?gameID={1}&userEMail={2}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail));
        WWW www = new WWW(url);

        StartCoroutine(OnWebServiceCallback(www, callback));
    }


    public void SPLoginResendVerificationEmail(Guid gameID, string userEmail, Action<string> callback)
    {
        string url = string.Format("{0}SPLoginResendVerificationEmail?gameID={1}&userEMail={2}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail));
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
