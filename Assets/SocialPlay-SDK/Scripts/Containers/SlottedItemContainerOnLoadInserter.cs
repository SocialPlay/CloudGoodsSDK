using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class SlottedItemContainerOnLoadInserter : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        GetComponent<ContainerItemLoader>().LoadedItemsForContainerEvent += OnContainerLoadItems;
    }

    void OnContainerLoadItems(List<ItemData> items, ItemContainer container)
    {
        foreach (ItemData item in items)
        {
            ItemData itemData = ItemConverter.ConvertItemDataToNGUIItemDataObject(item);

            foreach (SlottedContainerSlotData slot in (container as SlottedItemContainer).slots.Values)
            {
                if (slot.persistantID == item.persistantLocation && slot.slotData == null)
                {
                    (container as SlottedItemContainer).AddToSlot(itemData, int.Parse(slot.slotNameID), -1, false);
                    break;
                }
                Destroy(itemData.gameObject);
            }
            Destroy(item.gameObject);
        }
    }
}
