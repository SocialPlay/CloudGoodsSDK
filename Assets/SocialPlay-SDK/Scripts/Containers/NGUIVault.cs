using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;
using System.Collections.Generic;

public class NGUIVault : MonoBehaviour
{
    public NGUILimitlessGridItemContainerDisplay display;
    public ItemContainer container;

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
        display.SetupWindow();
    }


    void AddItem(ItemData data, bool isSave)
    {
        display.AddDisplayItem(data, this.transform);
        foreach (UIWidget item in data.GetComponentsInChildren<UIWidget>())
        {
            item.enabled = true;
        }
        foreach (MonoBehaviour item in data.GetComponentsInChildren<MonoBehaviour>())
        {
            item.enabled = true;
        }
    }

    void RemoveItem(ItemData data, bool isMovedToAnotherContainer)
    {
        display.RemoveDisplayItem(data);
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
