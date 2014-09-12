using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using SocialPlay.Generic;



class UIGridLoader : MonoBehaviour, IGridLoader
{
    public Transform grid = null;
    public GameObject itemPrefab = null;


    public event Action<PaidCurrencyBundleItem, GameObject> ItemAdded;

    public void LoadGrid(List<PaidCurrencyBundleItem> PaidCurrenyBundles)
    {
        foreach (PaidCurrencyBundleItem PaidCurrencyBundle in PaidCurrenyBundles)
        {
            GameObject gItem = Instantiate(itemPrefab) as GameObject;
            gItem.transform.SetParent(grid, false);
            if (ItemAdded != null)
                ItemAdded(PaidCurrencyBundle, gItem);
        }
    }
}