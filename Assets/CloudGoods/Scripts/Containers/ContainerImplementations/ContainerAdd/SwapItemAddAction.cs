using UnityEngine;
using System.Collections;

public class SwapItemAddAction : MonoBehaviour, IContainerAddAction
{
    // which index of the container items will be swapped (default first item)
    public int swapIndex = 0;

    //The amount of items needed in container until it is required to swap
    public int swapItemLimit = 0;

    private ItemContainer itemContainer;

    void Awake()
    {
        itemContainer = GetComponent<ItemContainer>();
    }

    public void AddItem(ItemData addItem, int amount, bool isSave)
    {
        if (IsSwapNeeded())
        {
            //Should only swap single item in container (first item in container items list)
            ItemData swapItem = itemContainer.containerItems[swapIndex];

            ItemContainerManager.MoveItem(swapItem, itemContainer, addItem.ownerContainer);
            AddItemToContainer(addItem, amount, isSave);
        }
        else
        {
            AddItemToContainer(addItem, amount, isSave);
        }
    }

    void AddItemToContainer(ItemData addItem, int amount, bool isSave)
    {
        if (amount == -1)
        {
            amount = addItem.stackSize;
            addItem.ownerContainer = itemContainer;

            itemContainer.containerItems.Add(addItem);
            itemContainer.AddItemEvent(addItem, isSave);

        }
        else
        {
            addItem.stackSize = amount;
            addItem.ownerContainer = itemContainer;

            itemContainer.containerItems.Add(addItem);
            itemContainer.AddItemEvent(addItem, isSave);
        }
    }

    bool IsSwapNeeded()
    {
        if (itemContainer.containerItems.Count >= swapItemLimit)
            return true;
        else
            return false;
    }


}
