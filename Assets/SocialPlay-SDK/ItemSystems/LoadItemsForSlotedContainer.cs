using UnityEngine;
using System.Collections;

public class LoadItemsForSlotedContainer : ContainerItemLoader
{

    public override void LoadItems()
    {
        if (container == null || container.GetType() != typeof(SlottedItemContainer))
        {
            Debug.LogWarning("Load Items For Sloted Container Script Requires Slotted container");
        }

        foreach (SlottedContainerSlotData slot in (container as SlottedItemContainer).slots.Values)
        {
            ItemServiceManager.service.GetOwnerItems(GetOwnerID(), sourceOwnerType.ToString(), slot.persistantID, ItemSystemGameData.AppID, RecivedItems);
        }
     
    }
}
