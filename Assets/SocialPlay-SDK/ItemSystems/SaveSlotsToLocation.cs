using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SaveSlotsToLocation : MonoBehaviour
{

    public ItemOwnerTypes DestinationOwnerType;

    public SlottedItemContainer SlotedContainer = null;

    void OnEnable()
    {
        if (SlotedContainer != null)
        {
            SlotedContainer.AddedItem += AddedItem;
            SlotedContainer.ModifiedItem += ModifiedItem;
            SlotedContainer.RemovedItem += RemovedItem;
        }
    }

    void OnDisable()
    {
        if (SlotedContainer != null)
        {
            SlotedContainer.ModifiedItem -= ModifiedItem;
            SlotedContainer.AddedItem -= AddedItem;
            SlotedContainer.RemovedItem -= RemovedItem;
        }
    }

    void ModifiedItem(ItemData data, bool isSave)
    {
        int slotId = SlotedContainer.FindItemInSlot(data);
        if (slotId != -1)
        {
            if (isSave == true && SlotedContainer.slots[slotId].persistantID != -1)
            {
                ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), ItemSystemGameData.AppID, SlotedContainer.slots[slotId].persistantID, ReturnedString);
            }
        }
    }

    void AddedItem(ItemData data, bool isSave)
    {
        int slotId = SlotedContainer.FindItemInSlot(data);
        if (slotId != -1)
        {
            if (isSave == true && SlotedContainer.slots[slotId].persistantID != -1)
            {
                ItemServiceManager.service.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), ItemSystemGameData.AppID, SlotedContainer.slots[slotId].persistantID, delegate(string x)
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

