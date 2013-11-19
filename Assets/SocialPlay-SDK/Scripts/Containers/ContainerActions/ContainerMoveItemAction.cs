using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class ContainerMoveItemAction : ContainerActions {

    public ItemContainer MoveToContainer;

    public override void DoAction(ItemData item)
    {
        ItemContainerManager.MoveItem(item, MoveToContainer);
    } 
}
