using UnityEngine;
using System.Collections;


public class ContainerRemoveItemAction : ContainerActions
{
    public override void DoAction(ItemDataComponent itemObject)
    {
        ItemData itemData = itemObject.GetComponent<ItemDataComponent>().itemData;
        itemData.ownerContainer.Remove(itemData, false, itemData.stackSize);
    }
}
