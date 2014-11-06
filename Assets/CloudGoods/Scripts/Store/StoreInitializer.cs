using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class StoreInitializer : MonoBehaviour
{
    public FilterNewestItems.SortTimeType timeFilterType = FilterNewestItems.SortTimeType.hours;
    public int itemDisplayCount = 0;
    public int timeDifference = 5;

    FilterNewestItems newestItemFilter = new FilterNewestItems();
    List<StoreItem> storeItems = new List<StoreItem>();
    List<ItemBundle> itemBundles = new List<ItemBundle>();

    public void InitializeStore()
    {
        CloudGoods.GetStandardCurrencyBalance(0, null);
        CloudGoods.GetPremiumCurrencyBalance(null);

        CloudGoods.GetStoreItems(OnReceivedStoreItems);
        CloudGoods.GetItemBundles(null);

    }

    void OnReceivedStoreItemBundles(List<ItemBundle> newItemBundles)
    {
        itemBundles = newItemBundles;
    }

    void OnReceivedStoreItems(List<StoreItem> newStoreItems)
    {
        for (int i = 0; i < newStoreItems.Count; i++)
        {
            storeItems.Add(newStoreItems[i]);
        }
    }

}
