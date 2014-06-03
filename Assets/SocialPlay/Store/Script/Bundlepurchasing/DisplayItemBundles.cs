using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class DisplayItemBundles : MonoBehaviour {

    public NGUIItemBundleLoader bundleLoader;

    public List<ItemBundle> ItemBundles = new List<ItemBundle>();

	void Start () {
	    SP.OnRegisteredUserToSession += OnReceivedUserAuth;
	}

    void OnReceivedUserAuth(string data)
    {
        GetItemBundles();
    }

    public void GetItemBundles()
    {
        SP.GetItemBundles(OnReceivedItemBundles);
    }

    void OnReceivedItemBundles(List<ItemBundle> newItemBundles)
    {
        bundleLoader.LoadBundleItems(newItemBundles);
    }
}