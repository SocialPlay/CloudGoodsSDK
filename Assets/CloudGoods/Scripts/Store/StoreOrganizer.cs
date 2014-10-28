using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class StoreOrganizer : MonoBehaviour
{
    public StoreLoader storeLoader;
    public InputValueChange SearchInput;
    private ISortItem currentSort;
    private int currentSortDirection = 1;
    public List<FilterItem> activeFilters;
    public string searchFilter = "";

    void OnEnable()
    {
        //StoreItemFilter.FilterUpdate += StoreItemFilter_FilterUpdate;
     
        //SortStoreItemsBy.SortUpdate += SortStoreItemsBy_SortUpdate;

        //if (searchFilter != null)
        //{
        //    SearchInput.searchUpdate += SearchNameFilter_searchUpdate;
        //}
    }

    void OnDisable()
    {
        //StoreItemFilter.FilterUpdate -= StoreItemFilter_FilterUpdate;
     
        //SortStoreItemsBy.SortUpdate -= SortStoreItemsBy_SortUpdate;

        //if (searchFilter != null)
        //{
        //    SearchInput.searchUpdate -= SearchNameFilter_searchUpdate;
        //}
    }

    void SortStoreItemsBy_SortUpdate(ISortItem CurrentSort, int direction)
    {
        currentSort = CurrentSort;
        currentSortDirection = direction;
        OrganizeStore();
    }

    void SearchNameFilter_searchUpdate(string searchFilter)
    {
        Debug.Log("searchFilter");
        this.searchFilter = searchFilter;
        OrganizeStore();
    }

    void StoreItemFilter_FilterUpdate(FilterItem updateFilter)
    {
        if (updateFilter.isActive && !activeFilters.Contains(updateFilter))
        {
            activeFilters.Add(updateFilter);
        }
        else if (!updateFilter.isActive && activeFilters.Contains(updateFilter))
        {
            activeFilters.Remove(updateFilter);
        }
        OrganizeStore();
    }

    void OrganizeStore()
    {      
        List<StoreItem> AllItems = storeLoader.GetStoreItemList();
        if (AllItems.Count == 0)
        {
            Debug.Log("No items to sort at this point");
            return;
        }
        List<StoreItem> storeList = AllItems.GetRange(0, AllItems.Count);

        //foreach (FilterItem filter in activeFilters) storeList = filter.FilterStoreList(storeList);

        storeList = CloudGoods.SearchStoreItems(storeList, searchFilter);
        //if (currentSort != null) storeList = currentSort.Sort(storeList, currentSortDirection);
        //else storeList = SortStoreItemsBy.DefaultSort(storeList);

        storeLoader.LoadStoreWithPaging(storeList, 0);

    }
}
