using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class EquipModelDisplay : MonoBehaviour
{

    public GameObject CharacterToSpawnOn;
    SlottedItemContainer equipment;

    static GameObject ActiveModel;

    ItemData currentItem;



    void OnEnable()
    {
        equipment.AddedItem += equipment_AddedItem;
        equipment.RemovedItem += equipment_removedItem;
    }

    void equipment_removedItem(ItemData item, int amount, bool isMovedToAnotherContainer)
    {
        if (item.assetURL == currentItem.assetURL && amount == item.stackSize)
        {
            Destroy(ActiveModel);
        }
    }

    void OnDisable()
    {
        equipment.AddedItem -= equipment_AddedItem;
        equipment.RemovedItem -= equipment_removedItem;
    }

    void equipment_AddedItem(ItemData item, bool isSave)
    {
        item.AssetBundle(GetItemCallback);
        currentItem = item;
    }


    void GetItemCallback(UnityEngine.Object asset)
    {
        if (ActiveModel != null)
        {
            Destroy(ActiveModel); //Clears old bundle prefabs
        }
        if (asset != null)
        {
            ActiveModel = Instantiate(asset) as GameObject;
            ActiveModel.transform.parent = CharacterToSpawnOn.transform;
        }
    }
}
