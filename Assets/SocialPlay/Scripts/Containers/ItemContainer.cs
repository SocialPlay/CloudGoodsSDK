using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public List<ItemData> containerItems = new List<ItemData>();

    /// <summary>
    /// Called after the contaienr added an item.
    /// </summary>
    public event Action<ItemData, bool> AddedItem;

    /// <summary>
    /// The developers implementation of what functionality will happen when a user tries to add an item into the container
    /// </summary>
    public IContainerAddAction containerAddAction;

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

    public List<IContainerRestriction> containerAddRestrictions = new List<IContainerRestriction>();
    public List<IContainerRestriction> containerRemoveRestrictions = new List<IContainerRestriction>();

    private bool isPerformingAction = false;

    void Awake()
    {
        if (GetComponent(typeof(IContainerAddAction)) == null)
            containerAddAction = gameObject.AddComponent<BasicAddContainer>();
        else
            containerAddAction = (IContainerAddAction)GetComponent(typeof(IContainerAddAction));
    }

    public void ModifiedItemEvent(ItemData item, bool isSave)
    {
        if (ModifiedItem != null)
        {
            ModifiedItem(item, isSave);
        }
    }

    public void ClearItemEvent()
    {
        if (ClearItems != null)
        {
            ClearItems();
        }
    }

    public void AddItemEvent(ItemData item, bool isSave)
    {
        if (AddedItem != null)
        {
            AddedItem(item, isSave);
        }
    }

    public void RemoveItemEvent(ItemData item, int amount, bool isMoving)
    {
        if (RemovedItem != null)
        {
            RemovedItem(item, amount, isMoving);
        }
    }

    public ContainerAddState GetContainerAddState(ItemData itemData)
    {
        if (itemData.isLocked)
        {
            return new ContainerAddState(ContainerAddState.ActionState.No);
        }

        if (containerAddRestrictions.Count > 0)
        {
            foreach (IContainerRestriction newRestriction in containerAddRestrictions)
            {
                if (newRestriction.IsRestricted(ContainerAction.add, itemData))
                    return new ContainerAddState(ContainerAddState.ActionState.No);
            }
        }

        return MyContainerAddState(itemData);
    }

    public bool GetContainerRemoveState(ItemData itemData)
    {
        if (itemData.isLocked)
        {
            return false;
        }

        if (containerAddRestrictions.Count > 0)
        {
            foreach (IContainerRestriction newRestriction in containerRemoveRestrictions)
            {
                if (newRestriction.IsRestricted(ContainerAction.remove, itemData))
                    return false;
            }
        }

        return true;
    }

    protected ContainerAddState MyContainerAddState(ItemData modified)
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

    public void Add(ItemData itemData, int amount = -1, bool isSave = true)
    {
        containerAddAction.AddItem(itemData, amount, isSave);
    }


    public void Remove(ItemData itemData, bool isMoving, int amount = -1)
    {
        if (ItemContainerStackRestrictor.Restrictor != null)
        {
            if (restriction.IsRestricted(ContainerAction.remove))
            {
                return;
            }
        }
        RemoveItem(itemData, isMoving, amount);
    }

    protected void RemoveItem(ItemData modified, bool isMoving, int amount = -1)
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

    public int Contains(ItemData modified)
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

    public void Clear()
    {
        foreach (ItemData item in containerItems)
        {
            Destroy(item.gameObject);
        }

        containerItems.Clear();
        ClearItemEvent();
    }

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

