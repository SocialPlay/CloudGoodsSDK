using UnityEngine;
using System.Collections;
using System;

public class ContainerItemComponentUseAction : ContainerActions {

    public override void DoAction(ItemDataComponent itemObject)
    {
        ItemData item = itemObject.GetComponent<ItemDataComponent>().itemData;
        try
        {
            IItemComponent itemComponent = itemObject.GetComponent(typeof(IItemComponent)) as IItemComponent;
            itemComponent.UseItem(item);
        }
        catch (Exception ex)
        {
            Debug.LogError("No IItemComponent was attached to item data object, unable to call use item function," + ex.Message);
        }
    }
}
