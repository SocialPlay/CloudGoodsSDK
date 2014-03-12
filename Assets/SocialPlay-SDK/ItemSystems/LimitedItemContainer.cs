using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[System.Serializable]
public class LimitedItemContainer : ItemContainer
{
    public int containerMaxSize = 0;

    public LimitedItemContainer(int size)
    {
        if (size <= 0)
            throw new System.Exception("Container size is not set");

        containerMaxSize = size;
    }

    protected override ContainerAddState MyContainerAddState(ItemData modified)
    {

       
        ContainerAddState currentAddableState = new ContainerAddState(ContainerAddState.ActionState.No, 0);

        if (containerItems.Count == containerMaxSize && !containerItems.Contains(modified))
        {
            return currentAddableState; //No room for this item.
        }


        //TODO max stack size to be properly retreived
        int maxStackSizePerSlot = -1;


        if (maxStackSizePerSlot == -1)
        {
            currentAddableState.actionState = ContainerAddState.ActionState.Add;
            currentAddableState.possibleAddAmount = modified.stackSize;
            return currentAddableState;
        }

        int amountAvalibleInCurrentStacks = 0;

        foreach (ItemData itemIterator in containerItems)
        {
            if (itemIterator.varianceID == modified.varianceID)
            {
                amountAvalibleInCurrentStacks += maxStackSizePerSlot - itemIterator.stackSize;
            }
        }

        if (amountAvalibleInCurrentStacks >= modified.stackSize)
        {
            currentAddableState.actionState = ContainerAddState.ActionState.Add;
            currentAddableState.possibleAddAmount = modified.stackSize;
            return currentAddableState;
        }

        int neededSpace = modified.stackSize - amountAvalibleInCurrentStacks;
        int PossibleEmpytSpace = (containerMaxSize - containerItems.Count) * maxStackSizePerSlot;

        if (modified.stackSize > amountAvalibleInCurrentStacks)
        {
            int numberOfSlotsNeededToFill = (int)Math.Ceiling(((float)modified.stackSize - ((float)maxStackSizePerSlot - (float)amountAvalibleInCurrentStacks)) / (float)maxStackSizePerSlot);
            if (neededSpace < PossibleEmpytSpace)
            {
                currentAddableState.possibleAddAmount = neededSpace + amountAvalibleInCurrentStacks;
            }
            else
            {
                currentAddableState.possibleAddAmount = PossibleEmpytSpace + amountAvalibleInCurrentStacks;
            }
            currentAddableState.actionState = ContainerAddState.ActionState.Add;
        }
        else
        {
            currentAddableState.actionState = ContainerAddState.ActionState.Add;
            currentAddableState.possibleAddAmount = modified.stackSize;
        }
        return currentAddableState;
    }

    protected override void AddItem(ItemData modified, int amount = -1, bool isSave = true)
    {
        if (amount == -1)
            amount = modified.stackSize;
        int maxStackSizePerSlot = -1;

        foreach (ItemData current in containerItems)
        {
            if (current.IsStackable(modified))
            {
                if (maxStackSizePerSlot == -1)
                {
                    current.stackSize += amount;
                    ModifiedItemEvent(modified, isSave);
                    Destroy(modified.gameObject);
                    return;
                }
                int currentStackEmptySpace = maxStackSizePerSlot - current.stackSize;
                if (currentStackEmptySpace > 0)
                {
                    current.stackSize += currentStackEmptySpace;
                    amount -= currentStackEmptySpace;
                }
            }
        }

        if (maxStackSizePerSlot == -1)
        {
            AddNewStackAmount(modified, modified.stackSize, isSave); //Add only one new stack.
            return;
        }

        while (amount > maxStackSizePerSlot)
        {
            AddNewStackAmount(modified, maxStackSizePerSlot, isSave);
            amount -= maxStackSizePerSlot;
        }
        if (amount > 0)
        {
            AddNewStackAmount(modified, amount, isSave);
        }

        Destroy(modified.gameObject);
    }

    private void AddNewStackAmount(ItemData data, int amount, bool isSave)
    {
        ItemData newItem;
        data.CreatNew(out newItem, amount, this);
        Destroy(data.gameObject);

        AddItemEvent(newItem, isSave);
        containerItems.Add(newItem);
    }

    private bool AddToExistingStack(ItemData data, int amount = -1)
    {
        foreach (ItemData item in containerItems)
        {
            if (item.varianceID.Equals(data.varianceID))
            {
                item.stackSize += amount;
                data.stackSize -= amount;

                if (data.stackSize <= 0)
                    Destroy(data.gameObject);

                return true;
            }
        }
        return false;
    }

    protected override void RemoveItem(ItemData modified, bool isMoving, int amount = -1)
    {
        foreach (ItemData item in containerItems)
        {
            if (item.itemName.Equals(modified.itemName))
            {
                if (amount == -1 || item.stackSize <= amount)
                {
                    containerItems.Remove(item);

                }
                else
                {
                    item.stackSize -= amount;
                }
                RemoveItemEvent(item, amount, isMoving);
                return;
            }
        }
        return;
    }

    public override int Contains(ItemData itemData)
    {
        foreach (ItemData item in containerItems)
        {
            if (item.IsSameItemAs(itemData))
            {
                return item.stackSize;
            }
        }
        return 0;
    }

    public override void Clear()
    {
        foreach (ItemData item in containerItems)
        {
            Destroy(item.gameObject);
        }

        containerItems.Clear();
        ClearItemEvent();
    }
}

