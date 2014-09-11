using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnityUIStoreLoader : MonoBehaviour
{
    public GameObject itemPurchasePanel;

    public GameObject pageButtonPrefab;
    public GameObject itemButtonPrefab;
    public GameObject pageGridObject;

    public int maxGridAmount = 32;

    UIGrid pageGrid;

    List<StoreItem> items = new List<StoreItem>();
    List<StoreItem> filteredList = new List<StoreItem>();

    List<GameObject> currentPageItems = new List<GameObject>();

    int currentPage = 0;

    // Use this for initialization
    void Awake()
    {
        pageGrid = pageGridObject.GetComponent<UIGrid>();
        SP.OnStoreListLoaded += OnStoreListLoaded;
    }

    void OnStoreListLoaded(List<StoreItem> listItems)
    {
        items = listItems;
        LoadStoreWithPaging(items, 0);
    }

    public void LoadStoreWithPaging(List<StoreItem> listItems, int pageNum)
    {
        filteredList = listItems;

        if (currentPageItems.Count > 0)
            ClearCurrentGrid();

        currentPageItems = new List<GameObject>();

        int PageMax = GetPageMax(listItems.Count, pageNum);

        for (int i = pageNum * maxGridAmount; i < (pageNum * maxGridAmount + PageMax); i++)
        {
            GameObject newItem = (GameObject)GameObject.Instantiate(itemButtonPrefab);
            newItem.transform.parent = transform;
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localScale = Vector3.one;
            currentPageItems.Add(newItem);

            UnityUIStoreItem itemInfo = newItem.GetComponent<UnityUIStoreItem>();
            itemInfo.SetItemData(listItems[i]);

            //UIEventListener.Get(itemInfo.gameObject).onClick += DisplayItemPurchasePanel;
        }

        SetPageButtons(GetPageAmount(listItems.Count));
    }

    int GetPageMax(int itemAmount, int pageNum)
    {
        int pageMax = 0;

        if (pageNum < GetPageAmount(itemAmount) - 1)
        {
            pageMax = maxGridAmount;
        }
        else
        {
            pageMax = itemAmount % maxGridAmount;
        }

        return pageMax;
    }

    private void ClearCurrentGrid()
    {
        //grid.transform.DetachChildren();

        //foreach (GameObject gridItemObj in currentPageItems)
        //{
        //    Destroy(gridItemObj);
        //}

        //currentPageItems.Clear();

        //grid.Reposition();
    }

    int GetPageAmount(int itemCount)
    {
        int calcPageAmount = 0;

        calcPageAmount = itemCount / maxGridAmount;

        if ((itemCount % maxGridAmount) > 0)
            calcPageAmount++;

        return calcPageAmount;
    }

    void SetPageButtons(int pageAmount)
    {
        ClearPageButtons();

        for (int i = 0; i < pageAmount; i++)
        {
            GameObject newPage = (GameObject)GameObject.Instantiate(pageButtonPrefab);
            newPage.transform.parent = pageGridObject.transform;
            newPage.transform.localScale = new Vector3(1, 1, 1);

            UnityUIPageInfo pageInfo = newPage.GetComponent<UnityUIPageInfo>();
            pageInfo.SetPage(i);

        }
    }

    void ClearPageButtons()
    {
        List<Transform> pageObj = new List<Transform>();
        for (int i = 0; i < pageGridObject.transform.childCount; i++)
        {
            pageObj.Add(pageGridObject.transform.GetChild(i));
        }

        pageGridObject.transform.DetachChildren();

        foreach (Transform gridItemObj in pageObj)
        {
            Destroy(gridItemObj.gameObject);
        }
    }

    void SetPage(GameObject pageButton)
    {
        PageInfo pageInfo = pageButton.GetComponent<PageInfo>();

        if (pageInfo.pageNumber != currentPage)
        {
            currentPage = pageInfo.pageNumber;

            LoadStoreWithPaging(filteredList, pageInfo.pageNumber);
        }
    }

    public List<StoreItem> GetStoreItemList()
    {
        return items;
    }

    public void DisplayItemPurchasePanel(GameObject itemButton)
    {
        itemPurchasePanel.SetActive(true);

        itemPurchasePanel.GetComponent<ItemPurchase>().DisplayItemPurchasePanel(itemButton.GetComponent<UIStoreItem>());
    }
}
