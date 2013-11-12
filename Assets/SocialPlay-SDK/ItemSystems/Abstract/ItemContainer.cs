using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour
{
    public List<ItemData> containerItems;
    public bool IsActive = false;

    public virtual event Action<ItemData, bool> AddedItem;
    public virtual event Action<ItemData, bool> ModifiedItem;
    public virtual event Action<ItemData, bool> removedItem;

    public Action<ItemData> ItemSingleClicked;
    public Action<ItemData> ItemDoubleClicked;
    public Action<ItemData> ItemRightClicked;
    public Action<ItemData> ItemKeyBindingClicked;

    private ItemContainerRestrictor restriction = null;

    private bool isPerformingAction = false;

    protected void ModifiedItemEvent(ItemData item, bool isSave)
    {
        if (ModifiedItem != null)
        {
            ModifiedItem(item, isSave);
        }
    }


    protected void AddItemEvent(ItemData item, bool isSave)
    {
        if (AddedItem != null)
        {
            AddedItem(item, isSave);
        }
    }

    protected void RemoveItemEvent(ItemData item, bool isMovingToAnotherContainer)
    {
        if (removedItem != null)
        {
            removedItem(item, isMovingToAnotherContainer);
        }
    }

    public ContainerAddState GetContainerAddState(ItemData modified)
    {
        if (restriction == null)
        {
            restriction = this.GetComponentInChildren<ItemContainerRestrictor>();
        }

        if (restriction != null)
        {
            if (restriction.IsRestricted(ItemContainerRestrictor.ContainerAction.add))
            {
                return new ContainerAddState(ContainerAddState.ActionState.No);
            }
        }
        return MyContainerAddState(modified);
    }

    protected abstract ContainerAddState MyContainerAddState(ItemData modified);




    public void Add(ItemData modified, int amount = -1, bool isSave = true)
    {
        if (restriction == null)
        {
            restriction = this.GetComponentInChildren<ItemContainerRestrictor>();
        }

        if (restriction != null)
        {
            if (restriction.IsRestricted(ItemContainerRestrictor.ContainerAction.add))
            {
                return;
            }
        }

        AddItem(modified, amount, isSave);

    }


    public void Remove(ItemData modified, bool isMovingToAnotherContainer, int amount = -1)
    {
        if (restriction == null)
        {
            restriction = this.GetComponentInChildren<ItemContainerRestrictor>();
        }

        if (restriction != null)
        {
            if (restriction.IsRestricted(ItemContainerRestrictor.ContainerAction.remove))
            {
                return;
            }
        }
        RemoveItem(modified, isMovingToAnotherContainer, amount);
    }

    protected abstract void AddItem(ItemData modified, int amount = -1, bool isSave = true);
    protected abstract void RemoveItem(ItemData modified, bool isMovingToAnotherContainer, int amount = -1);
    public abstract int Contains(ItemData modified);
    public abstract void Clear();

    public void OnItemSingleClick(ItemData item)
    {
        if (!isPerformingAction && ItemSingleClicked != null)
        {
            isPerformingAction = true;
            ItemSingleClicked(item);
        }
    }

    public void OnItemDoubleCliked(ItemData item)
    {
        if (!isPerformingAction && ItemDoubleClicked != null)
        {
            isPerformingAction = true;
            ItemDoubleClicked(item);
        }
    }

    public void OnItemKeybindClick(ItemData item)
    {
        if (!isPerformingAction && ItemKeyBindingClicked != null)
        {
            isPerformingAction = true;
            ItemKeyBindingClicked(item);
        }
    }

    public void finishActionCycle()
    {
        isPerformingAction = false;
    }
}

