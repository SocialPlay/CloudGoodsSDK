using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class FilterItemByProperties : FilterItem
{
    public override List<StoreItemInfo> FilterStoreList(List<StoreItemInfo> storeList)
    {
        List<StoreItemInfo> newStoreList = new List<StoreItemInfo>();
        foreach (StoreItemInfo item in storeList)
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
