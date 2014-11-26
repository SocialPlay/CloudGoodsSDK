using UnityEngine;
using System.Collections;
using System;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
using LitJson;
using SocialPlay.Data;
using SocialPlay.Generic;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

public class CloudGoods : MonoBehaviour//, IServiceCalls
{
    #region Global Events/Callbacks

    static public event Action<string> onErrorEvent;
    static public event Action<UserResponse> OnUserLogin;
    static public event Action onLogout;
    static public event Action<CloudGoodsUser> OnUserInfo;
    static public event Action<UserResponse> OnUserRegister;
    static public event Action<UserResponse> OnForgotPassword;
    static public event Action<UserResponse> OnVerificationSent;
    static public event Action<string> OnRegisteredUserToSession;
    static public event Action<CloudGoodsUser> OnUserAuthorized;
    static public event Action<List<StoreItem>> OnStoreListLoaded;
    static public event Action<List<ItemBundle>> OnStoreItemBundleListLoaded;
    static public event Action<List<ItemData>> OnItemsLoaded;
    static public event Action<int> OnStandardCurrency;
    static public event Action<int> OnPremiumCurrency;
    static public event Action<string> OnStandardCurrencyName;
    static public event Action<string> OnPremiumCurrencyName;
    static public event Action<Texture2D> OnStandardCurrencyTexture;
    static public event Action<Texture2D> OnPremiumCurrencyTexture;

    #endregion

    static public IServiceObjectConverter serviceConverter { get { if (mService == null) mService = new LitJsonFxJsonObjectConverter(); return mService; } }

    static public ItemDataConverter itemDataConverter { get { if (mDataConverter == null) mDataConverter = new GameObjectItemDataConverter(); return mDataConverter; } }

    /// <summary>
    /// Returns the AppSecret saved on settings.
    /// </summary>

    static public string AppSecret
    {
        get
        {
            if (string.IsNullOrEmpty(CloudGoodsSettings.AppSecret))
            {
                Debug.LogError("AppSecret has not been defined. Open Social Play Settings from the menu.");

                if (onErrorEvent != null)
                    onErrorEvent("AppSecret has not been defined. Open Social Play Settings from the menu.");
            }

            return CloudGoodsSettings.AppSecret;
        }
    }

    /// <summary>
    /// Returns the AppID saved on settings.
    /// </summary>

    static public string AppID
    {
        get
        {
            if (string.IsNullOrEmpty(CloudGoodsSettings.AppID))
            {
                Debug.LogError("AppID has not been defined. Open Social Play Settings from the menu.");

                if (onErrorEvent != null)
                    onErrorEvent("AppID has not been defined. Open Cloud Goods Settings from the menu.");
            }

            return CloudGoodsSettings.AppID.Trim();
        }
    }

    /// <summary>
    /// Returns the URL saved on settings.
    /// </summary>

    static public string Url
    {
        get
        {
            if (string.IsNullOrEmpty(CloudGoodsSettings.Url))
                Debug.LogError("Url has not been defined. Open Social Play Settings from the menu.");

            return CloudGoodsSettings.Url;
            //return "http://192.168.0.197/webservice/cloudgoods/cloudgoodsservice.svc/";
        }
    }

    /// <summary>
    /// Returns the Asset Bundles URL saved on settings.
    /// </summary>

    static public string BundlesUrl
    {
        get
        {
            if (string.IsNullOrEmpty(CloudGoodsSettings.BundlesUrl))
                Debug.LogError("BundlesUrl has not been defined. Open Social Play Settings from the menu.");

            return CloudGoodsSettings.BundlesUrl;
        }
    }

    /// <summary>
    /// Returns a the AppID inside a new Guid. If you need string only use AppID instead.
    /// </summary>
    /// <returns></returns>

    static public Guid GuidAppID
    {
        get
        {
            try
            {
                return new Guid(AppID);
            }
            catch
            {
                Debug.LogError("AppID is not a valid Guid. Please check your guid and try again.");
                return Guid.Empty;
            }

        }
    }

    /// <summary>
    /// Cached store items list.
    /// </summary>

    static public List<StoreItem> storeItems { get; private set; }

    /// <summary>
    /// Cached user items list.
    /// </summary>

    static public List<ItemData> userItems { get; private set; }

    /// <summary>
    /// Current amount of standard currency. You can listen to the event OnFreeCurrency which will be triggered everytime this value changes.
    /// </summary>

    static public int standardCurrency
    {
        get { return mFree; }
        private set
        {
            if (mFree != value)
            {
                mFree = value;
                if (OnStandardCurrency != null) OnStandardCurrency(mFree);
            }
        }
    }

    /// <summary>
    /// Current amount of premium currency. You can listen to the event OnPaidCurrency which will be triggered everytime this value changes.
    /// </summary>

    static public int premiumCurrency
    {
        get { return mPaid; }
        private set
        {
            if (mPaid != value)
            {
                mPaid = value;
                if (OnPremiumCurrency != null) OnPremiumCurrency(mPaid);
            }
        }
    }

    static public Texture2D standardCurrencyTexture
    {
        get
        {
            if (tFree != null)
                return tFree;
            else
            {
                if (isGettingWorldInfo == false)
                {
                    GetWorldCurrencyInfo(null);
                    isGettingWorldInfo = true;
                }

                return null;
            }
        }
    }
    static public Texture2D premiumCurrencyTexture
    {
        get
        {
            if (tPaid != null)
                return tPaid;
            else
            {
                if (isGettingWorldInfo == false)
                {
                    GetWorldCurrencyInfo(null);
                    isGettingWorldInfo = true;
                }

                return null;
            }
        }
    }

    static public string StandardCurrencyName
    {
        get
        {
            if (!string.IsNullOrEmpty(sfree))
            {
                return sfree;
            }
            else
            {
                if (isGettingWorldInfo == false)
                {
                    GetWorldCurrencyInfo(null);
                    isGettingWorldInfo = true;
                }

                return null;
            }
        }

    }

    static public string PremiumCurrencyName
    {
        get
        {
            if (!string.IsNullOrEmpty(sPaid))
            {
                return sPaid;
            }
            else
            {
                if (isGettingWorldInfo == false)
                {
                    GetWorldCurrencyInfo(null);
                    isGettingWorldInfo = true;
                }

                return null;
            }
        }
    }

    /// <summary>
    /// Returns the default item drop game object.
    /// </summary>

    static public GameObject DefaultItemDrop
    {
        get
        {
            if (CloudGoodsSettings.DefaultItemDrop == null)
                Debug.LogError("DefaultItemDrop has not been defined. Open Social Play Settings from the menu.");

            return CloudGoodsSettings.DefaultItemDrop;
        }
    }

    /// <summary>
    /// Returns the default ui item game object.
    /// </summary>

    static public GameObject DefaultUIItem
    {
        get
        {
            if (CloudGoodsSettings.DefaultUIItem == null)
                Debug.LogError("DefaultUIItem has not been defined. Open Social Play Settings from the menu.");

            return CloudGoodsSettings.DefaultUIItem;
        }
    }

    /// <summary>
    /// True if the user is logged in.
    /// </summary>

    static public bool isLogged
    {
        get { return user != null && user.sessionID != Guid.Empty; }
    }

    /// <summary>
    /// Current user information.
    /// </summary>

    static public CloudGoodsUser user { get; private set; }

    #region Private Members

    static IServiceObjectConverter mService;
    static ItemDataConverter mDataConverter;

    static bool isGettingWorldInfo = false;
    static int mFree = 0;
    static int mPaid = 0;
    static Texture2D tFree;
    static Texture2D tPaid;
    static string sfree;
    static string sPaid;
    static CloudGoods mInst;
    static CloudGoods Get()
    {
        if (mInst == null)
        {
            GameObject go = new GameObject("_CloudGoods");
            DontDestroyOnLoad(go);
            mInst = go.AddComponent<CloudGoods>();
        }
        return mInst;
    }

    #endregion

    #region Game Authentication

    static public void AuthorizeUser(CloudGoodsUser userInfo)
    {
        user = userInfo;
        user.userID = new Guid(user.userGuid.ToString());
        user.sessionID = Guid.NewGuid();

        //GetOwnerItems(user.userID.ToString(), "User", 0, OnItemsLoaded);
        //GetStoreItems(OnStoreListLoaded);
        GetStandardCurrencyBalance(0, null);
        GetPremiumCurrencyBalance(null);
        GetWorldCurrencyInfo(null);

        if (OnUserAuthorized != null)
            OnUserAuthorized(user);

		Debug.Log ("get session before");

        CloudGoods.RegisterGameSession(1, (Guid sessionGuid) =>
        {
			Debug.Log("Register game session finished: " + sessionGuid);
            user.sessionID = sessionGuid;
            if (OnRegisteredUserToSession != null) OnRegisteredUserToSession(user.userID.ToString());
        });

    }

    #endregion

    #region ItemContainerManagementCalls

    static public void GenerateItemsAtLocation(string OwnerType, int Location, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<List<ItemData>> callback, string ANDTags = "", string ORTags = "")
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return;
        }
        string url = string.Format("{0}GenerateItemsAtLocation?OwnerID={1}&OwnerType={2}&Location={3}&AppID={4}&MinimumEnergyOfItem={5}&TotalEnergyToGenerate={6}&ANDTags={7}&ORTags={8}", Url, user.sessionID, OwnerType, Location, GuidAppID, MinimumEnergyOfItem, TotalEnergyToGenerate, ANDTags, ORTags);

        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceCallGetListItemDatas(www, callback));
    }

    /// <summary>
    /// Loads item list from the specified owner.
    /// </summary>
    /// <param name="callback"></param>

    static public void GetItems(Action<List<ItemData>> callback)
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return;
        }
        GetOwnerItems(user.userID.ToString(), "User", 0, callback);
    }

    /// <summary>
    /// Loads item list from the specified owner.
    /// </summary>
    /// <param name="ownerID"></param>
    /// <param name="ownerType"></param>
    /// <param name="location"></param>
    /// <param name="callback"></param>

    static public void GetOwnerItems(string ownerID, string ownerType, int location, Action<List<ItemData>> callback)
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return;
        }
        if (string.IsNullOrEmpty(ownerID))
        {
            Debug.LogWarning("OwnerID cannot be empty");
            return;
        }
        string url = string.Format("{0}GetOwnerItems?ownerID={1}&ownerType={2}&location={3}&AppID={4}", Url, ownerID, ownerType, location, GuidAppID);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceCallGetListItemDatas(www, (List<ItemData> ownerItems) =>
        {
            userItems = ownerItems;
            if (callback != null) callback(userItems);
        }));
    }

    /// <summary>
    /// Returns List of item datas for the itemID 
    /// </summary>
    /// <param name="name"></param>
    static public void GetOwnerItemById(int location, int itemID, Action<List<ItemData>> callback)
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return;
        }
        if (string.IsNullOrEmpty(CloudGoods.user.userGuid))
        {
            Debug.LogWarning("OwnerID cannot be empty");
            return;
        }
        string url = string.Format("{0}GetOwnerItemById?AppID={1}&ownerID={2}&ownerType={3}&location={4}&itemID={5}", Url, AppID, user.userGuid, "User", location, itemID);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceCallGetListItemDatas(www, (List<ItemData> ownerItems) =>
        {
            userItems = ownerItems;
            if (callback != null) callback(userItems);
        }));
    }

    static public void ConsumeItemById(int ItemID, int ConsumeAmount, int Location, Action<ConsumeResponse> callback)
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return;
        }
        if (string.IsNullOrEmpty(CloudGoods.user.userGuid))
        {
            Debug.LogWarning("OwnerID cannot be empty");
            return;
        }
        string url = string.Format("{0}ConsumeItemById?AppID={1}&OwnerID={2}&OwnerType={3}&ItemID={4}&ConsumeAmount={5}&Location={6}", Url, AppID, user.userGuid, "User", ItemID, ConsumeAmount, Location);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceConsumeResponse(www, (ConsumeResponse ownerItems) =>
        {
            if (callback != null) callback(ownerItems);
        }));
    }

    /// <summary>
    /// Returns true if the user have the specified item.
    /// </summary>
    /// <param name="name"></param>

    static public bool HasItem(string name)
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return false;
        }
        if (userItems == null)
        {
            Debug.LogWarning("User Item list has not been loaded yet.");
            return false;
        }

        for (int i = 0, imax = userItems.Count; i < imax; i++)
        {
            if (userItems[i].itemName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns a single item from the user item list by its id
    /// </summary>
    /// <param name="name"></param>

    static public ItemData GetItem(int id)
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return null;
        }

        if (userItems == null)
        {
            Debug.LogWarning("User Item list has not been loaded yet.");
            return null;
        }

        if (userItems.Count == 0)
        {
            Debug.LogWarning("User Item list is empty.");
            return null;
        }

        ItemData item = null;

        for (int i = 0, imax = userItems.Count; i < imax; i++)
        {
            //Debug.Log("UserItems " + userItems[i].itemName + " / " + userItems[i].stackSize + " / " + userItems[i].varianceID);
            if (userItems[i].ItemID == id)
            {
                item = userItems[i];
                break;
            }
        }

        return item;
    }

    /// <summary>
    /// Returns a single item from the user item list by its name
    /// </summary>
    /// <param name="name"></param>

    static public ItemData GetItem(string name)
    {
        if (!isLogged)
        {
            Debug.LogWarning("Need to login first to get items.");
            return null;
        }

        if (userItems == null)
        {
            Debug.LogWarning("User Item list has not been loaded yet.");
            return null;
        }

        if (userItems.Count == 0)
        {
            Debug.LogWarning("User Item list is empty.");
            return null;
        }

        ItemData item = null;

        for (int i = 0, imax = userItems.Count; i < imax; i++)
        {
            if (userItems[i].itemName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
            {
                item = userItems[i];
                break;
            }
        }

        return item;
    }

    static public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, int DestinationLocation, Action<Guid> callback)
    {
        Debug.Log("Stack to move: " + StackToMove + " move amount: " + MoveAmount);

        string url = string.Format("{0}MoveItemStack?StackToMove={1}&MoveAmount={2}&DestinationOwnerID={3}&DestinationOwnerType={4}&AppID={5}&DestinationLocation={6}", Url, StackToMove, MoveAmount, DestinationOwnerID, DestinationOwnerType, GuidAppID, DestinationLocation);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetGuid(www, callback));
    }

    static public void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, int DestinationLocation, Action<MoveMultipleItemsResponse> callback)
    {
        string url = string.Format("{0}MoveItemStacks?stacks={1}&DestinationOwnerID={2}&DestinationOwnerType={3}&AppID={4}&DestinationLocation={5}", Url, stacks, DestinationOwnerID, DestinationOwnerType, GuidAppID, DestinationLocation);

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceMoveItemsResponse(www, callback));
    }

    static public void RemoveItemStack(Guid StackRemove, Action<string> callback)
    {
        string url = string.Format("{0}RemoveStackItem?stackID={1}", Url, StackRemove);

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string message) => { GetOwnerItems(user.userID.ToString(), "User", 0, OnItemsLoaded); GetStandardCurrencyBalance(0, null); if (callback != null) callback(message); }));
    }

    /// <summary>
    /// Consume the specified item and removes it from your item list.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="callback"></param>

    static public void UseItem(ItemData item, Action<string> callback)
    {
        if (item == null)
        {
            Debug.LogWarning("Item is null");
            return;
        }
        string url = string.Format("{0}DeductStackAmount?stackID={1}&amount={2}", Url, item.stackID, 1);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string message) => { GetOwnerItems(user.userID.ToString(), "User", 0, OnItemsLoaded); if (callback != null) callback(message); }));
    }

    static public void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback)
    {
        string url = string.Format("{0}DeductStackAmount?stackID={1}&amount={2}", Url, StackRemove, amount);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string message) => { GetOwnerItems(user.userID.ToString(), "User", 0, OnItemsLoaded); GetStandardCurrencyBalance(0, null); if (callback != null) callback(message); }));
    }

    static public void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback)
    {
        RemoveMultipleItems infos = new RemoveMultipleItems();
        infos.stacks = StacksToRemove;
        string stacksInfo = JsonMapper.ToJson(infos);
        string url = string.Format("{0}RemoveStackItems?stacks={1}", Url, stacksInfo);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string message) => { GetOwnerItems(user.userID.ToString(), "User", 0, OnItemsLoaded); GetStandardCurrencyBalance(0, null); if (callback != null) callback(message); }));
    }

    static public void GiveOwnerItems(string ownerID, WebModels.OwnerTypes OwnerType, List<WebModels.ItemsInfo> listOfItems, Action<string> callback)
    {
        GetToken(ownerID, "1", listOfItems, callback);
    }

    #endregion

    #region UserManagement

    static public void GetUserFromWorld(CloudGoodsPlatform platform, string platformUserID, string userName, string userEmail, Action<CloudGoodsUser> callback)
    {
        string url = Url + "GetUserFromWorld?appID=" + GuidAppID + "&platformID=" + (int)platform + "&platformUserID=" + platformUserID + "&userName=" + WWW.EscapeURL(userName) + "&loginUserEmail=" + userEmail;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetUserInfo(www, callback));
    }

    static public void RegisterGameSession(int instanceID, Action<Guid> callback)
    {
        string url = Url + "RegisterSession?UserId=" + user.userID + "&AppID=" + AppID.Trim() + "&InstanceId=" + instanceID;

		Debug.Log (url);

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

    static public void LoginWithPlatformUser(CloudGoodsPlatform platform, string platformUserID, string userName)
    {
        GetUserFromWorld(platform, platformUserID, userName, null, (CloudGoodsUser user) =>
        {
            AuthorizeUser(user);
        });
    }

    static public void Login(string userEmail, string password, Action<UserResponse> onSuccess)
    {
        Debug.Log("here");
        Debug.Log("appID:" + AppID);
        Debug.Log("guid app id: " + GuidAppID.ToString());

        string url = string.Format("{0}SPLoginUserLogin?gameID={1}&userEMail={2}&userPassword={3}", Url, GuidAppID.ToString(), WWW.EscapeURL(userEmail), WWW.EscapeURL(password));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceSpLoginResponse(www, (UserResponse response) =>
        {
            if (response.code == 7) if (onErrorEvent != null) onErrorEvent("ServerRelatedError");
            if (response.code == 0)
            {
                if (OnUserInfo != null)
                {

                    Debug.Log(response.message);

                    JsonData data = LitJson.JsonMapper.ToObject(response.message);

                    LoginUserInfo userInfo = new LoginUserInfo(new Guid(data["ID"].ToString()), data["name"].ToString(), data["email"].ToString());

                    //UserResponse responce = new UserResponse(int.Parse(data["code"].ToString()), data["message"].ToString(), userInfo);

                    CloudGoodsUser ui = new CloudGoodsUser(userInfo.ID.ToString(), userInfo.name, userInfo.email);
                    AuthorizeUser(ui);
                    OnUserInfo(ui);
                }
            }
            else
            {
                if (OnUserLogin != null) OnUserLogin(response);
                if (onSuccess != null) onSuccess(response);
            }
        }));
    }

    static public void Register(string userEmail, string password, string userName, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}SPLoginUserRegister?gameID={1}&userEMail={2}&userPassword={3}&userName={4}", Url, GuidAppID, WWW.EscapeURL(userEmail), WWW.EscapeURL(password), WWW.EscapeURL(userName));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceSpLoginResponse(www, (UserResponse response) =>
        {
            if (response.code == 7) if (onErrorEvent != null) onErrorEvent("ServerRelatedError");
            if (OnUserRegister != null) OnUserRegister(response);
            if (onSuccess != null) onSuccess(response);
        }));
    }

    static public void ForgotPassword(string userEmail, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}SPLoginForgotPassword?gameID={1}&userEMail={2}", Url, GuidAppID, WWW.EscapeURL(userEmail));
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string response) =>
        {
            if (response.Contains("Password reset E-mail has been sent"))
                onSuccess(null);
        }));
    }


    static public void ResendVerificationEmail(string userEmail, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}ResendVerificationEmail?gameID={1}&userEMail={2}", Url, GuidAppID, WWW.EscapeURL(userEmail));
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
        if (onLogout != null)
        {
            onLogout();
        }


        user = null;
    }

    #endregion

    #region Texture Cache

    static public Dictionary<string, Texture2D> ItemTextures = new Dictionary<string, Texture2D>();

    /// <summary>
    /// Loads item image from URL.
    /// </summary>
    /// <param name="URL"></param>
    /// <param name="callback"></param>

    static public void GetItemTexture(string URL, Action<ImageStatus, Texture2D> callback)
    {
        try
        {
            if (ItemTextures.ContainsKey(URL))
            {
                callback(ImageStatus.Cache, ItemTextures[URL]);
            }
            else
                GetItemTextureFromWeb(URL, callback);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            callback(ImageStatus.Error, null);
        }
    }

    static void GetItemTextureFromWeb(string URL, Action<ImageStatus, Texture2D> callback)
    {
        WWW www = new WWW(URL);

        Get().StartCoroutine(Get().OnReceivedItemTexture(www, callback, URL));
    }

    IEnumerator OnReceivedItemTexture(WWW www, Action<ImageStatus, Texture2D> callback, string imageURL)
    {
        yield return www;

        if (www.error == null)
        {
            if (ItemTextures.ContainsKey(imageURL))
            {
                callback(ImageStatus.Cache, ItemTextures[imageURL]);
            }
            else
            {
                ItemTextures.Add(imageURL, www.texture);
                callback(ImageStatus.Web, www.texture);
            }
        }
        else
        {
            if (CloudGoodsSettings.DefaultTexture != null)
                callback(ImageStatus.Cache, CloudGoodsSettings.DefaultTexture);
            else
                callback(ImageStatus.Error, null);
        }
    }

    #endregion

    #region StoreCalls

    static public void ConsumePremiumCurrency(int amount, Action<ConsumeResponse> callback)
    {
        string url = Url + "ConsumePremiumCurrency?AppID=" + AppID + "&UserID=" + user.userGuid + "&Amount=" + amount;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceConsumeResponse(www, (ConsumeResponse value) =>
        {
            if (callback != null) callback(value);
        }));
    }

    static public void GetStandardCurrencyBalance(int accessLocation, Action<int> callback)
    {
        string url = Url + "GetFreeCurrencyBalance?userID=" + user.userID.ToString() + "&accessLocation=" + accessLocation + "&appID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string value) =>
        {
            standardCurrency = System.Convert.ToInt32(value);
            if (callback != null) callback(standardCurrency);
        }));
    }

    static public void GetPremiumCurrencyBalance(Action<int> callback)
    {
        string url = Url + "GetPaidCurrencyBalance?userID=" + user.userID.ToString() + "&appID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string value) =>
        {
            premiumCurrency = System.Convert.ToInt32(value);
            if (callback != null) callback(premiumCurrency);
        }));
    }


    static public void GetWorldCurrencyInfo(Action<WorldCurrencyInfo> callback)
    {
        if (string.IsNullOrEmpty(AppID))
            return;

        string url = Url + "GetCurrencyInfo?AppID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string value) =>
        {
            WorldCurrencyInfo worldCurrencyInfo = serviceConverter.ConvertToWorldCurrencyInfo(value);

            if (callback != null) callback(worldCurrencyInfo);

            if (!string.IsNullOrEmpty(worldCurrencyInfo.FreeCurrencyName))
            {
                sfree = worldCurrencyInfo.FreeCurrencyName;
                if (OnStandardCurrencyName != null) OnStandardCurrencyName(sPaid);
            }

            if (!string.IsNullOrEmpty(worldCurrencyInfo.PaidCurrencyName))
            {
                sPaid = worldCurrencyInfo.PaidCurrencyName;
                if (OnPremiumCurrencyName != null) OnPremiumCurrencyName(sPaid);
            }

            if (!string.IsNullOrEmpty(worldCurrencyInfo.PaidCurrencyImage))
            {
                CloudGoods.GetItemTexture(worldCurrencyInfo.PaidCurrencyImage, delegate(ImageStatus imageStatus, Texture2D texture)
                {
                    tPaid = texture;
                    if (OnPremiumCurrencyTexture != null) OnPremiumCurrencyTexture(texture);
                });
            }

            if (!string.IsNullOrEmpty(worldCurrencyInfo.FreeCurrencyImage))
            {
                CloudGoods.GetItemTexture(worldCurrencyInfo.FreeCurrencyImage, delegate(ImageStatus imageStatus, Texture2D texture)
                {
                    tFree = texture;
                    if (OnStandardCurrencyTexture != null) OnStandardCurrencyTexture(texture);
                });
            }
        }));
    }

    /// <summary>
    /// Loads and return the list of store items from the server.
    /// </summary>
    /// <param name="callback"></param>

    static public void GetStoreItems(Action<List<StoreItem>> callback)
    {
        string url = Url + "LoadStoreItems?appID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetStoreItems(www, (List<StoreItem> items) =>
        {
            storeItems = items;
            if (OnStoreListLoaded != null) OnStoreListLoaded(storeItems);
            if (callback != null) callback(storeItems);
        }));
    }

    /// <summary>
    /// Returns an item from the store by its id.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>

    static public StoreItem GetStoreItem(int itemID)
    {
        if (storeItems == null)
        {
            Debug.LogWarning("Store Item list has not been loaded yet.");
            return null;
        }

        StoreItem item = null;

        for (int i = 0, imax = storeItems.Count; i < imax; i++)
        {
            if (storeItems[i].itemID == itemID)
            {
                item = storeItems[i];
                break;
            }
        }

        return item;
    }

    /// <summary>
    /// Returns an item from the store by its name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>

    static public StoreItem GetStoreItem(string name)
    {
        if (storeItems == null)
        {
            Debug.LogWarning("Store Item list has not been loaded yet.");
            return null;
        }

        StoreItem item = null;

        for (int i = 0, imax = storeItems.Count; i < imax; i++)
        {
            if (storeItems[i].itemName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
            {
                item = storeItems[i];
                break;
            }
        }

        return item;
    }

    /// <summary>
    /// Purchase an item from the store.
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="amount"></param>
    /// <param name="paymentType"></param>

    static public void StoreItemPurchase(int itemID, int amount, CurrencyType paymentType)
    {
        StoreItemPurchase(itemID, amount, paymentType, 0, null);
    }

    /// <summary>
    /// Purchase an item from the store.
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="amount"></param>
    /// <param name="paymentType"></param>
    /// <param name="saveLocation"></param>

    static public void StoreItemPurchase(int itemID, int amount, CurrencyType paymentType, int saveLocation)
    {
        StoreItemPurchase(itemID, amount, paymentType, saveLocation, null);
    }

    /// <summary>
    /// Purchase an item from the store.
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="amount"></param>
    /// <param name="paymentType"></param>
    /// <param name="saveLocation"></param>
    /// <param name="callback"></param>

    static public void StoreItemPurchase(int itemID, int amount, CurrencyType paymentType, int saveLocation, Action<string> callback)
    {
        string url = Url + "StoreItemPurchase?UserID=" + user.userID + "&ItemID=" + itemID + "&Amount=" + amount + "&PaymentType=" + paymentType + "&AppID=" + GuidAppID + "&saveLocation=" + saveLocation;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string message) =>
        {
            GetStandardCurrencyBalance(0, null);
            GetPremiumCurrencyBalance(null);
            if (callback != null) callback(message);
        }));
    }

    static public void GetItemBundles(Action<List<ItemBundle>> callback)
    {
        string url = Url + "GetItemBundles?Appid=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetItemBundles(www, callback));
    }

    static public void PurchaseItemBundles(int bundleID, CurrencyType paymentType, int location, Action<string> callback)
    {
        string url = Url + "PurchaseItemBundle?AppID=" + GuidAppID + "&UserID=" + user.userID + "&BundleID=" + bundleID + "&PaymentType=" + paymentType + "&Location=" + location;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string message) =>
        {
            JsonData response = LitJson.JsonMapper.ToObject(message);

            GetOwnerItems(user.userID.ToString(), "User", 0, OnItemsLoaded);
            GetStandardCurrencyBalance(0, null);
            GetPremiumCurrencyBalance(null);
            if (callback != null) callback(message);
        }));
    }

    static public void GetCreditBundles(int platformID, Action<List<PaidCurrencyBundleItem>> callback)
    {
        string url = Url + "GetCreditBundles?Appid=" + AppID + "&Platform=" + platformID;

        WWW www = new WWW(url);

		Debug.Log ("URL get credit bundles:" + url);

        Get().StartCoroutine(Get().ServiceGetCreditBundles(www, callback));
    }

    static public void PurchaseCreditBundles(string payload, Action<string> callback)
    {
        string url = Url + "PurchaseCreditBundle?AppID=" + GuidAppID + "&payload=" + WWW.EscapeURL(EncryptStringUnity(payload));

        WWW www = new WWW(url);

		Debug.Log ("Purchase bundles url: " + url);

        Get().StartCoroutine(Get().ServiceGetString(www, (string message) =>
        {
			Debug.Log("PUrchase credit bundles callback: : " + message);
            JsonData response = LitJson.JsonMapper.ToObject(message);

            GetStandardCurrencyBalance(0, null);
            GetPremiumCurrencyBalance(null);
            if (callback != null) callback(message);
        }));
    }

    static public void GetToken(string ownerID, string tokenType, List<WebModels.ItemsInfo> items, Action<string> callback)
    {
        string url = Url + "GetToken?appID=" + AppID + "&payload=" + WWW.EscapeURL(EncryptStringUnity(tokenType));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, (x) =>
        {
            SecureCall(DecryptString(AppSecret, x.Replace("\\", "")), ownerID, items, callback);
        }));
    }

    /// <summary>
    /// Search store items with given string.
    /// </summary>
    /// <param name="storeItems"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>

    static public List<StoreItem> SearchStoreItems(List<StoreItem> storeItems, string searchFilter)
    {
        if (searchFilter.Length == 0)
        {
            return storeItems;
        }

        List<StoreItem> filteredStoreItems = new List<StoreItem>();
        for (int i = 0, imax = storeItems.Count; i < imax; i++)
        {
            StoreItem storeItemInfo = storeItems[i];
            if (storeItemInfo.itemName.StartsWith(searchFilter.ToLower(), StringComparison.InvariantCultureIgnoreCase)) filteredStoreItems.Add(storeItemInfo);
        }
        return filteredStoreItems;
    }

    static public void SecureCall(string token, string ownerID, List<WebModels.ItemsInfo> items, Action<string> callback)
    {
        GiveOwnerItemWebserviceRequest request = new GiveOwnerItemWebserviceRequest();
        request.listOfItems = items;
        request.ownerID = ownerID;
        request.appID = AppID;
        request.OwnerType = WebModels.OwnerTypes.User;

        string newStringRequest = JsonMapper.ToJson(request);

        SecurePayload payload = new SecurePayload();
        payload.token = token;
        payload.data = newStringRequest;

        string securePayloadString = JsonMapper.ToJson(payload);

        Debug.Log(securePayloadString);

        string url = Url + "SecureAction?appID=" + AppID + "&payload=" + WWW.EscapeURL(EncryptStringUnity(securePayloadString));

        WWW www = new WWW(url);



        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    #endregion

    #region RecipeCalls
    static public void GetGameRecipes(Action<List<RecipeInfo>> callback)
    {
        string url = Url + "GetRecipes?appID=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetRecipeInfos(www, callback));
    }

    static public void CompleteQueueItem(int QueueID, int percentScore, int location, Action<string> callback)
    {
        string url = string.Format("{0}CompleteQueueItem?gameID={1}&QueueID={2}&percentScore={3}&location={4}", Url, GuidAppID, QueueID, percentScore, location);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void AddInstantCraftToQueue(int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback)
    {
        string url = string.Format("{0}AddInstantCraftToQueue?gameID={1}&UserID={2}&ItemID={3}&Amount={4}&ItemIngredients={5}", Url, GuidAppID, user.userID, ItemID, Amount, WWW.EscapeURL(JsonMapper.ToJson(ItemIngredients)));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void GetCraftQueue(Action<string> callback)
    {
        string url = string.Format("{0}GetCraftQueue?AppID={1}&UserID={2}", Url, GuidAppID, user.userID);

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    #endregion

    #region Item Data Calls

    static public void SaveUserData(string Key, string Value, Action<bool> callback)
    {
        string url = string.Format("{0}SaveUserData?appId={1}&UserID={2}&Key={3}&Value={4}", Url, AppID, user.userID, WWW.EscapeURL(Key), WWW.EscapeURL(Value));
        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceGetBool(www, callback));
    }



    static public void RetriveUserDataValue(string Key, Action<string> callback)
    {
        string url = string.Format("{0}RetriveUserDataValue?appId={1}&UserID={2}&Key={3}", Url, AppID, user.userID, WWW.EscapeURL(Key));
        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceGetString(www, callback));
    }

    static public void DeleteUserDateValue(string Key, Action<bool> callback)
    {
        string url = string.Format("{0}DeleteUserDateValue?appId={1}&UserID={2}&Key={3}", Url, AppID, user.userID, WWW.EscapeURL(Key));
        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceGetBool(www, callback));
    }

    static public void RetriveAllUserDataValues(Action<Dictionary<string, string>> callback)
    {
        string url = string.Format("{0}RetriveAllUserDataValues?appId={1}&UserID={2}", Url, AppID, user.userID);
        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceGetDictionary(www, callback));
    }

    static public void RetriveAllUserDataOfKey(string Key, Action<List<UserDataValue>> callback)
    {
        string url = string.Format("{0}RetriveAllUserDataOfKey?appId={1}&UserID={2}&Key={3}", Url, AppID, user.userID, WWW.EscapeURL(Key));
        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceUserDataValueResponse(www, callback));
    }

    #endregion

    #region IEnumeratorCalls

    IEnumerator ServiceGetString(WWW www, Action<string> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToString(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceGetBool(WWW www, Action<bool> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToBool(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceGetDictionary(WWW www, Action<Dictionary<string, string>> callback)
    {
        yield return www;
        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToDictionary(www.text));
            }
            catch
            {             
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceGetUserInfo(WWW www, Action<CloudGoodsUser> callback)
    {
        yield return www;

        if (www.error == null)
        {
            try
            {
				Debug.Log("User info received: " + www.text);
                callback(serviceConverter.ConvertToUserInfo(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            Debug.Log(www.error);
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceCallGetListItemDatas(WWW www, Action<List<ItemData>> callback)
    {
        yield return www;

        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToItemDataList(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceGetStoreItems(WWW www, Action<List<StoreItem>> callback)
    {
        yield return www;

        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToStoreItems(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceGetGuid(WWW www, Action<Guid> callback)
    {
        yield return www;

        if (www.error == null)
        {
			Debug.Log("Service get guid: " + www.text);

            try
            {
                callback(serviceConverter.ConvertToGuid(www.text));
            }
            catch (Exception ex)
            {
                Debug.LogError("Returned text: " + www.text + " Error: " + ex.Message);
            }
        }
        else
        {
			Debug.LogError("Error: " + www.error);
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }

        GetStandardCurrencyBalance(0, null);
    }

    IEnumerator ServiceGetRecipeInfos(WWW www, Action<List<RecipeInfo>> callback)
    {
        yield return www;

        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToListRecipeInfo(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceGetItemBundles(WWW www, Action<List<ItemBundle>> callback)
    {
        yield return www;

        if (www.error == null)
        {
            try
            {
                Debug.Log(www.text);
                List<ItemBundle> itemBundles = serviceConverter.ConvertToListItemBundle(www.text);
                if (OnStoreItemBundleListLoaded != null)
                {
                    OnStoreItemBundleListLoaded(itemBundles);
                }

                if (callback != null)
                    callback(itemBundles);
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceGetCreditBundles(WWW www, Action<List<PaidCurrencyBundleItem>> callback)
    {
        yield return www;
        Debug.Log(www.text);
        if (www.error == null)
            try
            {
                callback(serviceConverter.ConvertToListPaidCurrencyBundleItem(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceMoveItemsResponse(WWW www, Action<MoveMultipleItemsResponse> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToMoveMultipleItemsResponse(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }

        GetStandardCurrencyBalance(0, null);
    }

    IEnumerator ServiceSpLoginResponse(WWW www, Action<UserResponse> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToSPLoginResponse(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceConsumeResponse(WWW www, Action<ConsumeResponse> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConverToConsumeCreditsResponse(www.text));
            }
            catch
            {
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
        }
    }

    IEnumerator ServiceUserDataValueResponse(WWW www, Action<List<UserDataValue>> callback)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            try
            {
                callback(serviceConverter.ConvertToUserDataValueList(www.text));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError(www.text);
            }
        }
        else
        {
            if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
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

    #region Utils


    /// <summary>
    /// Returns domain from string.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    ///
    static public string GetDomain(string url)
    {
        if (url.StartsWith("file://"))
            return "localhost";

        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            return "unknown";

        // convert to lower-case
        url = url.ToLower();

        // a) remove http:// or https://
        if (url.StartsWith("http://"))
            url = url.Remove(0, 7);
        else if (url.StartsWith("https://"))
            url = url.Remove(0, 8);

        // b) remove path
        int pos = url.IndexOf("/");
        if (pos != -1)
            url = url.Remove(pos, url.Length - pos);

        // c) remove port if present
        pos = url.IndexOf(":");
        if (pos != -1)
            url = url.Remove(pos, url.Length - pos);

        // d) remove sub-domain if present
        pos = url.IndexOf(".");
        while (pos != -1)
        {
            string tempUrl = url.Remove(0, pos + 1);
            pos = tempUrl.IndexOf(".");
            if (pos != -1)
            {
                url = tempUrl;
            }
        }

        return url;
    }

    #endregion
}
