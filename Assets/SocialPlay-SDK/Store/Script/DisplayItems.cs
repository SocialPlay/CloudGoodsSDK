using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DisplayItems : MonoBehaviour
{
    public NGUIStoreLoader storeLoader;

    List<JToken> items = new List<JToken>();

    // Use this for initialization
    void Start()
    {
        GameAuthentication.OnUserAuthEvent += OnUserAuth;
    }

    void OnUserAuth(string user)
    {
        GetItems();
    }

    public void GetItems()
    {

        if (!storeLoader)
            storeLoader = this.gameObject.GetComponent<NGUIStoreLoader>();

        WebserviceCalls.webservice.GetStoreItems(ItemSystemGameData.AppID.ToString(), OnReceivedStoreItems);
    }

    void OnReceivedStoreItems(string storeItemsJson)
    {
        items = new List<JToken>();
        Debug.Log("store items: " + storeItemsJson);
        JToken token = JToken.Parse(storeItemsJson);

        JArray storeItems = JArray.Parse(token.ToString());

        
        for (int i = 0; i < storeItems.Count; i++)
        {
            items.Add(storeItems[i]);
        }


        storeLoader.SetMasterList(items);
    }
}
