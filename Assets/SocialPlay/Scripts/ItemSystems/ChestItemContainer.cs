using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class ChestItemContainer : ItemContainer
{
    public string chestName = string.Empty;
    public List<ItemData> chestItems;

    public ChestItemContainer(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new System.Exception("Chest Email is not set");

        chestName = name;
        chestItems = new List<ItemData>();
    }

    protected override ContainerAddState MyContainerAddState(ItemData modified)
    {
        return new ContainerAddState(ContainerAddState.ActionState.No, 0);
    }

    protected override void AddItem(ItemData modified, int amount = -1, bool isSave = true)
    {
        if (amount == -1)
        {
            modified.ownerContainer = this;
            if (!AddToExistingStack(modified, modified.stackSize))
            {
                AddItemEvent(modified, isSave);
                chestItems.Add(modified);
            }
        }
        else
        {
            ItemData newItem = null;
            modified.CreatNew(out newItem, amount, this);
            if (!AddToExistingStack(newItem, amount))
            {
                AddItemEvent(newItem, isSave);
                chestItems.Add(newItem);
            }
        }
    }

    private bool AddToExistingStack(ItemData data, int amount = -1)
    {
        foreach (ItemData item in chestItems)
        {
            if (item.IsSameItemAs(data))
            {
                item.stackSize += amount;
                return true;
            }
        }
        return false;
    }

    protected override void RemoveItem(ItemData modified, bool isMovingToAnotherContainer, int amount = -1)
    {
        foreach (ItemData item in chestItems)
        {
            if (item.gameObject == modified.gameObject)
            {
                if (amount == -1 || item.stackSize <= amount)
                {                   
                    chestItems.Remove(item);
                }
                else
                    item.stackSize -= amount;
                RemoveItemEvent(modified,amount, isMovingToAnotherContainer);
                return;
            }
        }
        return;
    }

    public override int Contains(ItemData modified)
    {
        foreach (ItemData item in chestItems)
        {
            if (item.IsSameItemAs(modified))
                return item.stackSize;
        }

        return 0;
    }

    public override void Clear()
    {
        chestItems.Clear();
        ClearItemEvent();
    }
}

