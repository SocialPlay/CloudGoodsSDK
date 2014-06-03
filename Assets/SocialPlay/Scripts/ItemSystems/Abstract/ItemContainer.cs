using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour
{
    [HideInInspector]
    public List<ItemData> containerItems;

    /// <summary>
    /// Called after the contaienr added an item.
    /// </summary>
    public event Action<ItemData, bool> AddedItem;

    /// <summary>
    /// Called after the Container itemData the item stack size or location in the Container
    /// </summary>
    public event Action<ItemData, bool> ModifiedItem;

    /// <summary>
    /// Called after the Container removes an item.
    /// </summary>
    public event Action<ItemData, int, bool> RemovedItem;

    /// <summary>
    /// Called when the Container is cleared.
    /// </summary>
    public event Action ClearItems;

    /// <summary>
    /// Called After the user clicks an item
    /// </summary>
    public event Action<ItemData> ItemSingleClicked;

    /// <summary>
    /// Called after the user double clicks an item
    /// </summary>
    public event Action<ItemData> ItemDoubleClicked;

    /// <summary>
    /// Called after the user right clicks an item
    /// </summary>
    public event Action<ItemData> ItemRightClicked;

    /// <summary>
    /// Called after a user presses a key that is linked to the Container(only for slotted containers)
    /// </summary>
    public event Action<ItemData> ItemKeyBindingClicked;

    private ItemContainerRestrictor restriction = null;

    private bool isPerformingAction = false;

    protected void ModifiedItemEvent(ItemData item, bool isSave)
    {
        if (ModifiedItem != null)
        {
            ModifiedItem(item, isSave);
        }
    }

    protected void ClearItemEvent()
    {
        if (ClearItems != null)
        {
            ClearItems();
        }
    }

    protected void AddItemEvent(ItemData item, bool isSave)
    {
        if (AddedItem != null)
        {
            AddedItem(item, isSave);
        }
    }

    protected void RemoveItemEvent(ItemData item, int amount, bool isMoving)
    {
        if (RemovedItem != null)
        {
            RemovedItem(item, amount, isMoving);
        }
    }

    public ContainerAddState GetContainerAddState(ItemData itemData)
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
        if (itemData.isLocked)
        {
            return new ContainerAddState(ContainerAddState.ActionState.No);
        }

        return MyContainerAddState(itemData);
    }

    protected abstract ContainerAddState MyContainerAddState(ItemData modified);




    public void Add(ItemData itemData, int amount = -1, bool isSave = true)
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

        AddItem(itemData, amount, isSave);

    }


    public void Remove(ItemData itemData, bool isMoving, int amount = -1)
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
        RemoveItem(itemData, isMoving, amount);
    }

    protected abstract void AddItem(ItemData modified, int amount = -1, bool isSave = true);
    protected abstract void RemoveItem(ItemData modified, bool isMoving, int amount = -1);
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

    public void OnItemRightCliked(ItemData item)
    {
        if (!isPerformingAction && ItemRightClicked != null)
        {
            isPerformingAction = true;
            ItemRightClicked(item);
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

    public void FinishActionCycle()
    {
        isPerformingAction = false;
    }
}

