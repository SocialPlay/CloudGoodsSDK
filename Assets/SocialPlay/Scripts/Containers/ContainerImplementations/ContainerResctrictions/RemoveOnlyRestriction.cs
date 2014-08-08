using UnityEngine;
using System.Collections;

public class RemoveOnlyRestriction : MonoBehaviour, IContainerRestriction {

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
            Debug.LogWarning("Item Resticted for being added to container because it has a Remove Only Restriction");
            return true;
        }
        else
            return false;
    }
}
