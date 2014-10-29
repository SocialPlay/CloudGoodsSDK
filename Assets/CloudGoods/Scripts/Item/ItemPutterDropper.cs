using UnityEngine;
using System.Collections.Generic;

public class ItemPutterDropper : MonoBehaviour, IItemPutter
{
    static public GameObject dropParent { get { if (mDrop == null) mDrop = new GameObject("DroppedItems"); return mDrop; } }
    static GameObject mDrop;

    public Transform dropTransform;
    public ItemPrefabInitilizer prefabinitilizer;   
    public IGameObjectAction postDropObjectAction;

    ItemDrop gameItemDrop;

    void Awake()
    {
        gameItemDrop = GetComponent<ItemDrop>();
        if (gameItemDrop == null) gameItemDrop = gameObject.AddComponent<ItemDrop>();
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
                model = prefabinitilizer.GetPrefabToInstantiate(data, CloudGoods.DefaultItemDrop);
            }
            else if (GlobalPrefabInitilizer.prefabInit != null)
            {
                model = GlobalPrefabInitilizer.prefabInit.GetPrefabToInstantiate(data, CloudGoods.DefaultItemDrop);
            }
            else
            {
                model = CloudGoods.DefaultItemDrop;
            }
            gameItemDrop.DropItemIntoWorld(data, dropTransform.position, model);     
        }
    }
}
