using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class FilterItemByProperties : FilterItem
{
    public override List<StoreItem> FilterStoreList(List<StoreItem> storeList)
    {
        List<StoreItem> newStoreList = new List<StoreItem>();
        foreach (StoreItem item in storeList)
        {   
            if (item.itemDetail.Count > 0)
            {
                foreach (StoreItemDetail detail in item.itemDetail)
                {
                    if (detail.propertyName == filterBy)
                    {
                        newStoreList.Add(item);
                        break;
                    }
                }
            }

        }
        return newStoreList;
    }
}
