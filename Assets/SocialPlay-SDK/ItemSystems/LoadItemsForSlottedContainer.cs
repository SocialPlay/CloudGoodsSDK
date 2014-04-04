using UnityEngine;
using System.Collections;

public class LoadItemsForSlottedContainer : ContainerItemLoader
{

    public override void LoadItems()
    {
        if (Container == null || Container.GetType() != typeof(SlottedItemContainer))
        {
            Debug.LogWarning("Load Items For Sloted Container Script Requires Slotted container");
        }

        foreach (SlottedContainerSlotData slot in (Container as SlottedItemContainer).slots.Values)
        {
            WebserviceCalls.webservice.GetOwnerItems(GetOwnerID(), SourceOwnerType.ToString(), slot.persistantID, ItemSystemGameData.AppID, RecivedItems);
        }
     
    }
}
