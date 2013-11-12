using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

public class StoreItemFilter : MonoBehaviour
{
    //  public NGUIStoreLoader storeLoader;

    List<GameObject> filterTabs = new List<GameObject>();

    public string appID;

    public GameObject filterTabPrefab;
    public UIGrid filterTabGrid;

    public List<FilterItem> filters;

    public static event Action<FilterItem> FilterUpdate;

    void Start()
    {
        foreach (FilterItem newfilter in GetComponentsInChildren<FilterItem>())
        {
            if (!filters.Contains(newfilter))
            {
                filters.Add(newfilter);
            }
        }

        GetFilterTabs(appID);

    }

    public void GetFilterTabs(string appID)
    {
        OnReceivedFilterTabs("");
    }

    public void OnReceivedFilterTabs(string filterTabsJson)
    {
        List<string> tmpFilterList = new List<string>();
        AddFiltersFromGame();
    }

    void AddFiltersFromGame()
    {
        foreach (FilterItem filter in filters)
        {
            filter.init();
            AddFilterTab(filter.gameObject);
        }
    }

    public void AddFilterTab(GameObject filter)
    {
        filter.transform.parent = filterTabGrid.transform;
        UIEventListener.Get(filter).onClick += OnClickedFilteritem;

        filterTabGrid.repositionNow = true;
    }


    void OnClickedFilteritem(GameObject filterButton)
    {

        //TODO: fix with new NGUI 3.0
        //FilterItem filterItem = filterButton.GetComponent<FilterItem>();
        //filterItem.isActive = !filterItem.isActive;
        //if (filterItem.checkBox != null)
        //{
        //    filterItem.checkBox.isChecked = filterItem.isActive;
        //}
        //if (FilterUpdate != null)
        //{
        //    FilterUpdate(filterItem);
        //}

        //List<JToken> AllItems = storeLoader.GetStoreItemList();
        //List<JToken> storeList = AllItems.GetRange(0, AllItems.Count);

        //storeList = filterItem.FilterStoreList(storeList);

        // storeLoader.LoadStoreWithPaging(storeList, 0);

    }
}
