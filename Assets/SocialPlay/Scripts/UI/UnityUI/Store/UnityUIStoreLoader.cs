using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnityUIStoreLoader : StoreLoader
{
    public GameObject itemPurchasePanel;

    public GameObject pageButtonPrefab;
    public GameObject itemButtonPrefab;
    public GameObject pageGridObject;

    bool isLoadingPage = false;

    UnityEngine.Events.UnityAction OnPageButtonClicked;

    public override void LoadStoreWithPaging(List<StoreItem> listItems, int pageNum)
    {
        if (isLoadingPage == false)
        {
            isLoadingPage = true;

            filteredList = listItems;

            if (currentPageItems.Count > 0)
                ClearCurrentGrid();

            currentPageItems.Clear();

            int PageMax = GetPageMax(listItems.Count, pageNum);

            for (int i = pageNum * maxGridAmount; i < (pageNum * maxGridAmount + PageMax); i++)
            {
                GameObject newItem = (GameObject)GameObject.Instantiate(itemButtonPrefab);
                newItem.transform.parent = transform;
                newItem.transform.localPosition = Vector3.zero;
                newItem.transform.localScale = Vector3.one;
                currentPageItems.Add(newItem);

                UnityUIStoreItem itemInfo = newItem.GetComponent<UnityUIStoreItem>();
                itemInfo.Init(listItems[i], this);

                Button storeButton = newItem.GetComponent<Button>();
                storeButton.onClick.AddListener(itemInfo.OnStoreItemClicked);
            }

            SetPageButtons(GetPageAmount(listItems.Count));

            isLoadingPage = false;
        }
    }
       
    void SetPageButtons(int pageAmount)
    {
        ClearPageButtons();

        for (int i = pageAmount-1; i > -1; i--)
        {
            GameObject newPage = (GameObject)GameObject.Instantiate(pageButtonPrefab);
            newPage.transform.parent = pageGridObject.transform;
            newPage.transform.localScale = new Vector3(1, 1, 1);

            UnityUIPageInfo pageInfo = newPage.GetComponent<UnityUIPageInfo>();
            pageInfo.Init(i, this);

            OnPageButtonClicked += pageInfo.StoreCurrentPage;

            Button pageButton = newPage.GetComponent<Button>();
            pageButton.onClick.AddListener(OnPageButtonClicked);

        }
    }

    void ClearPageButtons()
    {
        List<Transform> pageObj = new List<Transform>();
        for (int i = 0; i < pageGridObject.transform.childCount; i++)
        {
            Transform pageTrans = pageGridObject.transform.GetChild(i);
            pageObj.Add(pageTrans);
            UnityUIPageInfo pageInfo = pageTrans.gameObject.GetComponent<UnityUIPageInfo>();
            OnPageButtonClicked -= pageInfo.StoreCurrentPage;
        }

        pageGridObject.transform.DetachChildren();

        foreach (Transform gridItemObj in pageObj)
        {
            Destroy(gridItemObj.gameObject);
        }
    }

    

    public void DisplayItemPurchasePanel(GameObject itemButton)
    {
        itemPurchasePanel.SetActive(true);

        itemPurchasePanel.GetComponent<UnityUIItemPurchase>().DisplayItemPurchasePanel(itemButton.GetComponent<UnityUIStoreItem>());
    }
}
