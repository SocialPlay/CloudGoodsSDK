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

public class ItemBundle
{
    public int ID;
    public int CreditPrice;
    public int CoinPrice;

    //State 1 = Credit and Coin Purchaseable
    //State 2 = Credit purchase only
    //State 3 = Coin Purchase only
    //State 4 = Free
    public SPBundle State;

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
