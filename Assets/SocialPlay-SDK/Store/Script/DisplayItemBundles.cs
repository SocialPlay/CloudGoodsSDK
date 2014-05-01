using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class DisplayItemBundles : MonoBehaviour {

    public List<ItemBundle> ItemBundles = new List<ItemBundle>();

	void Start () {
	    GameAuthentication.OnUserAuthEvent += OnReceivedUserAuth;
	}

    void OnReceivedUserAuth(string data)
    {
        GetItemBundles();
    }

    public void GetItemBundles()
    {
        WebserviceCalls.webservice.GetItemBundles(ItemSystemGameData.AppID.ToString(), OnReceivedItemBundles);
    }

    void OnReceivedItemBundles(string data)
    {

        Debug.Log(data);

        ItemBundles = new List<ItemBundle>();

        JToken itemBundleParse = JToken.Parse(data);
        JArray itemBundleObj = JArray.Parse(itemBundleParse.ToString());

        for (int i = 0; i < itemBundleObj.Count; i++)
        {
            ItemBundle itemBundle = JsonConvert.DeserializeObject<ItemBundle>(itemBundleObj[i].ToString());

            JArray BundleItemArray = JArray.Parse(itemBundleObj[i]["items"].ToString());

            for (int j = 0; j < BundleItemArray.Count; j++)
            {
                BundleItem bundleItem = JsonConvert.DeserializeObject<BundleItem>(BundleItemArray[j].ToString());

                JArray bundleDetailsArray = JArray.Parse(BundleItemArray[j]["Detail"].ToString());

                for (int k = 0; k < bundleDetailsArray.Count; k++)
                {
                    BundleItemDetails bundleDetails = JsonConvert.DeserializeObject<BundleItemDetails>(bundleDetailsArray[k].ToString());

                    Debug.Log(bundleDetails.Name);

                    bundleItem.bundleItemDetails.Add(bundleDetails);

                    Debug.Log(bundleItem.bundleItemDetails.Count);
                }

                itemBundle.bundleItems.Add(bundleItem);
            }

            ItemBundles.Add(itemBundle);
        }

        Debug.Log(ItemBundles[0].bundleItems[0].bundleItemDetails.Count);

    }
}

public class ItemBundle
{
    public int ID;
    public int CreditPrice;
    public int CoinPrice;
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

    public string Name;
}
