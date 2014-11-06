using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityUIItemBundleLoader : MonoBehaviour {

    public UnityUIBundlePurchasing bundlePurchasing;
    public GameObject ItemBundleButtonObject;

    public GameObject gridObject;

    void Awake()
    {
        CloudGoods.OnStoreItemBundleListLoaded += CloudGoods_OnStoreItemBundleListLoaded;
    }

    void CloudGoods_OnStoreItemBundleListLoaded(List<ItemBundle> obj)
    {
        LoadBundleItems(obj);
    }

    public void LoadBundleItems(List<ItemBundle> itemBundles)
    {
        foreach (ItemBundle bundle in itemBundles)
        {
            GameObject newItemBundle = (GameObject)GameObject.Instantiate(ItemBundleButtonObject);
            UnityUIItemBundle ItemBundle = newItemBundle.GetComponent<UnityUIItemBundle>();

            newItemBundle.transform.parent = gridObject.transform;
            newItemBundle.transform.localScale = new Vector3(1, 1, 1);

            ItemBundle.SetupUnityUIItemBundle(bundle, bundlePurchasing);
        }
    }

}
