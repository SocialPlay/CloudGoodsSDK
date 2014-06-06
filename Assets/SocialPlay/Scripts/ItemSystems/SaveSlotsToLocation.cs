﻿using System;
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
                SP.MoveItemStack(data.stackID, data.stackSize, GetOwnerID(), DestinationOwnerType.ToString(), SlotedContainer.slots[slotId].persistantID, delegate(Guid x)
                {
                    try
                    {
                        Debug.Log("Slot Added: " + x + "\nOriginal: " + data.stackID.ToString());
                        data.stackID = x;
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

            SP.DeductStackAmount(data.stackID, -amount, delegate(string x)
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
            //case ItemOwnerTypes.Instance:
           //     return ItemSystemGameData.InstanceID.ToString();
            case ItemOwnerTypes.Session:
                return SP.user.sessionID.ToString();
            case ItemOwnerTypes.User:
                return SP.user.userID.ToString();
        }
        return "";
    }
}

