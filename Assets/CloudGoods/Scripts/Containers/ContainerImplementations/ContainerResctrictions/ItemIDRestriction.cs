using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemIDRestriction : MonoBehaviour, IContainerRestriction {

    public List<int> ItemIDList = new List<int>();
    public bool IsExcluded = false;

    ItemContainer restrictedContainer;

    void Awake()
    {
        restrictedContainer = GetComponent<ItemContainer>();
        restrictedContainer.containerAddRestrictions.Add(this);
    }

    public bool IsRestricted(ContainerAction action, ItemData itemData)
    {
        if (IsExcluded)
        {
            foreach (int itemID in ItemIDList)
            {
                if (itemData.ItemID == itemID)
                {
                    Debug.LogWarning("Item Resticted for being added to container because it has a Item ID Restriction");
                    return true;
                }
            }

            return false;
        }
        else
        {
            foreach (int itemID in ItemIDList)
            {
                if (itemData.ItemID == itemID)
                    return false;
            }

            Debug.LogWarning("Item Resticted for being added to container because it has a Item ID Restriction");
            return true;
        }
    }
}
