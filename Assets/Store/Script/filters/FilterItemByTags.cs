using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class FilterItemByTags : FilterItem
{
    public override void init()
    {
        filterLabel.text = filterDisplayName;
    }

    public void SetFilterName(string name)
    {
        filterLabel.text = name;
        filterBy = name;
    }

    public override List<JToken> FilterStoreList(List<JToken> storeList)
    { 

        List<JToken> newStoreList = new List<JToken>();

        for (int i = 0; i < storeList.Count; i++)
        {
            JArray tags = (JArray)storeList[i]["tags"];    

            for (int j = 0; j < tags.Count; j++)
            {
                if (string.IsNullOrEmpty( tags[j].ToString()))
                {
                    continue;
                }
                if (tags[j]["Name"].ToString() == filterBy)
                {
                    newStoreList.Add(storeList[i]);
                    continue;
                }

            }
        }
        return newStoreList;
    }

}
