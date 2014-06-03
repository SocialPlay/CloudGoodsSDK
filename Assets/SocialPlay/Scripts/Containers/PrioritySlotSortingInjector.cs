using UnityEngine;
using System.Collections;


public class PrioritySlotSortingInjector : MonoBehaviour
{
    void Awake()
    {
        SlottedItemContainer.slotSelector = new PrioritySelector();
    }
}
