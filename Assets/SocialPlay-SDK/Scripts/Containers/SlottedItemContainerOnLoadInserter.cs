using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class SlottedItemContainerOnLoadInserter : MonoBehaviour
{
    

	// Use this for initialization
	void Start () {
        GetComponent<ContainerItemLoader>().LoadedItemsForContainerEvent += OnContainerLoadItems;   
	}

    void OnContainerLoadItems(List<ItemData> items, ItemContainer container)
    {
        foreach (ItemData item in items)
        {
            ItemData itemData = ItemConverter.ConvertItemDataToNGUIItemDataObject(item);

            List<ItemData> componentInitailizerList = new List<ItemData>();

            componentInitailizerList.Add(itemData);

            foreach (SlottedContainerSlotData slot in (container as SlottedItemContainer).slots.Values)
            {
                if (slot.persistantID == item.persistantLocation)
                {
                    (container as SlottedItemContainer).AddToSlot(itemData, int.Parse(slot.slotNameID), -1, false);
                    break;
                }
            }
            Destroy(item.gameObject);
        }
    }
}
