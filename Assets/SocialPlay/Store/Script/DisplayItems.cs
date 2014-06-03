using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DisplayItems : MonoBehaviour
{
    public NGUIStoreLoader storeLoader;

    List<StoreItemInfo> items = new List<StoreItemInfo>();

    // Use this for initialization
    void Start()
    {
        SP.OnRegisteredUserToSession += OnUserAuth;
    }

    void OnUserAuth(string user)
    {
        GetItems();
    }

    public void GetItems()
    {

        if (!storeLoader)
            storeLoader = this.gameObject.GetComponent<NGUIStoreLoader>();

        SP.GetStoreItems(OnReceivedStoreItems);
    }

    void OnReceivedStoreItems(List<StoreItemInfo> storeItems)
    {   
        for (int i = 0; i < storeItems.Count; i++)
        {
            items.Add(storeItems[i]);
        }


        storeLoader.SetMasterList(items);
    }
}
