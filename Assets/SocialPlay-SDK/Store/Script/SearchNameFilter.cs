using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

public class SearchNameFilter : MonoBehaviour
{

    //public NGUIStoreLoader storeLoader;

    UIInput input;
    string lastText = "";

    public static event Action<string> searchUpdate;
    public static bool inputSelected = false;

    void Awake()
    {
        input = GetComponentInChildren<UIInput>();
    }

    void Update()
    {
        if (input != null && lastText != input.value)
        {
            lastText = input.value;
            if (searchUpdate != null)
            {         
                searchUpdate(input.value);
            }
        }

        inputSelected = input.selected;
    }

    void OnSubmit(string currentString)
    {
        if (searchUpdate != null)
        {
            searchUpdate(currentString);
        }
    }

   public static  List<JToken> FilterStoreItemsFromSearch(List<JToken> storeItems, string searchFilter)
    {
        if (searchFilter.Length == 0)
        {
            return storeItems;
        }

        List<JToken> filteredStoreItems = new List<JToken>();
        for (int i = 0; i < storeItems.Count; i++)
        {
            if (storeItems[i]["Name"].ToString().ToLower().Contains(searchFilter.ToLower()))
                filteredStoreItems.Add(storeItems[i]);
        }
        return filteredStoreItems;
    }
}
