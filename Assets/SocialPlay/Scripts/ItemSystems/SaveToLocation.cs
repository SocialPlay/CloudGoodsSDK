﻿using System;
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
            Debug.Log("Saving Modified: " + data.stackSize);
            data.isLocked = true;
            SP.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), DestinationLocation, delegate(Guid x)
            {
                data.stackID = x;
                data.isLocked = false;
                Container.RefreshContainer();
            });
        }
    }

    void AddedItem(ItemData data, bool isSave)
    {
        if (isSave == true)
        {
            Debug.Log("Saving Added: " + data.stackSize);
            data.isLocked = true;
            SP.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), DestinationLocation, delegate(Guid x)
            {
                data.stackID = x;
                data.isLocked = false;
                Container.RefreshContainer();
            });
        }
    }

    void RemovedItem(ItemData data, int amount, bool isMoving)
    {
        if (!isMoving)
        {
            Debug.Log("Saving Removig: " + data.stackSize);

            SP.DeductStackAmount(data.stackID, -amount, delegate(string x)
            {
                Debug.Log("Removed : " + x);
                Container.RefreshContainer();
            });
        }
    }

    string GetOwnerID()
    {
        switch (DestinationOwnerType)
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



}

