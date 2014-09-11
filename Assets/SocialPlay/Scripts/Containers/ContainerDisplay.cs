using UnityEngine;
using System.Collections;

using System.Collections.Generic;

[RequireComponent(typeof(ItemContainer))]
public class ContainerDisplay : MonoBehaviour
{
    internal UIGrid itemGrid;

    public GameObject itemPrefab;
    public bool StartWindowActive = true;

    protected bool isActive = true;

    List<ContainerDisplayAction> disaplyActions = new List<ContainerDisplayAction>();
    ItemContainer itemContainer;

    // List of all instantiated rows
    List<ItemDataComponent> mList = new List<ItemDataComponent>();
    List<ItemDataComponent> mUnused = new List<ItemDataComponent>();

    protected void Awake()
    {
        itemContainer = GetComponent<ItemContainer>();
    }

    protected void OnEnable()
    {
        itemContainer.AddedItem += AddedItem;
        itemContainer.RemovedItem += RemovedItem;
        itemContainer.ClearItems += ClearItems;
    }


    protected void OnDisable()
    {
        itemContainer.AddedItem -= AddedItem;
        itemContainer.RemovedItem -= RemovedItem;
        itemContainer.ClearItems -= ClearItems;
    }

    protected void Start()
    {
        if (itemPrefab == null) itemPrefab = SP.DefaultUIItem;
        SetupWindow();
    }

    protected void Update()
    {
        if (!isActive)
        {
            HideWindow();
        }
        if (isActive)
        {
            ShowWindow();
        }
    }

    /// <summary>
    /// Create a new entry, reusing an old entry if necessary.
    /// </summary>

    ItemDataComponent Create(ItemData item)
    {
        for (int i = 0; i < mList.Count; ++i)
        {
            ItemDataComponent ch = mList[i];

            if (ch.itemData.itemID == item.itemID)
            {
                ch.itemData = item;
                ch.SetData(item);
                return ch;
            }
        }

        if (mUnused.Count > 0)
        {
            ItemDataComponent idc = mUnused[mUnused.Count - 1];
            mUnused.RemoveAt(mUnused.Count - 1);
            NGUITools.SetActive(idc.gameObject, true);
            mList.Add(idc);
            idc.itemData = item;
            idc.SetData(item);
            return idc;
        }

        GameObject go = NGUITools.AddChild(itemGrid.gameObject, itemPrefab);
        ItemDataComponent ent = go.GetComponent<ItemDataComponent>();
        if (ent == null) ent = go.AddComponent<ItemDataComponent>();
        ent.itemData = item;
        ent.SetData(item);
        mList.Add(ent);
        return ent;
    }

    /// <summary>
    /// Delete the specified entry, adding it to the unused list.
    /// </summary>

    void Delete(ItemDataComponent ent)
    {
        mList.Remove(ent);
        mUnused.Add(ent);
        NGUITools.SetActive(ent.gameObject, false);
    }

    void ClearItems()
    {
        for (int i = 0; i < mList.Count; ++i)
        {
            //Delete(mList[i]);
        }
    }

    protected void AddedItem(ItemData itemData, bool isSave)
    {
        Create(itemData);

        /*foreach (UIWidget item in go.GetComponentsInChildren<UIWidget>())
        {
            item.enabled = true;
        }
        foreach (MonoBehaviour item in go.GetComponentsInChildren<MonoBehaviour>())
        {
            if (item != null)
            {
                item.enabled = true;
            }
        }*/
        itemGrid.repositionNow = true;
    }

    protected void RemovedItem(ItemData itemData, int amount, bool isBeingMoved)
    {
        Debug.Log("removed item: " + itemData.stackSize + "  amoutn: " + amount);       

        if (!isBeingMoved)
        {
            if (itemData.stackSize - amount <= 0)
            {
                Delete(itemData.uiReference);
                //Destroy(itemData.uiReference.gameObject);
                itemGrid.repositionNow = true;
            }
        }

        if (amount == -1 || itemData.stackSize <= 0)
        {
            Delete(itemData.uiReference);
            //Destroy(itemData.uiReference.gameObject);
            itemGrid.repositionNow = true;
        }
    }

    protected virtual void SetupWindow()
    {
        isActive = StartWindowActive;
        gameObject.SetActive(true);
        itemGrid = GetComponentInChildren<UIGrid>();
    }

    private void GetDisplayActions()
    {
        if (GetComponentsInChildren<ContainerDisplayAction>().Length != 0)
        {
            foreach (ContainerDisplayAction action in GetComponentsInChildren<ContainerDisplayAction>())
            {
                disaplyActions.Add(action);
            }
        }

        if (GetComponents<ContainerDisplayAction>().Length != 0)
        {
            foreach (ContainerDisplayAction action in GetComponents<ContainerDisplayAction>())
            {
                disaplyActions.Add(action);
            }
        }
    }

    public void ShowWindow()
    {
        if (isActive) return;
        isActive = true;
        foreach (ContainerDisplayAction action in disaplyActions)
        {
            action.Activate();
        }
    }

    public void HideWindow()
    {
        if (!isActive) return;
        foreach (ContainerDisplayAction action in disaplyActions)
        {
            action.Deactivate();
        }
        isActive = false;
    }

    public bool IsWindowActive()
    {
        return isActive;
    }

    public void SetWindowIsActive(bool state)
    {
        isActive = state;
    }

}
