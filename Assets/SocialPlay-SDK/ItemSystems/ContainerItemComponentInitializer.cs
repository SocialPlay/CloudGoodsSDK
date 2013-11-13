using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerItemComponentInitializer : MonoBehaviour {

    ItemContainer itemContainer;

    void Start()
    {
        itemContainer = gameObject.GetComponent<ItemContainer>();
        itemContainer.AddedItem += itemContainer_AddedItem;
    }

    void itemContainer_AddedItem(ItemData item, bool isSaved)
    {
        ItemComponentInitalizer.InitializeItemWithComponents(item,AddComponetTo.container);
    }
}
