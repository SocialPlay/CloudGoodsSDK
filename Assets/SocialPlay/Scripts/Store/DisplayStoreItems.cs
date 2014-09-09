using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class DisplayStoreItems : MonoBehaviour
{
    public UnityUIStoreLoader storeLoader;

    public FilterNewestItems.SortTimeType timeFilterType = FilterNewestItems.SortTimeType.hours;
    public int itemDisplayCount = 0;
    public int timeDifference = 5;

    FilterNewestItems newestItemFilter = new FilterNewestItems();
    List<StoreItem> items = new List<StoreItem>();

    public void DisplayItems()
    {
        if (!storeLoader)
            storeLoader = this.gameObject.GetComponent<UnityUIStoreLoader>();

        SP.GetStoreItems(OnReceivedStoreItems);
    }

    void OnReceivedStoreItems(List<StoreItem> storeItems)
    {
        for (int i = 0; i < storeItems.Count; i++)
        {
            items.Add(storeItems[i]);
        }
    }

}
