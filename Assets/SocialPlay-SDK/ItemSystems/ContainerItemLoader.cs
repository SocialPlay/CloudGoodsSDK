using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;
using SocialPlay.Data;

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

    protected void RecivedItems(object Data)
    {
        if (ItemConversion.converter == null)
        {
            throw new Exception("Item conversion is not setup correctly!");
        }

        ItemDataList itemData = LitJson.JsonMapper.ToObject<ItemDataList>(Data.ToString());

        List<ItemData> receivedItems = ItemConversion.converter.convertToItemDataFromString(itemData);

        AddItemComponentToNewlyConvertedItem(receivedItems);

        if (LoadedItemsForContainerEvent != null)
        {
            LoadedItemsForContainerEvent(receivedItems, Container);
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
