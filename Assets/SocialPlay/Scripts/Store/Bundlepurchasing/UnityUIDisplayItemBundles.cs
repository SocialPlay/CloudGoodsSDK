using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityUIDisplayItemBundles : MonoBehaviour {

    public UnityUIItemBundleLoader bundleLoader;

    public List<ItemBundle> ItemBundles = new List<ItemBundle>();

    void Start()
    {
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
