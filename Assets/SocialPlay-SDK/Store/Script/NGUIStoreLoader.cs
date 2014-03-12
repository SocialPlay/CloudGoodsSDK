using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NGUIStoreLoader : MonoBehaviour
{
    public GameObject itemPurchasePanel;

    public GameObject pageButtonPrefab;
    public GameObject itemButtonPrefab;
    public GameObject itemGridObject;
    public GameObject pageGridObject;

    public int maxGridAmount = 32;

    UIGrid itemGrid;
    UIGrid pageGrid;

    List<JToken> items = new List<JToken>();
    List<JToken> filteredList = new List<JToken>();

    List<GameObject> currentPageItems = new List<GameObject>();

    int currentPage = 0;

    // Use this for initialization
    void Awake()
    {
        pageGrid = pageGridObject.GetComponent<UIGrid>();
        itemGrid = itemGridObject.GetComponent<UIGrid>();
    }

    public void SetMasterList(List<JToken> listItems)
    {
        items = listItems;
        LoadStoreWithPaging(items, 0);
    }

    public void LoadStoreWithPaging(List<JToken> listItems, int pageNum)
    {
        filteredList = listItems;

        if (currentPageItems.Count > 0)
            ClearCurrentGrid();

        currentPageItems = new List<GameObject>();

        int PageMax = GetPageMax(listItems.Count, pageNum);

        Debug.Log("pageNax: " + PageMax);

        for (int i = pageNum * maxGridAmount; i < (pageNum * maxGridAmount + PageMax); i++)
        {
            GameObject newItem = (GameObject)GameObject.Instantiate(itemButtonPrefab);
            newItem.transform.parent = itemGridObject.transform;
            newItem.transform.localScale = new Vector3(1, 1, 1);
            currentPageItems.Add(newItem);

            ItemInfo itemInfo = newItem.GetComponent<ItemInfo>();
            itemInfo.SetItemInfo(listItems[i]);

            UIEventListener.Get(itemInfo.gameObject).onClick += DisplayItemPurchasePanel;
        }

        itemGrid.repositionNow = true;

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
        itemGridObject.transform.DetachChildren();

        foreach (GameObject gridItemObj in currentPageItems)
        {
            Destroy(gridItemObj);
        }

        currentPageItems.Clear();

        itemGrid.Reposition();
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

            PageInfo pageInfo = newPage.GetComponent<PageInfo>();
            pageInfo.SetPage(i);

            UIEventListener.Get(newPage).onClick += SetPage;
        }

        pageGrid.repositionNow = true;
    }

    void ClearPageButtons()
    {
        List<Transform> pageObj = new List<Transform>();
        for (int i = 0; i < pageGridObject.transform.GetChildCount(); i++)
        {
            pageObj.Add(pageGridObject.transform.GetChild(i));
        }

        pageGridObject.transform.DetachChildren();

        foreach (Transform gridItemObj in pageObj)
        {
            Destroy(gridItemObj.gameObject);
        }

        pageGrid.repositionNow = true;
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

    public List<JToken> GetStoreItemList()
    {
        return items;
    }

    public void DisplayItemPurchasePanel(GameObject itemButton)
    {
        itemPurchasePanel.SetActive(true);

        itemPurchasePanel.GetComponent<ItemPurchase>().DisplayItemPurchasePanel(itemButton.GetComponent<ItemInfo>());
    }
}
