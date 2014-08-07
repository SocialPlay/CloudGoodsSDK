using UnityEngine;
using System.Collections;

public class AddOnlyRestriction : MonoBehaviour, IContainerRestriction {

    ItemContainer restrictedContainer;

    void Awake()
    {
        restrictedContainer = GetComponent<ItemContainer>();
        restrictedContainer.containerRemoveRestrictions.Add(this);
    }

    public bool IsRestricted(ContainerAction containerAction, ItemData itemData)
    {
        if (containerAction == ContainerAction.remove)
        {
            Debug.LogWarning("Item Resticted for being removed from container because it has an Add-Only Restriction");
            return true;
        }
        else
            return false;
    }
}
