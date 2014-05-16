using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class DisplayItemBundles : MonoBehaviour {

    public NGUIItemBundleLoader bundleLoader;

    public List<ItemBundle> ItemBundles = new List<ItemBundle>();

	void Start () {
	    GameAuthentication.OnRegisteredUserToSession += OnReceivedUserAuth;
	}

    void OnReceivedUserAuth(string data)
    {
        GetItemBundles();
    }

    public void GetItemBundles()
    {
        WebserviceCalls.webservice.GetItemBundles(ItemSystemGameData.AppID.ToString(), OnReceivedItemBundles);
    }

    void OnReceivedItemBundles(List<ItemBundle> newItemBundles)
    {
        bundleLoader.LoadBundleItems(newItemBundles);
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
    public int State;

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
