using UnityEngine;
using System.Collections;
using System;

public class ContainerItemComponentUseAction : ContainerActions {

    public override void DoAction(ItemData item)
    {
        try
        {
            IItemComponent itemComponent = item.gameObject.GetComponent(typeof(IItemComponent)) as IItemComponent;
            itemComponent.UseItem(item);
        }
        catch (Exception ex)
        {
            Debug.LogError("No IItemComponent was attached to item data object, unable to call use item function");
        }
    }
}
