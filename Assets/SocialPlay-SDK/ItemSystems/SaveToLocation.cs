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
            data.isLocked = true;
            WebserviceCalls.webservice.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), ItemSystemGameData.AppID, DestinationLocation, delegate(string x)
            {
                Debug.Log("Mod: " + x + "\nOriginal: " + data.stackID.ToString());
                JToken token = JToken.Parse(x);
                data.stackID = new Guid(token.ToString());
                data.isLocked = false;
            });
        }
    }

    void AddedItem(ItemData data, bool isSave)
    {
        if (isSave == true)
        {
            data.isLocked = true;
            WebserviceCalls.webservice.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), ItemSystemGameData.AppID, DestinationLocation, delegate(string x)
            {
                Debug.Log("Added: " + x + "\nOriginal: " + data.stackID.ToString());
                JToken token = JToken.Parse(x);
                data.stackID = new Guid(token.ToString());
                data.isLocked = false;
            });
        }
    }

    void RemovedItem(ItemData data, int amount, bool isMoving)
    {
        if (!isMoving)
        {
            WebserviceCalls.webservice.DeductStackAmount(data.stackID, -amount, delegate(string x)
            {
                Debug.Log("Removed : " + x);
            });
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



}

