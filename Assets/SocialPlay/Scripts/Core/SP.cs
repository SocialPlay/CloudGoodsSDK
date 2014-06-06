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
    #region Global Events/Callbacks

    static public Action<string> onErrorEvent;
    static public event Action<UserResponse> OnUserLogin;
    //static public event Action onLogout;
	static public event Action<SocialPlayUser> OnUserInfo;
    static public event Action<UserResponse> OnUserRegister;
    static public event Action<UserResponse> OnForgotPassword;
    static public event Action<UserResponse> OnVerificationSent;
    static public event Action<string> OnRegisteredUserToSession;
	static public event Action<SocialPlayUser> OnUserAuthorized;
	static public event Action<List<StoreItem>> OnStoreListLoaded;
	static public event Action<int> OnFreeCurrency;
	static public event Action<int> OnPaidCurrency;

    #endregion

    static public IServiceObjectConverter serviceConverter { get { if (mService == null) mService = new LitJsonFxJsonObjectConverter(); return mService; } }    

    /// <summary>
    /// Returns the AppSecret saved on settings.
    /// </summary>

    static public string AppSecret 
	{ 
		get 
		{
			if (string.IsNullOrEmpty(SocialPlaySettings.AppSecret))
				Debug.LogError("AppSecret has not been defined. Open Social Play Settings from the menu.");

			return SocialPlaySettings.AppSecret; 
		} 
	}

    /// <summary>
    /// Returns the AppID saved on settings.
    /// </summary>

	static public string AppID 
	{ 
		get 
		{ 
			if (string.IsNullOrEmpty(SocialPlaySettings.AppID)) 
				Debug.LogError("AppID has not been defined. Open Social Play Settings from the menu.");

			return SocialPlaySettings.AppID; 
		} 
	}

    /// <summary>
    /// Returns the URL saved on settings.
    /// </summary>
    
    static public string Url 
	{ 
		get 
		{
			if (string.IsNullOrEmpty(SocialPlaySettings.Url))
				Debug.LogError("Url has not been defined. Open Social Play Settings from the menu.");

			return SocialPlaySettings.Url; 
		} 
	}

    /// <summary>
    /// Returns the Asset Bundles URL saved on settings.
    /// </summary>

    static public string BundlesUrl 
	{ 
		get 
		{
			if (string.IsNullOrEmpty(SocialPlaySettings.BundlesUrl))
				Debug.LogError("BundlesUrl has not been defined. Open Social Play Settings from the menu.");

			return SocialPlaySettings.BundlesUrl; 
		} 
	}

    /// <summary>
    /// Returns a the AppID inside a new Guid. If you need string only use AppID instead.
    /// </summary>
    /// <returns></returns>

    static public Guid GuidAppID
    {
		get { return new Guid(AppID); }
    }

	/// <summary>
	/// Cached store items list.
	/// </summary>

	static public List<StoreItem> storeItems { get; private set; }

	/// <summary>
	/// Current amount of free currency. You can listen to the event OnFreeCurrency which will be triggered everytime this value changes.
	/// </summary>

	static public int freeCurrency
	{ 
		get { return mFree; }
		private set
		{
			if (mFree != value)
			{
				mFree = value;
				if (OnFreeCurrency != null) OnFreeCurrency(mFree);
			}
		}
	}

	/// <summary>
	/// Current amount of paid currency. You can listen to the event OnPaidCurrency which will be triggered everytime this value changes.
	/// </summary>

	static public int paidCurrency
	{
		get { return mPaid; }
		private set
		{
			if (mPaid != value)
			{
				mPaid = value;
				if (OnPaidCurrency != null) OnPaidCurrency(mPaid);
			}
		}
	}

	/// <summary>
	/// Current user information.
	/// </summary>

	static public SocialPlayUser user { get; private set; }

    #region Private Members

    static IServiceObjectConverter mService;

	static int mFree = 0;
	static int mPaid = 0;
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
	static string buyUrl = "http://socialplaywebservice.azurewebsites.net/publicservice.svc/";

    #endregion

    #region Game Authentication

	static public void AuthorizeUser(SocialPlayUser userInfo)
    {
		user = userInfo;
		user.userID = new Guid(user.userGuid.ToString());
		user.sessionID = Guid.NewGuid();

		GetStoreItems(OnStoreListLoaded);
		GetFreeCurrencyBalance(0, null);
		GetPaidCurrencyBalance(null);

        if (OnUserAuthorized != null)
			OnUserAuthorized(user);

        SP.RegisterGameSession(1, (Guid sessionGuid) =>
        {			
            ItemSystemGameData.SessionID = sessionGuid;
            if (OnRegisteredUserToSession != null) OnRegisteredUserToSession(user.userID.ToString());
        });

    }

    #endregion

    #region ItemContainerManagementCalls

    static public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<List<ItemData>> callback, string ANDTags = "", string ORTags = "")
    {
        string url = string.Format("{0}GenerateItemsAtLocation?OwnerID={1}&OwnerType={2}&Location={3}&AppID={4}&MinimumEnergyOfItem={5}&TotalEnergyToGenerate={6}&ANDTags={7}&ORTags={8}", Url, OwnerID, OwnerType, Location, GuidAppID, MinimumEnergyOfItem, TotalEnergyToGenerate, ANDTags, ORTags);
        
        WWW www = new WWW(url);
        Get().StartCoroutine(Get().ServiceCallGetListItemDatas(www, callback));
    }

    static public void GetOwnerItems(string ownerID, string ownerType, int location, Action<List<ItemData>> callback)
    {
		string url = string.Format("{0}GetOwnerItems?ownerID={1}&ownerType={2}&location={3}&AppID={4}", Url, ownerID, ownerType, location, GuidAppID);
        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceCallGetListItemDatas(www, callback));
    }

    static public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, int DestinationLocation, Action<Guid> callback)
    {
        Debug.Log(StackToMove.ToString());

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

    static public void GiveOwnerItems(string ownerID, WebModels.OwnerTypes OwnerType, List<WebModels.ItemsInfo> listOfItems, Action<string> callback)
    {
		GetToken(ownerID, "1", listOfItems, callback);
    }

    #endregion

    #region UserManagement

    static public void GetUserFromWorld(SocialPlayPlatform platform, string platformUserID, string userName, string userEmail, Action<SocialPlayUser> callback)
    {
		string url = Url + "GetUserFromWorld?appID=" + GuidAppID + "&platformID=" + (int)platform + "&platformUserID=" + platformUserID + "&userName=" + WWW.EscapeURL(userName) + "&loginUserEmail=" + userEmail;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetUserInfo(www, callback));
    }

    static public void RegisterGameSession(int instanceID, Action<Guid> callback)
    {
        string url = Url + "RegisterSession?UserId=" + user.userID + "&AppID=" + AppID + "&InstanceId=" + instanceID;

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

	static public void LoginWithPlatformUser(SocialPlayPlatform platform, string platformUserID, string userName)
	{
		GetUserFromWorld(platform, platformUserID, userName, null, (SocialPlayUser user) =>
		{
			AuthorizeUser(user);
		});
	}

    static public void Login(string userEmail, string password, Action<UserResponse> onSuccess)
    {
        string url = string.Format("{0}SPLoginUserLogin?gameID={1}&userEMail={2}&userPassword={3}", Url, GuidAppID, WWW.EscapeURL(userEmail), WWW.EscapeURL(password));

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceSpLoginResponse(www, (UserResponse response) =>
        {
            if (response.code == 7) if (onErrorEvent != null) onErrorEvent("ServerRelatedError");
            if (response.code == 0)
            {
                if (OnUserInfo != null)
                {
					LoginUserInfo userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginUserInfo>(response.message);
					SocialPlayUser ui = new SocialPlayUser(userInfo.ID.ToString(), userInfo.name, userInfo.email);
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
        string url = string.Format("{0}ForgotPassword?gameID={1}&userEMail={2}", Url, GuidAppID, WWW.EscapeURL(userEmail));
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

        new ItemSystemGameData(Guid.Empty.ToString(), Guid.Empty.ToString(), 0, Guid.Empty.ToString(), "", "");
    }

    #endregion

    #region StoreCalls

    static public void GetFreeCurrencyBalance(int accessLocation, Action<int> callback)
    {
		string url = Url + "GetFreeCurrencyBalance?userID=" + user.userID.ToString() + "&accessLocation=" + accessLocation + "&appID=" + AppID;

        WWW www = new WWW(url);

		Get().StartCoroutine(Get().ServiceGetString(www, (string value) => { freeCurrency = System.Convert.ToInt16(value); if (callback != null) callback(freeCurrency); }));
    }

    static public void GetPaidCurrencyBalance(Action<int> callback)
    {
		string url = Url + "GetPaidCurrencyBalance?userID=" + user.userID.ToString() + "&appID=" + AppID;

        WWW www = new WWW(url);

		Get().StartCoroutine(Get().ServiceGetString(www, (string value) => { paidCurrency = System.Convert.ToInt16(value); if (callback != null) callback(paidCurrency); }));
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

	static public void StoreItemPurchase(int itemID, int amount, CurrencyType paymentType, int saveLocation, Action<string> callback)
    {
		string url = buyUrl + "StoreItemPurchase?UserID=" + user.userID + "&ItemID=" + itemID + "&Amount=" + amount + "&PaymentType=" + paymentType + "&AppID=" + GuidAppID + "&saveLocation=" + saveLocation;

        WWW www = new WWW(url);
		//currencyBalance.SetItemPaidCurrency(dataObj["Balance"].ToString());
		Get().StartCoroutine(Get().ServiceGetString(www, (string message) => { GetFreeCurrencyBalance(0, null); GetPaidCurrencyBalance(null); if (callback != null) callback(message); }));
    }

    static public void GetItemBundles(Action<List<ItemBundle>> callback)
    {
        string url = Url + "GetItemBundles?Appid=" + AppID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetItemBundles(www, callback));
    }

	static public void PurchaseItemBundles(int bundleID, CurrencyType paymentType, int location, Action<string> callback)
    {
		string url = buyUrl + "PurchaseItemBundle?AppID=" + GuidAppID + "&UserID=" + user.userID + "&BundleID=" + bundleID + "&PaymentType=" + paymentType + "&Location=" + location;

        WWW www = new WWW(url);

		Get().StartCoroutine(Get().ServiceGetString(www, (string message) => { GetFreeCurrencyBalance(0, null); GetPaidCurrencyBalance(null); if (callback != null) callback(message); }));
    }

    static public void GetCreditBundles(int platformID, Action<List<CreditBundleItem>> callback)
    {
        string url = Url + "GetCreditBundles?Appid=" + AppID + "&Platform=" + platformID;

        WWW www = new WWW(url);

        Get().StartCoroutine(Get().ServiceGetCreditBundles(www, callback));
    }

    static public void PurchaseCreditBundles(string payload, Action<string> callback)
    {
		string url = Url + "PurchaseCreditBundle?AppID=" + GuidAppID + "&payload=" + WWW.EscapeURL(EncryptStringUnity(payload));

        WWW www = new WWW(url);

		Get().StartCoroutine(Get().ServiceGetString(www, (string message) => { GetFreeCurrencyBalance(0, null); GetPaidCurrencyBalance(null); if (callback != null) callback(message); }));
    }

	static public void GetToken(string ownerID, string tokenType, List<WebModels.ItemsInfo> items, Action<string> callback)
	{
		string url = Url + "GetToken?appID=" + AppID + "&payload=" + WWW.EscapeURL(EncryptStringUnity(tokenType));

		WWW www = new WWW(url);

		Get().StartCoroutine(Get().ServiceGetString(www, (x) =>
		{
			SecureCall(DecryptString(AppSecret, x.Replace("\\", "")), "", items, callback);
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
        string url = string.Format("{0}AddInstantCraftToQueue?gameID={1}&UserID={2}&ItemID={3}&Amount={4}&ItemIngredients={5}", Url, GuidAppID, user.userID, ItemID, Amount, WWW.EscapeURL(JsonConvert.SerializeObject(ItemIngredients)));

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

    IEnumerator ServiceGetUserInfo(WWW www, Action<SocialPlayUser> callback)
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
			if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
		}
    }


    IEnumerator ServiceCallGetListItemDatas(WWW www, Action<List<ItemData>> callback)
    {
        yield return www;

        if (www.error == null)
            callback(serviceConverter.ConvertToItemDataList(www.text));
		else
		{
			if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
		}
    }

    IEnumerator ServiceGetStoreItems(WWW www, Action<List<StoreItem>> callback)
    {
        yield return www;

        if (www.error == null)
            callback(serviceConverter.ConvertToStoreItems(www.text));
		else
		{
			if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
		}
    }

    IEnumerator ServiceGetGuid(WWW www, Action<Guid> callback)
    {
        yield return www;

		if (www.error == null)
			callback(serviceConverter.ConvertToGuid(www.text));
		else
		{
			if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
		}
    }

    IEnumerator ServiceGetRecipeInfos(WWW www, Action<List<RecipeInfo>> callback)
    {
        yield return www;

		if (www.error == null)
			callback(serviceConverter.ConvertToListRecipeInfo(www.text));
		else
		{
			if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
		}
    }

    IEnumerator ServiceGetItemBundles(WWW www, Action<List<ItemBundle>> callback)
    {
        yield return www;

		if (www.error == null)
			callback(serviceConverter.ConvertToListItemBundle(www.text));
		else
		{
			if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
		}
    }

    IEnumerator ServiceGetCreditBundles(WWW www, Action<List<CreditBundleItem>> callback)
    {
        yield return www;
        Debug.Log(www.text);
		if (www.error == null)
			callback(serviceConverter.ConvertToListCreditBundleItem(www.text));
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
            callback(serviceConverter.ConvertToMoveMultipleItemsResponse(www.text));
        }
        else
        {
			if (onErrorEvent != null) onErrorEvent("Error: " + www.error);
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
            if(onErrorEvent!= null) onErrorEvent("Error: " + www.error);
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
