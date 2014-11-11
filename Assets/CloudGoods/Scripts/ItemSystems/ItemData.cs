using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class ItemData
{
    internal ItemContainer ownerContainer = null;
    internal int stackSize = 0;

    public string itemName = "";
    internal Guid stackID = Guid.Empty;
    internal int classID = 0;
    internal int CollectionID = 0;
    internal int ItemID = 0;
    internal int totalEnergy = 0;
    internal int baseEnergy = 0;
    internal int salePrice = 0;
    internal List<BehaviourDefinition> behaviours = new List<BehaviourDefinition>();
    internal string description = "";
    internal int quality = 0;
    internal string imageName = "";
    internal bool isOwned = false;
    internal int persistantLocation = -1;

    internal Dictionary<string, float> stats;
    internal string assetURL;
    internal List<string> tags;

    public bool isLocked = false;

    /// <summary>
    /// Visual UI item reference.
    /// </summary>

    public ItemDataComponent uiReference;

    public override string ToString()
    {
        return "ItemData {itemName: " + itemName + " itemID: " + CollectionID + " totalEnergy:" + totalEnergy + " salePrice: " + salePrice + " quality: " + quality + " isOwned: " + isOwned + " imageName: " + imageName+"}";
    }

    public void AssetBundle(Action<UnityEngine.Object> callBack)
    {
        try
        {
            SocialPlay.Bundles.BundleSystem.Get(assetURL, callBack, "Items", true);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            callBack(null);
        }
    }

    public virtual void CreatNew(out ItemData newItem, int amount, ItemContainer ownerContainer)
    {
        newItem = new ItemData();
        newItem.stackSize = amount;
        newItem.ownerContainer = ownerContainer;
        newItem.itemName = itemName;
        newItem.classID = classID;
        newItem.CollectionID = CollectionID;
        newItem.stackID = stackID;
        newItem.totalEnergy = totalEnergy;
        newItem.baseEnergy = baseEnergy;
        newItem.salePrice = salePrice;
        newItem.behaviours = behaviours;
        newItem.description = description;
        newItem.quality = quality;
        newItem.imageName = imageName;
        newItem.isOwned = isOwned;
        newItem.ItemID = ItemID;
        newItem.stats = stats;
        newItem.assetURL = assetURL;
        newItem.tags = tags;
        newItem.persistantLocation = persistantLocation;
        newItem.isLocked = isLocked;
    }

    ///// <summary>
    ///// Used to create new version of this item
    ///// </summary>
    ///// <returns>new ItemData(); (Overrider for each derived class)</returns>
    //protected virtual ItemData NewItem()
    //{

    //    GameObject tmp = Instantiate(this.gameObject) as GameObject;
    //    tmp.name = "(" + itemID + "," + quality + ")" + itemName;		
    //    return tmp.GetComponent<ItemData>();
    //}

    public virtual bool UseItem()
    {
        return false;
    }

    public bool IsOwned()
    {
        return isOwned;
    }


    public bool IsSameItemAs(ItemData other)
    {
        if (this == null || other == null)
        {
            return false;
        }
        if (ItemID == other.ItemID && isOwned == other.isOwned)
            return true;
        else return false;
    }

    public bool IsStackable(ItemData other)
    {
        if (this == null || other == null)
        {
            return false;
        }
        if (ItemID == other.ItemID && isOwned == other.isOwned)
            return true;
        else return false;
    }

    public void UpdateStackID(string newStackID)
    {
        stackID = new Guid(JToken.Parse(newStackID).ToString());
    }

    public virtual void ExtraGameConversions(ItemDetail detail, GameObject spawnedObject)
    {

    }

    public void SetItemData(ItemData itemData)
    {
        stackSize = itemData.stackSize;
        ownerContainer = itemData.ownerContainer;
        itemName = itemData.itemName;
        classID = itemData.classID;
        CollectionID = itemData.CollectionID;
        stackID = itemData.stackID;
        totalEnergy = itemData.totalEnergy;
        baseEnergy = itemData.baseEnergy;
        salePrice = itemData.salePrice;
        behaviours = itemData.behaviours;
        description = itemData.description;
        quality = itemData.quality;
        imageName = itemData.imageName;
        isOwned = itemData.isOwned;
        ItemID = itemData.ItemID;
        stats = itemData.stats;
        assetURL = itemData.assetURL;
        tags = itemData.tags;
        persistantLocation = itemData.persistantLocation;
        isLocked = itemData.isLocked;
    }
}

