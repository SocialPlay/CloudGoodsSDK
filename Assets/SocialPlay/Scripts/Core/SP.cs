using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using LitJson;
using SocialPlay.Data;
using SocialPlay.Generic;
using System.Collections.Generic;
using System.Text; 
using System.Security.Cryptography;

public class SP : MonoBehaviour//, IServiceCalls
{
    #region Internal Classes
    public class SecurePayload
    {
        public string token;
        public string data;
    }

    public class GiveOwnerItemWebserviceRequest
    {
        public List<WebModels.ItemsInfo> listOfItems;
        public WebModels.OwnerTypes OwnerType;
        public string ownerID;
        public string appID;
    }
    public class UserInfo
    {
        public string ID = "";
        public bool isNewUserToWorld = false;
        public string userName = "";
        public string email = "";

        public UserInfo(string ID, string userName, string email)
        {
            this.ID = ID;
            this.userName = userName;
            this.email = email;
        }
    }

    public class UserResponse
    {
        public int code;
        public string message;
        public UserInfo userInfo;

        public UserResponse(int caseCode, string msg, UserInfo newUserInfo)
        {
            code = caseCode;
            message = msg;
            userInfo = newUserInfo;
        }

        public override string ToString()
        {
            return "Code :" + code + "\nMessage :" + message;
        }
    }

    #endregion

    #region Global Events/Callbacks

    static public Action<string> onErrorEvent;
    static public event Action<UserResponse> OnUserLogin;
    //static public event Action onLogout;
    static public event Action<UserInfo> OnUserInfo;
    static public event Action<UserResponse> OnUserRegister;
    static public event Action<UserResponse> OnForgotPassword;
    static public event Action<UserResponse> OnVerificationSent;
    static public event Action<string> OnRegisteredUserToSession;
    static public event Action<SP.UserInfo> OnUserAuthorizedEvent;

    #endregion

    static public IServiceObjectConverter serviceConverter { get { if (mService == null) mService = new LitJsonFxJsonObjectConverter(); return mService; } }    

    /// <summary>
    /// Returns the AppSecret saved on settings.
    /// </summary>

    static public string AppSecret { get { return SocialPlaySettings.AppSecret; } }

    /// <summary>
    /// Returns the AppID saved on settings.
    /// </summary>

    static public string AppID { get { return SocialPlaySettings.AppID; } }

    /// <summary>
    /// Returns the URL saved on settings.
    /// </summary>
    
    static public string Url { get { return SocialPlaySettings.Url; } }

    /// <summary>
    /// Returns the Asset Bundles URL saved on settings.
    /// </summary>

    static public string BundlesUrl { get { return SocialPlaySettings.BundlesUrl; } }

    /// <summary>
    /// Returns a the AppID inside a new Guid. If you need string only use AppID instead.
    /// </summary>
    /// <returns></returns>

    static public Guid GetAppID()
    {
        return new Guid(AppID);
    }

    #region Private Members

    static IServiceObjectConverter mService;

    static SP mInst;
    static SP Get()
    {
        if (mInst == null)
        {
            GameObject go = new GameObject("_CloudGoods");
            DontDestroyOnLoad(go);
            mInst = go.AddComponent<SP>();
        }
        return mInst;
    }

    #endregion

    #region Game Authentication

    static public void OnUserAuthorized(SP.UserInfo socialplayMsg)
    {
        new ItemSystemGameData(AppID, socialplayMsg.ID, -1, Guid.NewGuid().ToString(), socialplayMsg.userName, socialplayMsg.email);

        if (OnUserAuthorizedEvent != null)
            OnUserAuthorizedEvent(socialplayMsg);

        SP.RegisterGameSession(ItemSystemGameData.UserID, 1, (Guid sessionGuid) =>
        {
            ItemSystemGameData.SessionID = sessionGuid;
            if (OnRegisteredUserToSession != null) OnRegisteredUserToSession(ItemSystemGameData.UserID.ToString());
        });

    }

    #endregion

    #region ItemContainerManagementCalls

    static public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<List<ItemData>> callback, string ANDTags = "", string ORTags = "")
    {
        string url = string.Format("{0}GenerateItemsAtLocation?OwnerID={1}&OwnerType={2}&Location={3}&AppID={4}&MinimumEnergyOfItem={5}&TotalEnergyToGenerate={6}&ANDTags={7}&ORTags={8}", Url, OwnerID, OwnerType, Location, AppID, MinimumEnergyOfItem, TotalEnergyToGenerate, ANDTags, ORTags);
        
        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceCallGetListItemDatas(www, callback));
    }

    static public void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<List<ItemData>> callback)
    {
        string url = string.Format("{0}GetOwnerItems?ownerID={1}&ownerType={2}&location={3}&AppID={4}", Url, ownerID, ownerType, location, AppID.ToString());
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceCallGetListItemDatas(www, callback));
    }

    static public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<Guid> callback)
    {
        Debug.Log(StackToMove.ToString());

        string url = string.Format("{0}MoveItemStack?StackToMove={1}&MoveAmount={2}&DestinationOwnerID={3}&DestinationOwnerType={4}&AppID={5}&DestinationLocation={6}", Url, StackToMove, MoveAmount, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetGuid(www, callback));
    }

    static public void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<MoveMultipleItemsResponse> callback)
    {
        string url = string.Format("{0}MoveItemStacks?stacks={1}&DestinationOwnerID={2}&DestinationOwnerType={3}&AppID={4}&DestinationLocation={5}", Url, stacks, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceMoveItemsResponse(www, callback));
    }

    static public void RemoveItemStack(Guid StackRemove, Action<string> callback)
    {
        string url = string.Format("{0}RemoveStackItem?stackID={1}", Url, StackRemove);

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback)
    {
        string url = string.Format("{0}DeductStackAmount?stackID={1}&amount={2}", Url, StackRemove, amount);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback)
    {
        RemoveMultipleItems infos = new RemoveMultipleItems();
        infos.stacks = StacksToRemove;
        string stacksInfo = JsonConvert.SerializeObject(infos);
        string url = string.Format("{0}RemoveStackItems?stacks={1}", Url, stacksInfo);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void GiveOwnerItems(WebModels.OwnerTypes OwnerType, List<WebModels.ItemsInfo> listOfItems, Action<string> callback)
    {
        string jsonList = JsonConvert.SerializeObject(listOfItems);

        string url = string.Format("{0}GiveOwnerItems?AppID={1}&OwnerID={2}&OwnerType={3}&listOfItems={4}", Url, ItemSystemGameData.AppID, ItemSystemGameData.UserID.ToString(), OwnerType.ToString(), jsonList);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    #endregion

    #region UserManagement

    static public void GetUserFromWorld(int platformID, string platformUserID, string userName, string userEmail, Action<UserInfo> callback)
    {
        string url = Url + "GetUserFromWorld?appID=" + GetAppID() + "&platformID=" + platformID + "&platformUserID=" + platformUserID + "&userName=" + WWW.EscapeURL(userName) + "&loginUserEmail=" + userEmail;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetUserInfo(www, callback));
    }

    static public void RegisterGameSession(Guid userID, int instanceID, Action<Guid> callback)
    {
        string url = Url + "RegisterSession?UserId=" + userID + "&AppID=" + AppID + "&InstanceId=" + instanceID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetGuid(www, callback));
    }

    static public void GetAccessPinFromGuid(string userPin, Action<string> callback)
    {
        string url = Url + "GetUserInfoFromPin?pin=" + userPin;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void GetAccessPinForUser(string UserId, Action<string> callback)
    {
        string url = Url + "GetUserPin?UserId=" + UserId;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void Login(string userEmail, string password, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}SPLoginUserLogin?gameID={1}&userEMail={2}&userPassword={3}", Url, GetAppID(), WWW.EscapeURL(userEmail), WWW.EscapeURL(password));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceSpLoginResponse(www, (UserResponse response) =>
        {
            if (response.code == 7) if (onErrorEvent != null) onErrorEvent("ServerRelatedError");
            if (response.code == 0)
            {
                if (OnUserInfo != null)
                {
                    UserInfo userInfo = response.userInfo;
                    SP.OnUserAuthorized(new UserInfo(userInfo.ID.ToString(), userInfo.userName, userInfo.email));
                    OnUserInfo(userInfo);
                }
            }
            else
            {
                if (OnUserLogin != null) OnUserLogin(response);
                if (response != null) onSuccess(response);
            }
        }));
    }

    static public void Register(string userEmail, string password, string userName, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}SPLoginUserRegister?gameID={1}&userEMail={2}&userPassword={3}&userName={4}", Url, GetAppID(), WWW.EscapeURL(userEmail), WWW.EscapeURL(password), WWW.EscapeURL(userName));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceSpLoginResponse(www, (UserResponse response) =>
        {
            if (response.code == 7) if (onErrorEvent != null) onErrorEvent("ServerRelatedError");
            if (OnUserRegister != null) OnUserRegister(response);
            if (response != null) onSuccess(response);
        }));
    }

    static public void ForgotPassword(string userEmail, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}ForgotPassword?gameID={1}&userEMail={2}", Url, GetAppID(), WWW.EscapeURL(userEmail));
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceSpLoginResponse(www, (UserResponse response) =>
        {
            if (response.code == 7) if (onErrorEvent != null) onErrorEvent("ServerRelatedError");
            if (OnForgotPassword != null) OnForgotPassword(response);
            if (onSuccess != null) onSuccess(response);
        }));
    }


    static public void ResendVerificationEmail(string userEmail, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}ResendVerificationEmail?gameID={1}&userEMail={2}", Url, GetAppID(), WWW.EscapeURL(userEmail));
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceSpLoginResponse(www, (UserResponse response) =>
        {
            if (response.code == 7) if (onErrorEvent != null) onErrorEvent("ServerRelatedError");
            if (OnVerificationSent != null) OnVerificationSent(response);
            if (onSuccess != null) onSuccess(response);
        }));
    }

    static public void Logout()
    {
        PlayerPrefs.DeleteKey("SocialPlay_UserGuid");
        PlayerPrefs.DeleteKey("SocialPlay_UserName");
        PlayerPrefs.DeleteKey("SocialPlay_UserEmail");

        new ItemSystemGameData(Guid.Empty.ToString(), Guid.Empty.ToString(), 0, Guid.Empty.ToString(), "", "");
    }

    #endregion

    #region StoreCalls

    static public void GetFreeCurrencyBalance(string userID, int accessLocation, Action<string> callback)
    {
        string url = Url + "GetFreeCurrencyBalance?userID=" + userID + "&accessLocation=" + accessLocation + "&appID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void GetPaidCurrencyBalance(string userID, Action<string> callback)
    {
        string url = Url + "GetPaidCurrencyBalance?userID=" + userID + "&appID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void GetStoreItems(Action<List<StoreItemInfo>> callback)
    {
        string url = Url + "LoadStoreItems?appID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetStoreItems(www, callback));
    }

    static public void StoreItemPurchase(string URL, Guid userID, int itemID, int amount, string paymentType, int saveLocation, Action<string> callback)
    {
        string url = URL + "StoreItemPurchase?UserID=" + userID + "&ItemID=" + itemID + "&Amount=" + amount + "&PaymentType=" + paymentType + "&AppID=" + GetAppID() + "&saveLocation=" + saveLocation;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void GetItemBundles(Action<List<ItemBundle>> callback)
    {
        string url = Url + "GetItemBundles?Appid=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetItemBundles(www, callback));
    }

    static public void PurchaseItemBundles(Guid UserID, int bundleID, string paymentType, int location, Action<string> callback)
    {
        string url = Url + "PurchaseItemBundle?AppID=" + GetAppID() + "&UserID=" + UserID + "&BundleID=" + bundleID + "&PaymentType=" + paymentType + "&Location=" + location;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void GetCreditBundles(int platformID, Action<List<CreditBundleItem>> callback)
    {
        string url = Url + "GetCreditBundles?Appid=" + AppID + "&Platform=" + platformID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetCreditBundles(www, callback));
    }

    static public void PurchaseCreditBundles(string payload, Action<string> callback)
    {
        string url = Url + "PurchaseCreditBundle?AppID=" + GetAppID() + "&payload=" + WWW.EscapeURL(EncryptStringUnity(payload));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }


    static public void GetToken(string securePayload, Action<string> callback)
    {
        string url = Url + "GetToken?appID=" + AppID + "&payload=" + WWW.EscapeURL(EncryptStringUnity(securePayload));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (x) =>
        {
            SecureCall(DecryptString(AppSecret, x.Replace("\\", "")), "", callback);
        }));
    }

    static public void SecureCall(string token, string securePayload, Action<string> callback)
    {
        List<WebModels.ItemsInfo> listOfItems = new List<WebModels.ItemsInfo>();

        WebModels.ItemsInfo item = new WebModels.ItemsInfo();
        item.amount = 1;
        item.ItemID = 106465;
        item.location = 0;
        listOfItems.Add(item);

        GiveOwnerItemWebserviceRequest request = new GiveOwnerItemWebserviceRequest();
        request.listOfItems = listOfItems;
        request.ownerID = "ef595214-369f-4313-9ac7-b0036e5ac25c";
        request.appID = AppID;
        request.OwnerType = WebModels.OwnerTypes.User;

        string newStringRequest = JsonConvert.SerializeObject(request);

        SecurePayload payload = new SecurePayload();
        payload.token = token;
        payload.data = newStringRequest;

        string securePayloadString = JsonConvert.SerializeObject(payload);

        Debug.Log(securePayloadString);

        string url = Url + "SecureAction?appID=" + AppID + "&payload=" + WWW.EscapeURL(EncryptStringUnity(securePayloadString));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }    

    #endregion

    #region RecipeCalls
    static public void GetGameRecipes(string appID, Action<List<RecipeInfo>> callback)
    {
        string url = Url + "GetRecipes?appID=" + appID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetRecipeInfos(www, callback));
    }

    static public void CompleteQueueItem(int QueueID, int percentScore, int location, Action<string> callback)
    {
        string url = string.Format("{0}CompleteQueueItem?gameID={1}&QueueID={2}&percentScore={3}&location={4}", Url, GetAppID(), QueueID, percentScore, location);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void AddInstantCraftToQueue(Guid UserID, int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback)
    {
        string url = string.Format("{0}AddInstantCraftToQueue?gameID={1}&UserID={2}&ItemID={3}&Amount={4}&ItemIngredients={5}", Url, GetAppID(), UserID, ItemID, Amount, WWW.EscapeURL(JsonConvert.SerializeObject(ItemIngredients)));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    #endregion

    #region IEnumeratorCalls

    IEnumerator ServiceGetString(WWW www, Action<string> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            callback(serviceConverter.ConvertToString(www.text));
        }
        else
        {
            callback("WWW Error: " + www.error);
        }
    }

    IEnumerator ServiceGetUserInfo(WWW www, Action<UserInfo> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
            callback(serviceConverter.ConvertToUserInfo(www.text));
        }
        else
        {
            onErrorEvent("Error:" + www.error);
        }
    }


    IEnumerator ServiceCallGetListItemDatas(WWW www, Action<List<ItemData>> callback)
    {
        yield return www;

        if (www.error == null)
            callback(serviceConverter.ConvertToItemDataList(www.text));
        else
            onErrorEvent(www.error);
    }

    IEnumerator ServiceGetStoreItems(WWW www, Action<List<StoreItemInfo>> callback)
    {
        yield return www;

        if (www.error == null)
            callback(serviceConverter.ConvertToStoreItems(www.text));
        else
            onErrorEvent(www.error);
    }

    IEnumerator ServiceGetGuid(WWW www, Action<Guid> callback)
    {
        yield return www;

        if (www.error == null)
            callback(serviceConverter.ConvertToGuid(www.text));
        else
            onErrorEvent(www.error);
    }

    IEnumerator ServiceGetRecipeInfos(WWW www, Action<List<RecipeInfo>> callback)
    {
        yield return www;

        if (www.error == null)
            callback(serviceConverter.ConvertToListRecipeInfo(www.text));
        else
            onErrorEvent(www.error);
    }

    IEnumerator ServiceGetItemBundles(WWW www, Action<List<ItemBundle>> callback)
    {
        yield return www;

        Debug.Log(www.text);

        if (www.error == null)
            callback(serviceConverter.ConvertToListItemBundle(www.text));
        else
            onErrorEvent(www.error);
    }

    IEnumerator ServiceGetCreditBundles(WWW www, Action<List<CreditBundleItem>> callback)
    {
        yield return www;
        Debug.Log(www.text);
        if (www.error == null)
            callback(serviceConverter.ConvertToListCreditBundleItem(www.text));
        else
            onErrorEvent(www.error);
    }

    IEnumerator ServiceMoveItemsResponse(WWW www, Action<MoveMultipleItemsResponse> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            callback(serviceConverter.ConvertToMoveMultipleItemsResponse(www.text));
        }
        else
        {
            onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceSpLoginResponse(WWW www, Action<UserResponse> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            callback(serviceConverter.ConvertToSPLoginResponse(www.text));
        }
        else
        {
            onErrorEvent("Error: " + www.error);
        }
    }

    #endregion

    #region Encryption

    static public string EncryptStringUnity(string Message)
    {
        byte[] Results;

        UTF8Encoding UTF8 = new UTF8Encoding();


        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();

        //TODO put in pass phrase
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(AppSecret));

        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;

        byte[] DataToEncrypt = UTF8.GetBytes(Message);

        try
        {
            ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
        }
        finally
        {
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        return Convert.ToBase64String(Results);
    }

    static public string DecryptString(string passphrase, string Message)
    {
        byte[] Results;
        UTF8Encoding UTF8 = new UTF8Encoding();

        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));

        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;

        byte[] DataToDecrypt = Convert.FromBase64String(Message);

        try
        {
            ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
        }
        catch (Exception ex)
        {
            TDESAlgorithm.Clear();
            HashProvider.Clear();
            return ex.ToString();
        }
        finally
        {
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        return UTF8.GetString(Results);
    }

    #endregion
}
