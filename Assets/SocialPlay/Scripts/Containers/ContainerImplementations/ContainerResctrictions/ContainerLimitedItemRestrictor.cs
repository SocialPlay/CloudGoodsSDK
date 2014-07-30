using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ItemContainer))]
public class ContainerLimitedItemRestrictor : MonoBehaviour, IContainerRestrictor {

    public int ContainerItemLimit = 0;

    ItemContainer itemContainer;

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        itemContainer = GetComponent<ItemContainer>();
        itemContainer.ContainerRestrictor = this;
    }

    public bool CanAddToContainer(ItemData itemData)
    {
        if (itemContainer.containerItems.Count >= ContainerItemLimit)
            return false;
        else
            return true;
    }
}
