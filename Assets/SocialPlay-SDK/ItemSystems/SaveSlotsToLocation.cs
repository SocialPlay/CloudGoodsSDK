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
            SlotedContainer.RemovedItem += RemovedItem;
        }
    }

    void OnDisable()
    {
        if (SlotedContainer != null)
        {
            SlotedContainer.AddedItem -= AddedItem;
            SlotedContainer.RemovedItem -= RemovedItem;
        }
    }


    void AddedItem(ItemData data, bool isSave)
    {
        int slotId = SlotedContainer.FindItemInSlot(data);
        if (slotId != -1)
        {
            if (isSave == true && SlotedContainer.slots[slotId].persistantID != -1)
            {
                data.isLocked = true;
                WebserviceCalls.webservice.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), ItemSystemGameData.AppID, SlotedContainer.slots[slotId].persistantID, delegate(string x)
                {
                    try
                    {
                        Debug.Log("Slot Added: " + x + "\nOriginal: " + data.stackID.ToString());
                        JToken token = JToken.Parse(x);
                        data.stackID = new Guid(token.ToString());
                    }
                    finally
                    {
                        data.isLocked = false;
                    }
                });
            }
        }
    }

    void RemovedItem(ItemData data, int amount, bool isMovingToAnotherContainer)
    {
        if (isMovingToAnotherContainer == false)
        {

            WebserviceCalls.webservice.DeductStackAmount(data.stackID, -amount, delegate(string x)
            {
                try
                {
                    Debug.Log("Slot Removed: " + x + "\nOriginal: " + data.stackID.ToString());
           
                }
                finally
                {
                    data.isLocked = true;
                }
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

