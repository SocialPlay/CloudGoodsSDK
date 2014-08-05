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
            return true;
        else
            return false;
    }
}
