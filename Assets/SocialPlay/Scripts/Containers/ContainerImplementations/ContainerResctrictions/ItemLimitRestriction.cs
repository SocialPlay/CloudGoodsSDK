using UnityEngine;
using System.Collections;

public class ItemLimitRestriction : MonoBehaviour, IContainerRestriction {

    public int ContainerItemLimit = 0;
    ItemContainer restrictedContainer;

    void Awake()
    {
        restrictedContainer = GetComponent<ItemContainer>();
        restrictedContainer.containerAddRestrictions.Add(this);
    }

    public bool IsRestricted(ContainerAction containerAction, ItemData itemData)
    {
        if (containerAction == ContainerAction.add)
        {
            if (restrictedContainer.containerItems.Count >= ContainerItemLimit)
                return true;
        }

        return false;
    }
}
