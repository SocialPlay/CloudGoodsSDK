using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemsForContainer : MonoBehaviour
{

    public Action<List<ItemData>, ItemContainer> LoadedItemsForContainerEvent;

    public ItemContainer container;
    public int sourceLocation;
    public ItemOwnerTypes sourceOwnerType;

    void Awake()
    {
        if (container == null)
        {
            container = this.GetComponent<ItemContainer>();
        }
    }

    public void LoadItems()
    {
        ItemServiceManager.service.GetOwnerItems(GetOwnerID(), sourceOwnerType.ToString(), sourceLocation, ItemSystemGameData.AppID, RecivedItems);
    }

    public string GetOwnerID()
    {
        switch (sourceOwnerType)
        {
            case ItemOwnerTypes.Instance:
                return ItemSystemGameData.InstanceID.ToString();
            case ItemOwnerTypes.Session:
                return ItemSystemGameData.SessionID.ToString();
            case ItemOwnerTypes.User:
                return ItemSystemGameData.UserID.ToString();
        }
        return "";
    }

    public void RecivedItems(string Data)
    {
        if (ItemConversion.converter == null)
        {
            throw new Exception("Item conversion is not setup correctly!");
        }

        List<ItemData> recivedItems = ItemConversion.converter.convertToItemDataFromString(Data);

        AddItemComponentToNewlyConvertedItem(recivedItems);

        if (LoadedItemsForContainerEvent != null)
        {
            LoadedItemsForContainerEvent(recivedItems, container);
        }
    }

    void AddItemComponentToNewlyConvertedItem(List<ItemData> items)
    {
        GameObject itemComponentInitialierObj = GameObject.Find("ItemComponents");

        if (itemComponentInitialierObj != null)
        {
            ItemComponentInitalizer.InitializeItemWithComponents(items, AddComponetTo.container);
        }
    }
}

