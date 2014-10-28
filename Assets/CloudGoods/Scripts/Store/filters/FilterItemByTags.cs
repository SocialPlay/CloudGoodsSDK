using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class FilterItemByTags : FilterItem
{
    public override void init()
    {
        //filterLabel.text = filterDisplayName;
    }

    public void SetFilterName(string name)
    {
        //filterLabel.text = name;
        filterBy = name;
    }

    public override List<StoreItem> FilterStoreList(List<StoreItem> storeList)
    {
        List<StoreItem> newStoreList = new List<StoreItem>();

        foreach (StoreItem storeitemInfo in storeList)
        {
            foreach(string tag in storeitemInfo.tags)
            {
                if (string.IsNullOrEmpty(tag))
                {
                    continue;
                }
                if (tag == filterBy)
                {
                    newStoreList.Add(storeitemInfo);
                    continue;
                }

            }
        }
        return newStoreList;
    }

}
