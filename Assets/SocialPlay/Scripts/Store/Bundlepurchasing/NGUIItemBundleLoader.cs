using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGUIItemBundleLoader : MonoBehaviour {


    public BundlePurchasing bundlePurchasing;
    public GameObject ItemBundleButtonObject;

    public void LoadBundleItems(List<ItemBundle> itemBundles)
    {
        UIGrid bundleGrid = GetComponent<UIGrid>();

        foreach (ItemBundle bundle in itemBundles)
        {
            GameObject newItemBundle = (GameObject)GameObject.Instantiate(ItemBundleButtonObject);
            NGUIItemBundle nguiItemBundle = newItemBundle.GetComponent<NGUIItemBundle>();

            newItemBundle.transform.parent = transform;
            newItemBundle.transform.localScale = new Vector3(1, 1, 1);

            nguiItemBundle.SetupNGUIItemBundle(bundle, bundlePurchasing);
        }

        bundleGrid.repositionNow = true;
    }
}
