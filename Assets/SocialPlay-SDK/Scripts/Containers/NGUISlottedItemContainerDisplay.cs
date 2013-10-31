using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class NGUISlottedItemContainerDisplay : ContainerDisplay
{
    public SlotData[] slots;
    public string containerName;

    public override void SetupWindow()
    {
        if (slots == null)
            throw new System.Exception("Slots is not initialized.");
        base.SetupWindow();
    }

    public override void AddDisplayItem(ItemData itemData, Transform parent)
    {
        //itemData.ShowEffect();
    }

    public void SetToSlot(int slot, ItemData itemData)
    {
        if (slot == -1)
            return;
        itemData.transform.parent = slots[slot].transform;
        itemData.transform.localPosition = new Vector3(0, 0, -1);
        itemData.transform.localScale = Vector3.one;
    }

    public override void RemoveDisplayItem(ItemData itemData)
    {
        Destroy(itemData.gameObject);
    }


 
}
