using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class NGUISlottedItemContainerDisplay : ContainerDisplay
{
    public SlotData[] slots;

    protected override void Start()
    {
        if (itemContainer.GetType() != typeof(SlottedItemContainer)) throw new System.Exception("NGUI Sloted Item Container Disaply requires a Sloted Item Container for ItemContainer");
        base.Start();
        foreach (SlotData slot in slots)
        {
            (itemContainer as SlottedItemContainer).AddSlot(slot.slotID, null, slot.filters, slot.persistantLocationID, slot.slotSizeLimit, slot.priority);
            if (slot.gameObject.GetComponent<SlotKeybinding>())
            {
                slot.gameObject.GetComponent<SlotKeybinding>().BindingPressed += itemContainer.OnItemKeybindClick;
            }
        }
    }

    protected override void AddedItem(ItemData data, bool isSave)
    {
        base.AddedItem(data, isSave);
        int containedSlot = (itemContainer as SlottedItemContainer).FindItemInSlot(data);
        SetToSlot(containedSlot, data as ItemData);

        foreach (UIWidget item in data.GetComponentsInChildren<UIWidget>())
        {
            item.enabled = true;
        }
        foreach (MonoBehaviour item in data.GetComponentsInChildren<MonoBehaviour>())
        {
            if (item != null)
            {
                item.enabled = true;
            }
        }
    }

    protected override void SetupWindow()
    {
        if (slots == null)
            throw new System.Exception("Slots is not initialized.");
        base.SetupWindow();
    }

    void SetToSlot(int slot, ItemData itemData)
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

    public override void AddDisplayItem(ItemData itemData, Transform parent)
    {     
    }
}
