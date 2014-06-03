using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotData : MonoBehaviour
{
    /// <summary>
    /// The number of item that can fit into this slot (default 1)
    /// </summary>
    public int slotSizeLimit = 1;

    /// <summary>
    /// The priority of the slot. higher number mean this slot will be filled before others.
    /// </summary>
    public int priority = 0;

    /// <summary>
    /// The ID of the slot. Can NOT be deplicate.
    /// </summary>
    public int slotID;


    /// <summary>
    /// The location to save this slot to. if -1 no persistancy will be applied to this slot.
    /// </summary>
    public int persistantLocationID =-1;

    internal List<ItemFilterSystem> filters = new List<ItemFilterSystem>();


    /// <summary>
    /// Gets all attached slot filters.
    /// </summary>
    void Awake()
    {
        foreach (SlotItemFilter filter in GetComponents<SlotItemFilter>())
        {
            filters.Add(filter.filter);
        }
    }
}
