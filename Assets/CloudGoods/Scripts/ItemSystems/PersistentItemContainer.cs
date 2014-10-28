using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class PersistentItemContainer : MonoBehaviour
{
    public Action<List<ItemData>, ItemContainer> LoadedItemsForContainerEvent;

    public ItemContainer Container;
    public ItemOwnerTypes OwnerType;
    public int Location;

    public void LoadItems()
    {
        CloudGoods.GetOwnerItems(GetOwnerID(), OwnerType.ToString(), Location, RecivedItems);
    }

    void Start()
    {
        if (Container == null)
        {
            Container = this.GetComponent<ItemContainer>();
        }
    }

    protected string GetOwnerID()
    {
        switch (OwnerType)
        {
            //case ItemOwnerTypes.Instance:
            //    return ItemSystemGameData.InstanceID.ToString();
            case ItemOwnerTypes.Session:
                return CloudGoods.user.sessionID.ToString();
            case ItemOwnerTypes.User:
                return CloudGoods.user.userID.ToString();
        }
        return "";
    }


    #region Loading Items
    protected void RecivedItems(List<ItemData> receivedItems)
    {      
        if (CloudGoods.itemDataConverter == null)
        {
            throw new Exception("Item conversion is not setup correctly!");
        }

        foreach (ItemData item in receivedItems)
        {
            Container.Add(item, -1, false);
        }

        if (LoadedItemsForContainerEvent != null)
        {
            LoadedItemsForContainerEvent(receivedItems, Container);
        }
    }
    #endregion

    #region Saving Items

    void OnEnable()
    {
        if (Container != null)
        {
            Container.AddedItem += AddedItem;
            Container.ModifiedItem += ModifiedItem;
            Container.RemovedItem += RemovedItem;
        }
    }

    void OnDisable()
    {
        if (Container != null)
        {
            Container.ModifiedItem -= ModifiedItem;
            Container.AddedItem -= AddedItem;
            Container.RemovedItem -= RemovedItem;
        }
    }

    void ModifiedItem(ItemData data, bool isSave)
    {
        if (isSave == true)
        {
            Debug.Log("Saving Modified: " + data.stackSize);
            data.isLocked = true;
            CloudGoods.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), OwnerType.ToString(), Location, delegate(Guid x)
            {
                data.stackID = x;
                data.isLocked = false;
                //Container.RefreshContainer();
            });
        }
    }

    void AddedItem(ItemData data, bool isSave)
    {
        if (isSave == true)
        {
            data.isLocked = true;
            CloudGoods.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), OwnerType.ToString(), Location, delegate(Guid x)
            {
                data.stackID = x;
                data.isLocked = false;
                //Container.RefreshContainer();
            });
        }
    }

    void RemovedItem(ItemData data, int amount, bool isMoving)
    {
        if (!isMoving)
        {
            CloudGoods.DeductStackAmount(data.stackID, -amount, delegate(string x)
            {
                //Container.RefreshContainer();
            });
        }
    }

    #endregion
}
