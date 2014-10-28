using UnityEngine;
using System.Collections;


public class ContainerMoveItemAction : ContainerActions {

    public ItemContainer MoveToContainer;

    public override void DoAction(ItemDataComponent itemObject)
    {
        ItemData itemData = itemObject.GetComponent<ItemDataComponent>().itemData;
        ItemContainerManager.MoveItem(itemData, itemData.ownerContainer, MoveToContainer);
    } 
}
