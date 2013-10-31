using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class PrioritySlotSortingInjector : MonoBehaviour
{
    void Awake()
    {
        SlottedItemContainer.slotSelector = new PrioritySelector();
    }
}
