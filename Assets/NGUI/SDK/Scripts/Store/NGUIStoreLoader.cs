using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NGUIStoreLoader : StoreLoader
{
    public UIGrid grid;
    public GameObject itemPurchasePanel;

    public GameObject pageButtonPrefab;
    public GameObject itemButtonPrefab;
    public GameObject pageGridObject;

    UIGrid pageGrid;

    protected override void Awake()
    {
        pageGrid = pageGridObject.GetComponent<UIGrid>();
        base.Awake();
    }

    public override void LoadStoreWithPaging(List<StoreItem> listItems, int pageNum)
    {
        filteredList = listItems;

        if (currentPageItems.Count > 0)
            ClearCurrentGrid();

        currentPageItems = new List<GameObject>();

        int PageMax = GetPageMax(listItems.Count, pageNum);

        for (int i = pageNum * maxGridAmount; i < (pageNum * maxGridAmount + PageMax); i++)
        {
            GameObject newItem = NGUITools.AddChild(grid.gameObject, itemButtonPrefab);
            currentPageItems.Add(newItem);

            UIStoreItem itemInfo = newItem.GetComponent<UIStoreItem>();
            itemInfo.SetItemData(listItems[i]);

            UIEventListener.Get(itemInfo.gameObject).onClick += DisplayItemPurchasePanel;
        }

        grid.repositionNow = true;

        SetPageButtons(GetPageAmount(listItems.Count));
    }
    
    protected override void ClearCurrentGrid()
    {
        grid.transform.DetachChildren();
        base.ClearCurrentGrid();
        grid.Reposition();
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

            UIEventListener.Get(newPage).onClick += SetPageFromObject;
        }

        pageGrid.repositionNow = true;
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

        pageGrid.repositionNow = true;
    }

    void SetPageFromObject(GameObject pageButton)
    {
        PageInfo pageInfo = pageButton.GetComponent<PageInfo>();

        SetPage(pageInfo.pageNumber);
    }


    public void DisplayItemPurchasePanel(GameObject itemButton)
    {
        itemPurchasePanel.SetActive(true);

        itemPurchasePanel.GetComponent<ItemPurchase>().DisplayItemPurchasePanel(itemButton.GetComponent<UIStoreItem>());
    }
}
