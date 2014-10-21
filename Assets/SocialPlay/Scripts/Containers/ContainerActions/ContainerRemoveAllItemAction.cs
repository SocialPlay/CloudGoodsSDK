using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

public class ContainerRemoveAllItemAction : ContainerActions
{
    public ItemContainer SourceContainer;

    public override void DoAction(ItemDataComponent itemData)
    {
        ItemData[] itemsFromContainer = new ItemData[SourceContainer.containerItems.Count];
        SourceContainer.containerItems.CopyTo(itemsFromContainer);
        List<Guid> stackIDs = new List<Guid>();
        foreach (ItemData data in itemsFromContainer)
        {
            Debug.Log(stackIDs);
            Debug.Log(data.stackID);
            stackIDs.Add(data.stackID);
        }
        CloudGoods.RemoveItemStacks(stackIDs, delegate(string x) { });

        foreach (ItemData data in itemsFromContainer)
        {
            SourceContainer.Remove(data, true);
        }
    }
}
