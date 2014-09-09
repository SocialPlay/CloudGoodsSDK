using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class ItemConverter  {

    //public static ItemData ConvertItemDataToNGUIItemDataObject(ItemData item)
    //{
    //    UnityEngine.Object obj = Resources.Load("NGUIContainerItem");
    //    GameObject go = GameObject.Instantiate(obj) as GameObject;
    //    ItemData itemData = go.GetComponent<ItemData>();

    //    itemData.SetItemData(item);
    //    return itemData;
    //}

    public static List<GameObject> ConvertToItemDropObject(List<ItemData> items, bool isPerItemDrop)
    {
        List<GameObject> ItemDrops = new List<GameObject>();

        for (int i = 0, imax = items.Count; i < imax; i++ )
        {
            ItemData item = items[i];
            Debug.Log("item " + item);
            GameObject go = new GameObject(item.itemName+" (ID: "+item.itemID+")");
            ItemDataComponent comp = go.AddComponent<ItemDataComponent>();
            comp.itemData.SetItemData(item);
            ItemDrops.Add(go);
        }

        return ItemDrops;
    }
}
