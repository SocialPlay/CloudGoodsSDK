using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;



class NGUIGridLoader : MonoBehaviour, SocialPlay.Generic.IGridLoader
{
    public GameObject grid = null;
    public GameObject itemPrefab = null;

    public event Action<PaidCurrencyBundleItem, GameObject> ItemAdded;

    public void LoadGrid(List<PaidCurrencyBundleItem> PaidCurrenyBundles)
    {
        foreach (PaidCurrencyBundleItem PaidCurrencyBundle in PaidCurrenyBundles)
        {
            GameObject gItem = NGUITools.AddChild(grid, itemPrefab);
            if (ItemAdded != null)
                ItemAdded(PaidCurrencyBundle, gItem);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }
}

