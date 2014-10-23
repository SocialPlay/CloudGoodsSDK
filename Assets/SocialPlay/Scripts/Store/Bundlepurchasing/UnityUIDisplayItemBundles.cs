using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityUIDisplayItemBundles : MonoBehaviour {

    public UnityUIItemBundleLoader bundleLoader;

    public List<ItemBundle> ItemBundles = new List<ItemBundle>();

    public void GetItemBundles()
    {
        CloudGoods.GetItemBundles(OnReceivedItemBundles);
    }

    void OnReceivedItemBundles(List<ItemBundle> newItemBundles)
    {
        bundleLoader.LoadBundleItems(newItemBundles);
    }
}
