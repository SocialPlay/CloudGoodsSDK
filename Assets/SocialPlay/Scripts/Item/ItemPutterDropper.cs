using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;

public class ItemPutterDropper : MonoBehaviour, IItemPutter
{
    public Transform dropTransform;
    public ItemPrefabInitilizer prefabinitilizer;
    ItemDrop gameItemDrop;

    void Awake()
    {
        if (GetComponent<ItemDrop>())
            gameItemDrop = GetComponent<ItemDrop>();
        else
            gameItemDrop = gameObject.AddComponent<ItemDrop>();
    }

    public void PutGameItem(List<ItemData> items)
    {
        DropItems(items);
    }

    void DropItems(List<ItemData> dropItems)
    {
        foreach (ItemData dropItem in dropItems)
        {
            ItemData data = dropItem;

            GameObject model;
            if (prefabinitilizer != null)
            {
                model = prefabinitilizer.GetPrefabToInstantiate(data, SP.DefaultItemDrop);
            }
            else if (GlobalPrefabInitilizer.prefabInit != null)
            {
                model = GlobalPrefabInitilizer.prefabInit.GetPrefabToInstantiate(data, SP.DefaultItemDrop);
            }
            else
            {
                model = SP.DefaultItemDrop;
            }
            gameItemDrop.DropItemIntoWorld(data, dropTransform.position, model);     
        }
    }
}
