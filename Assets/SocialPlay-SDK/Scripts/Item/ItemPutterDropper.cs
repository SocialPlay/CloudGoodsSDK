using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;

public class ItemPutterDropper : MonoBehaviour, IItemPutter
{

    public GameObject defaultDropModelPrefab;
    public Transform dropTransform;
    public ItemPrefabInitilizer prefabinitilizer;
    ItemDrop gameItemDrop;

    void Start()
    {
        gameItemDrop = gameObject.AddComponent<ItemDrop>();
    }

    public void PutGameItem(List<ItemData> items)
    {
        foreach (ItemData item in items)
        {
            List<ItemData> itemsList = new List<ItemData>();
            itemsList.Add(item);
            DropItems(ItemConverter.ConvertToItemDropObject(itemsList, false));
            Destroy(item.gameObject);
        }
    }

    void DropItems(List<GameObject> dropItems)
    {
        foreach (GameObject dropItem in dropItems)
        {
            ItemData data = dropItem.GetComponent<ItemData>();
            GameObject model;
            if (prefabinitilizer != null)
            {
                model = prefabinitilizer.GetPrefabToInstantiate(data, defaultDropModelPrefab);
            }
            else if (GlobalPrefabInitilizer.prefabInit != null)
            {
                model = GlobalPrefabInitilizer.prefabInit.GetPrefabToInstantiate(data, defaultDropModelPrefab);
            }
            else
            {
                model = defaultDropModelPrefab;
            }

            gameItemDrop.DropItemIntoWorld(data, dropTransform.position, model);
            Destroy(dropItem);
        }
    }



}
