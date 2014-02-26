using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[System.Serializable]
public class LimitlessItemContainer : ItemContainer
{

    protected override void AddItem(ItemData modified, int amount = -1, bool isSave = true)
    {
        ItemData newItem = null;

        if (amount == -1)
        {
            amount = modified.stackSize;
            modified.ownerContainer = this;
            if (!AddToExistingStack(modified, modified.stackSize, isSave))
            {
                modified.CreatNew(out newItem, amount, this);
                containerItems.Add(newItem);
                AddItemEvent(newItem, isSave);

            }
        }
        else
        {
            modified.CreatNew(out newItem, amount, this);
            if (!AddToExistingStack(newItem, amount, isSave))
            {
                containerItems.Add(newItem);
                AddItemEvent(newItem, isSave);
            }
        }

        Destroy(modified.gameObject);
    }

    private bool AddToExistingStack(ItemData data, int amount, bool isSave)
    {
        foreach (ItemData item in containerItems)
        {
            if (item.varianceID.Equals(data.varianceID))
            {
                item.stackSize += amount;

                ModifiedItemEvent(data, isSave);

                data.stackSize -= amount;

                if (data.stackSize <= 0)
                    Destroy(data.gameObject);

                return true;
            }
        }
        return false;
    }

    protected override void RemoveItem(ItemData modified, bool isMovingToAnotherContainer, int amount = -1)
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
                RemoveItemEvent(item, amount, isMovingToAnotherContainer);
                return;
            }
        }
        return;
    }

    public override int Contains(ItemData modified)
    {
        foreach (ItemData item in containerItems)
        {
            if (item.IsSameItemAs(modified))
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

    protected override ContainerAddState MyContainerAddState(ItemData modified)
    {
        int addAbleAmount = modified.stackSize;
        if (ItemContainerStackRestrictor.Restrictor != null)
        {
            int restrictionAmount = ItemContainerStackRestrictor.Restrictor.GetRestrictedAmount(modified, this);

            if (restrictionAmount != -1 && restrictionAmount > modified.stackSize)
            {
                addAbleAmount = restrictionAmount;
            }
        }


        return new ContainerAddState(ContainerAddState.ActionState.Add, addAbleAmount);
    }
}

