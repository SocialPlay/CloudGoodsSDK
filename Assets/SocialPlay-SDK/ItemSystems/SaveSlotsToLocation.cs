using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SaveSlotsToLocation : MonoBehaviour
{

    public ItemOwnerTypes destinationOwnerType;

    public SlottedItemContainer slotedContainer = null;

    void OnEnable()
    {
        if (slotedContainer != null)
        {
            slotedContainer.AddedItem += AddedItem;
            slotedContainer.ModifiedItem += ModifiedItem;
            slotedContainer.removedItem += RemovedItem;
        }
    }

    void OnDisable()
    {
        if (slotedContainer != null)
        {
            slotedContainer.ModifiedItem -= ModifiedItem;
            slotedContainer.AddedItem -= AddedItem;
            slotedContainer.removedItem -= RemovedItem;
        }
    }

    void ModifiedItem(ItemData data, bool isSave)
    {
        int slotId = slotedContainer.FindItemInSlot(data);
        if (slotId != -1)
        {
            if (isSave == true && slotedContainer.slots[slotId].persistantID != -1)
            {
                ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), destinationOwnerType.ToString(), ItemSystemGameData.AppID, slotedContainer.slots[slotId].persistantID, ReturnedString);
            }
        }
    }

    void AddedItem(ItemData data, bool isSave)
    {
        int slotId = slotedContainer.FindItemInSlot(data);
        if (slotId != -1)
        {
            if (isSave == true && slotedContainer.slots[slotId].persistantID != -1)
            {
                ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), destinationOwnerType.ToString(), ItemSystemGameData.AppID, slotedContainer.slots[slotId].persistantID, delegate(string x)
                {
                    JToken token = JToken.Parse(x);
                    data.stackID = new Guid(token.ToString());
                });
            }
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

