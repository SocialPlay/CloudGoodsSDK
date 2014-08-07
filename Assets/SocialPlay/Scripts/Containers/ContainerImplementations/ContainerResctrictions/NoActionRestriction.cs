using UnityEngine;
using System.Collections;

public class NoActionRestriction : MonoBehaviour, IContainerRestriction {

    ItemContainer restrictedContainer;

    void Awake()
    {
        restrictedContainer = GetComponent<ItemContainer>();
        restrictedContainer.containerRemoveRestrictions.Add(this);
        restrictedContainer.containerAddRestrictions.Add(this);
    }

    public bool IsRestricted(ContainerAction containerAction, ItemData itemData)
    {
        Debug.LogWarning("Item Resticted for being added to or removed from container because it has a No Action Restriction");
        return true;
    }
}
