using UnityEngine;
using System.Collections;


public class ContainerMoveItemAction : ContainerActions {

    public ItemContainer MoveToContainer;

    public override void DoAction(ItemData item)
    {
        ItemContainerManager.MoveItem(item, null, MoveToContainer);
    } 
}
