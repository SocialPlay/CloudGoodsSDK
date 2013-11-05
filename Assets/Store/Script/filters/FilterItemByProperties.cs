using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class FilterItemByProperties : FilterItem
{
    public override List<JToken> FilterStoreList(List<JToken> storeList)
    {
        List<JToken> newStoreList = new List<JToken>();
        foreach (JToken item in storeList)
        {   
            if (!string.IsNullOrEmpty(item["Detail"].ToString()))
            {
                JObject det = JObject.Parse(item["Detail"].ToString());
                foreach (var detail in det)
                {
                    if (detail.Key == filterBy)
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
