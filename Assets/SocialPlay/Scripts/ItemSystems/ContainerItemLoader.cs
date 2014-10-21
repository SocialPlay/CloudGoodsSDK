using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public abstract class ContainerItemLoader : MonoBehaviour
{
    public Action<List<ItemData>, ItemContainer> LoadedItemsForContainerEvent;

    public ItemContainer container;
    public ItemOwnerTypes SourceOwnerType;

    public abstract void LoadItems();


    void Start()
    {
        if (container == null)
        {
            container = this.GetComponent<ItemContainer>();
        }

    }

    protected string GetOwnerID()
    {
        switch (SourceOwnerType)
        {
            //case ItemOwnerTypes.Instance:
            //    return ItemSystemGameData.InstanceID.ToString();
            case ItemOwnerTypes.Session:
                return SP.user.sessionID.ToString();
            case ItemOwnerTypes.User:
                return SP.user.userID.ToString();
        }
        return "";
    }

    protected void RecivedItems(List<ItemData> receivedItems)
    {      
        if (SP.itemDataConverter == null)
        {
            throw new Exception("Item conversion is not setup correctly!");
        }

        //List<ItemData> recivedItems = ItemConversion.converter.convertToItemDataFromString(Data);

        AddItemComponentToNewlyConvertedItem(receivedItems);

        foreach (ItemData item in receivedItems)
        {
          
            container.Add(item, -1, false);
        }

        if (LoadedItemsForContainerEvent != null)
        {
            LoadedItemsForContainerEvent(receivedItems, container);
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
