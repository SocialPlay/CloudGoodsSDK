using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionsIDRestriction : MonoBehaviour, IContainerRestriction {

    public List<int> CollectionsIDList= new List<int>();
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
            foreach (int itemID in CollectionsIDList)
            {
                if (itemData.itemID == itemID)
                    return true;
            }

            return false;
        }
        else
        {
            foreach (int itemID in CollectionsIDList)
            {
                if (itemData.itemID == itemID)
                    return false;
            }
            return true;
        }
    }
}
