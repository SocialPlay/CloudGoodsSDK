using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class DisplayNewestItems : MonoBehaviour
{
    public NGUIStoreLoader storeLoader;

    public string appID;
    public FilterNewestItems.SortTimeType timeFilterType = FilterNewestItems.SortTimeType.hours;
    public int itemDisplayCount = 0;
    public int timeDifference = 5;

    FilterNewestItems newestItemFilter = new FilterNewestItems();
    List<StoreItem> items = new List<StoreItem>();

    void Start()
    {
        if (!storeLoader)
            storeLoader = this.gameObject.GetComponent<NGUIStoreLoader>();

        SP.GetStoreItems(OnReceivedStoreItems);
    }

    void OnReceivedStoreItems(List<StoreItem> storeItems)
    {
            for (int i = 0; i < storeItems.Count; i++)
            {
                items.Add(storeItems[i]);
            }
      

        FilterItemsByDateTime();
    }

    void FilterItemsByDateTime()
    {
        List<StoreItem> newestItems = newestItemFilter.FilterItems(items, timeFilterType, itemDisplayCount, timeDifference);

        storeLoader.LoadStoreWithPaging(newestItems, 0);
    }
}
