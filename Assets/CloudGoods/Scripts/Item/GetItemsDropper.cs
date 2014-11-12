using UnityEngine;
using System.Collections.Generic;

public class GetItemsDropper : MonoBehaviour, IGetItems
{
    static public GameObject dropParent { get { if (mDrop == null) mDrop = new GameObject("DroppedItems"); return mDrop; } }
    static GameObject mDrop;

    public Transform dropTransform;
    //public ItemPrefabInitilizer prefabinitilizer;   
    public IGameObjectAction postDropObjectAction;

    ItemDrop gameItemDrop;

    void Awake()
    {
        gameItemDrop = GetComponent<ItemDrop>();
        if (gameItemDrop == null) gameItemDrop = gameObject.AddComponent<ItemDrop>();
    }

    public void GetGameItem(List<ItemData> items)
    {
        DropItems(items);
    }

    void DropItems(List<ItemData> dropItems)
    {
        foreach (ItemData dropItem in dropItems)
        {
<<<<<<< HEAD
            gameItemDrop.DropItemIntoWorld(dropItem, dropTransform.position, ItemPrefabInitilizer.GetPrefabToInstantiate(dropItem));     
=======
            ItemData data = dropItem;

            GameObject model;

            if (prefabinitilizer != null)
            {
                model = prefabinitilizer.GetPrefabToInstantiate(data, CloudGoods.DefaultItemDrop);
            }
            else
            {
                model = CloudGoods.DefaultItemDrop;
            }
            gameItemDrop.DropItemIntoWorld(data, dropTransform.position, model);     
>>>>>>> b1e21c1f703b4d6b9bd208a9727fc19df2589341
        }
    }
}
