using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class PropertiesFilter : MonoBehaviour {

    public NGUIStoreLoader storeLoader;

    float time = 0.0f;

    void Update()
    {
        time += Time.deltaTime;

        if (time >= 3.0f)
        {
            FilterProperties(storeLoader.GetStoreItemList(), "D", 0, 100);
            time = 0.0f;
        }
    }

    List<JToken> FilterProperties(List<StoreItemInfo> storeList, string filterType, int filterMin, int filterMax)
    {
        List<JToken> newStoreList = new List<JToken>();

        foreach(StoreItemInfo storeInfo in storeList)
        {
            if (storeInfo.itemDetail.Count > 0)
            {
                Debug.Log("Found Property : " + filterType + " In Object: " + storeInfo.itemName);
            }
        }

        return newStoreList;
    }
    
    string ConvertPropertyString(string compressedString)
    {
        switch (compressedString)
        {
            case "Attack Power":
                return "P";
            case "Attack Range":
                return "CR";
            case "Defence":
                return "D";
            case "Strength":
                return "S";
            case "Intelligence":
                return "I";
            default:
                return "";

        }
    }
}
