using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public abstract class ContainerItemLoader : MonoBehaviour
{
    public Action<List<ItemData>, ItemContainer> LoadedItemsForContainerEvent;

    public ItemContainer Container;
    public ItemOwnerTypes SourceOwnerType;

    public abstract void LoadItems();


    void Awake()
    {
        if (Container == null)
        {
            Container = this.GetComponent<ItemContainer>();
        }
    }

    protected string GetOwnerID()
    {
        switch (SourceOwnerType)
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

    protected void RecivedItems(string Data)
    {
        if (ItemConversion.converter == null)
        {
            throw new Exception("Item conversion is not setup correctly!");
        }

        List<ItemData> recivedItems = ItemConversion.converter.convertToItemDataFromString(Data);

        AddItemComponentToNewlyConvertedItem(recivedItems);

        if (LoadedItemsForContainerEvent != null)
        {
            LoadedItemsForContainerEvent(recivedItems, Container);
        }
    }

    protected void AddItemComponentToNewlyConvertedItem(List<ItemData> items)
    {
        GameObject itemComponentInitialierObj = GameObject.Find("ItemComponents");

        if (itemComponentInitialierObj != null)
        {
            ItemComponentInitalizer.InitializeItemWithComponents(items, AddComponetTo.container);
        }
    }
}
