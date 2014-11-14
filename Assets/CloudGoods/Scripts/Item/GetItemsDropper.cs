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
            gameItemDrop.DropItemIntoWorld(dropItem, dropTransform.position, ItemPrefabInitilizer.GetPrefabToInstantiate(dropItem));
        }
    }
}
