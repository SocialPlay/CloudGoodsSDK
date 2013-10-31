using UnityEngine;
using SocialPlay.ItemSystems;
using System.Collections;
using System.Collections.Generic;

public class ItemConverter  {

    public static ItemData ConvertItemDataToNGUIItemDataObject(ItemData item)
    {
        UnityEngine.Object obj = Resources.Load("NGUIContainerItem");
        GameObject go = GameObject.Instantiate(obj) as GameObject;
        ItemData itemData = go.GetComponent<ItemData>();

        itemData.SetItemData(item);
        return itemData;
    }

    public static List<GameObject> ConvertToItemDropObject(List<ItemData> items, bool isPerItemDrop)
    {
        List<GameObject> ItemDrops = new List<GameObject>();

        foreach (ItemData item in items)
        {
            UnityEngine.Object obj = Resources.Load("ItemDrop");
            GameObject go = GameObject.Instantiate(obj) as GameObject;
            ItemData gameItemData = go.GetComponent<ItemData>();
            gameItemData.SetItemData(item);
            ItemDrops.Add(go);
        }

        return ItemDrops;
    }
}
