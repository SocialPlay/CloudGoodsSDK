using System;
using System.Collections.Generic;

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

public class SocialPlayUser
{
	public string userGuid = "";
	public bool isNewUserToWorld = false;
	public string userName = "";
	public string userEmail = "";
	public Guid sessionID;
	public Guid userID;

	public SocialPlayUser(string newUserGuid, string newUserName, string newUserEmail)
	{
		userGuid = newUserGuid;
		userName = newUserName;
		userEmail = newUserEmail;
	}
}

public class LoginUserInfo
{
	public Guid ID;
	public string name;
	public string email;

	public LoginUserInfo(Guid userID, string userName, string userEmail)
	{
		ID = userID;
		name = userName;
		email = userEmail;
	}
}

public class UserResponse
{
	public int code;
	public string message;
	public SocialPlayUser userInfo;

	public UserResponse(int caseCode, string msg, SocialPlayUser newUserInfo)
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

#region Bundles

public class CreditBundleItem
{
    public int Amount = 0;
    public string Cost = "";
    public int ID = 0;
    public string CurrencyName = "";
    public string CurrencyIcon = "";

    public Dictionary<string, string> CreditPlatformIDs = new Dictionary<string,string>();
}


public class ItemBundle
{
    public int ID;
    public int CreditPrice;
    public int CoinPrice;

    //State 1 = Credit and Coin Purchaseable
    //State 2 = Credit purchase only
    //State 3 = Coin Purchase only
    //State 4 = Free
    public SocialPlayBundle State;

    public string Name;
    public string Description;
    public string Image;

    public List<BundleItem> bundleItems = new List<BundleItem>();
}

public class BundleItem
{
    public int Quantity;
    public int Quality;

    public string Name;
    public string Image;
    public string Description;

    public List<BundleItemDetails> bundleItemDetails = new List<BundleItemDetails>();
}

public class BundleItemDetails
{
    public int Value;
    public string BundleDetailName;
}

public class BundlePurchaseRequest
{
    public int BundleID;
    public Guid UserID;
    public string ReceiptToken;
    public int PaymentPlatform;
}

#endregion

#region Items

public class StoreItem
{
	public int ID = 0;
	public string itemName = "";
	public List<StoreItemDetail> itemDetail = new List<StoreItemDetail>();
	public DateTime addedDate;
	public string behaviours;
	public List<string> tags;
	public int itemID = 0;
	public int creditValue = 0;
	public int coinValue = 0;
	public string imageURL = "";
}

public class StoreItemDetail
{
	public string propertyName;
	public int propertyValue;
	public bool invertEnergy;
}

#endregion

#region Recipes

public class RecipeInfo
{
	public int recipeID;
	public string name;
	public int energy;
	public string description;
	public string imgURL;

	public List<ItemDetail> RecipeDetails;
	public List<IngredientDetail> IngredientDetails;
}

public class IngredientDetail
{
	public string name;
	public int ingredientID;
	public int amount;
	public int energy;
	public string imgURL;
}

public class ItemDetail
{
	public string name;
	public float value;
}

#endregion

/*namespace CloudGoodsSDK.Models
{
	public class PlatformUser
	{
		public int platformID;
		public string platformUserID;
		public string userName;
	}
}*/
