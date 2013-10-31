using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class ContainerMoveItemAction : ContainerActions {

    public ItemContainer moveToContainer;

    public override void DoAction(ItemData item)
    {
        ItemContainerManager.MoveItem(item, moveToContainer);
    } 
}
