using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SaveToLocation : MonoBehaviour
{

    public ItemOwnerTypes DestinationOwnerType;

    public ItemContainer Container = null;

    public int DestinationLocation;

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
            ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), ItemSystemGameData.AppID, DestinationLocation, ReturnedString);
        }
    }

    void AddedItem(ItemData data, bool isSave)
    {
        if (isSave == true)
        {
            ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), ItemSystemGameData.AppID, DestinationLocation, delegate(string x)
            {
                JToken token = JToken.Parse(x);
                data.stackID = new Guid(token.ToString());
            });
        }
    }

    void RemovedItem(ItemData data, int amount, bool isMovingToAnotherContainer)
    {
        if (isMovingToAnotherContainer == false)
        {
            ItemServiceManager.service.ChangePlayerItemStackAmount(data.stackID, amount, ReturnedString);
        }
    }

    string GetOwnerID()
    {
        switch (DestinationOwnerType)
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


    void ReturnedString(string msg)
    {
    }
}

