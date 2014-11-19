using UnityEngine;
using System.Collections;

using System.Collections.Generic;

[RequireComponent(typeof(ItemContainer))]
[RequireComponent(typeof(BoxCollider))]
public class NGUIContainerDisplay : ItemContainerDisplay
{
    public UIGrid itemGrid;
    public GameObject itemPrefab;
    public bool StartWindowActive = true;
    protected bool isActive = true;

    //List<ContainerDisplayAction> disaplyActions = new List<ContainerDisplayAction>();


    // List of all instantiated rows
    List<ItemDataComponent> mList = new List<ItemDataComponent>();
    List<ItemDataComponent> mUnused = new List<ItemDataComponent>();


    BoxCollider myCollider;
    void Awake()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        if (myContainer == null)
        {
            myContainer = GetComponent<ItemContainer>();
        }
    }

    protected void Start()
    {
        if (itemPrefab == null) itemPrefab = CloudGoods.DefaultUIItem;
        SetupWindow();

    }

    void Update()
    {
        myCollider.enabled = NGUIDragDropItem.IsDraggingItem;

    }

    /// <summary>
    /// Create a new entry, reusing an old entry if necessary.
    /// </summary>

    ItemDataComponent Create(ItemData item)
    {
        for (int i = 0; i < mList.Count; ++i)
        {
            ItemDataComponent ch = mList[i];

            if (ch.itemData.CollectionID == item.CollectionID)
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
            //idc.SetData(item);
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

    public override void ClearItems()
    {
        for (int i = 0; i < mList.Count; ++i)
        {
            Delete(mList[i]);
        }
    }

    public override void AddedItem(ItemData itemData, bool isSave)
    {
        Create(itemData);
        itemGrid.repositionNow = true;
    }

    public override void RemovedItem(ItemData itemData, int amount, bool isBeingMoved)
    {
        if (!isBeingMoved)
        {
            if (itemData.stackSize - amount <= 0)
            {
                Delete(itemData.uiReference);
                itemGrid.repositionNow = true;
            }
        }
        if (amount == -1 || itemData.stackSize <= 0)
        {
            Delete(itemData.uiReference);
            itemGrid.repositionNow = true;
        }
    }

    protected virtual void SetupWindow()
    {
        isActive = StartWindowActive;
        gameObject.SetActive(true);
        if (itemGrid == null) itemGrid = GetComponentInChildren<UIGrid>();
        //GetDisplayActions();
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
