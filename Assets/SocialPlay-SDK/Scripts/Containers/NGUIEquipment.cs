using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;

public class NGUIEquipment : MonoBehaviour
{
    public NGUISlottedItemContainerDisplay display;
    public SlottedItemContainer container;
    public List<StatsPairs> statsDisplays = new List<StatsPairs>();


    void OnEnable()
    {
        container.AddedItem += AddItem;
        container.removedItem += RemoveItem;
    }

    void OnDisable()
    {
        container.AddedItem -= AddItem;
        container.removedItem -= RemoveItem;
    }

    void Start()
    {
        foreach (SlotData pair in display.slots)
        {    
           container.AddSlot(pair.slotID, null, pair.filters,1,pair.priority);
            if (pair.gameObject.GetComponent<SlotKeybinding>())
            {
                pair.gameObject.GetComponent<SlotKeybinding>().bindingPressed += container.OnItemKeybindClick;
            }
        }
        SetStats();
    }

 

    void AddItem(ItemData data, bool isSave)
    {
        display.AddDisplayItem(data as ItemData, this.transform);

        int containedSlot = container.FindItemInSlot(data);
        display.SetToSlot(containedSlot, data as ItemData);

        foreach (UIWidget item in data.GetComponentsInChildren<UIWidget>())
        {
            item.enabled = true;
        }
        foreach (MonoBehaviour item in data.GetComponentsInChildren<MonoBehaviour>())
        {
            item.enabled = true;
        }
        SetStats();
        
    }

    void SetStats()
    {
        foreach (StatsPairs sta in statsDisplays)
        {
            if (string.IsNullOrEmpty(sta.statName) || sta.label == null)
                continue;
            sta.label.text = sta.statName + ": ";
            if (container.stats.ContainsKey(sta.statName))
            {
                sta.label.text += container.stats[sta.statName].ToString();
            }
            else
            {
                sta.label.text += "0";
            }
        }
    }

    void RemoveItem(ItemData data, bool isMovedToAnotherContainer)
    {
        display.RemoveDisplayItem(data as ItemData);
        SetStats();
    }

    void Update()
    {
        if (display.isWindowActive && !container.IsActive)
        {
            display.HideWindow();
        }
        if (!display.isWindowActive && container.IsActive)
        {
            display.ShowWindow();
        }
    }

 }

[System.Serializable]
public class StatsPairs
{
    public string statName;
    public UILabel label;
}
