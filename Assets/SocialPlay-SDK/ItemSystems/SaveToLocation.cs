using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SaveToLocation : MonoBehaviour
{

    public ItemOwnerTypes destinationOwnerType;

    public ItemContainer container = null;

    public int destinationLocation;

    void OnEnable()
    {
        if (container != null)
        {
            container.AddedItem += AddedItem;
            container.ModifiedItem += ModifiedItem;
            container.RemovedItem += RemovedItem;
        }
    }

    void OnDisable()
    {
        if (container != null)
        {
            container.ModifiedItem -= ModifiedItem;
            container.AddedItem -= AddedItem;
            container.RemovedItem -= RemovedItem;
        }
    }

    void ModifiedItem(ItemData data, bool isSave)
    {
        if (isSave == true)
        {
            ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), destinationOwnerType.ToString(), ItemSystemGameData.AppID, destinationLocation, ReturnedString);
        }
    }

    void AddedItem(ItemData data, bool isSave)
    {
        if (isSave == true)
        {
            ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), destinationOwnerType.ToString(), ItemSystemGameData.AppID, destinationLocation, delegate(string x) {
                        JToken token = JToken.Parse(x);
                data.stackID = new Guid(token.ToString());
            });
        }
    }

    void RemovedItem(ItemData data, bool isMovingToAnotherContainer)
    {
        if (isMovingToAnotherContainer == false)
        {
            ItemServiceManager.service.RemoveItemStack(data.stackID, ReturnedString);
        }
    }

    string GetOwnerID()
    {
        switch (destinationOwnerType)
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

