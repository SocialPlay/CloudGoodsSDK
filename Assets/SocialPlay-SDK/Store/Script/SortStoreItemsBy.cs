using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

public class SortStoreItemsBy : MonoBehaviour
{

    public List<ISortItem> sortables = new List<ISortItem>();
    public UIPopupList popUpList;
    public NGUIStoreLoader storeLoader;
    string startSorting = "By Name";
    string currentSort = "";
    int currentDirection = 1;

    /// <summary>
    /// Called when the user selects a new sort method or changes the direction of sort.
    /// {0} is null if default sorting is selected.
    /// {1} is direction of sort (1) == is desc (2) is assc by deafult.
    /// </summary>
    public static event Action<ISortItem, int> SortUpdate;



    void OnSelectionChange(string selectedItem)
    {
        if (currentSort == selectedItem)
        {
            currentDirection *= -1;
        }
        else
        {
            currentDirection = 1;
        }
        if (selectedItem == startSorting)
        {
            SortUpdate(null, currentDirection);
            //storeLoader.LoadStoreWithPaging(DefaultSort(storeLoader.GetStoreItemList()), 0);

        }
        else
        {
            foreach (ISortItem sort in sortables)
            {
                if (sort.displayName == selectedItem)
                {
                    if (SortUpdate != null)
                    {
                        SortUpdate(sort, currentDirection);
                    }
                    // storeLoader.LoadStoreWithPaging(sort.Sort(storeLoader.GetStoreItemList(), currentDirection), 0);
                }
            }
        }
        currentSort = selectedItem;
    }

    void Awake()
    {

        if (sortables.Count == 0)
        {
            this.gameObject.SetActive(false);
            return;
        }

        List<string> sortOptions = new List<string>();
        sortOptions.Add(startSorting);
        foreach (ISortItem sortable in sortables)
        {

            bool duplicate = false;
            foreach (string checking in sortOptions)
            {
                if (checking == sortable.displayName)
                {
                    duplicate = true;
                }
            }
            if (!duplicate)
            {
                sortOptions.Add(sortable.displayName);
            }
            else
            {
                Debug.LogWarning("Can not add duplicate sortable display names for :" + sortable.displayName);
            }
        }
        popUpList.items = sortOptions;
    }

    public static List<JToken> DefaultSort(List<JToken> StoreItems)
    {
        StoreItems.Sort(SortByName);
        return StoreItems;
    }

    static int SortByName(JToken x, JToken y)
    {
        return x["Name"].ToString().ToLower().CompareTo(y["Name"].ToString().ToLower());
    }

}
