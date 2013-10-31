using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotData : MonoBehaviour 
{
    public int priority = 0;
    public int slotID;
    internal List<ItemFilterSystem> filters = new List<ItemFilterSystem>();

    void Awake()
    {
        foreach (SlotItemfilter filter in GetComponents<SlotItemfilter>())
        {
            filters.Add(filter.filter);
        }
    }
}
