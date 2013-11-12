using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class ItemContainerOnLoadInserter : MonoBehaviour {
    

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
            container.Add(itemData, -1, false);
            Destroy(item.gameObject);
        }
    }
}
