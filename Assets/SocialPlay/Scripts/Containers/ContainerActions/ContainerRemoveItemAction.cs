using UnityEngine;
using System.Collections;


public class ContainerRemoveItemAction :  ContainerActions
{
    public override void DoAction(ItemData itemData)
    {
        itemData.ownerContainer.Remove(itemData, false, itemData.stackSize);
    }
}
