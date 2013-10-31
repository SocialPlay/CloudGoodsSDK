using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class MySlotSelector : MonoBehaviour
{
    void Awake()
    {
        SlottedItemContainer.slotSelector = new PrioritySelector();
    }
}
