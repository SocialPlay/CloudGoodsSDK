using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ItemContainer))]
public class BasicAddContainer : MonoBehaviour, IContainerAddAction
{

    private ItemContainer itemContainer;

    void Awake()
    {
        itemContainer = GetComponent<ItemContainer>();
    }

    public void AddItem(ItemData addItem, int amount, bool isSave)
    {
        if (amount == -1)
        {
            amount = addItem.stackSize;
            addItem.ownerContainer = itemContainer;
            if (!AddToExistingStack(addItem, addItem.stackSize, isSave))
            {
                itemContainer.containerItems.Add(addItem);
                itemContainer.AddItemEvent(addItem, isSave);
            }
        }
        else
        {
            addItem.ownerContainer = itemContainer;
            if (!AddToExistingStack(addItem, amount, isSave))
            {
                addItem.stackSize = amount;
                itemContainer.containerItems.Add(addItem);
                itemContainer.AddItemEvent(addItem, isSave);
            }
        }
    }

    private bool AddToExistingStack(ItemData data, int amount, bool isSave)
    {

        foreach (ItemData item in itemContainer.containerItems)
        {
            if (item.ItemID.Equals(data.ItemID))
            {
                Debug.Log("add to existing stack");

                itemContainer.ModifiedItemEvent(data, isSave);

                data.stackSize -= amount;

                return true;
            }
        }
        return false;
    }


}
