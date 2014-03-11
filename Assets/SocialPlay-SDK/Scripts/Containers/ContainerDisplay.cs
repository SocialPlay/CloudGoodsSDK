using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public abstract class ContainerDisplay : MonoBehaviour
{

    public GameObject ContainerDisplayObject;
    public ItemContainer itemContainer = null;
    public bool StartWindowActive = true;

    protected bool isActive = true;

    private List<ContainerDisplayAction> disaplyActions = new List<ContainerDisplayAction>();


    protected virtual void OnEnable()
    {
        itemContainer.AddedItem += AddedItem;
        itemContainer.RemovedItem += RemovedItem;
    }


    protected virtual void OnDisable()
    {
        itemContainer.AddedItem -= AddedItem;
        itemContainer.RemovedItem -= RemovedItem;
    }

    protected virtual void Start()
    {
        SetupWindow();
    }

    protected virtual void Update()
    {
        if (!isActive)
        {
            HideWindow();
        }
        if (isActive)
        {
            ShowWindow();
        }
    }

    protected virtual void AddedItem(ItemData data, bool isSave)
    {
        AddDisplayItem(data as ItemData, this.transform);
    }

    protected virtual void RemovedItem(ItemData data, int amount, bool isBeingMoved)
    {
        if (!isBeingMoved)
        {
            if (data.stackSize - amount < 0)
            {
                RemoveDisplayItem(data as ItemData);
            }
        }
        else if (amount == -1 || data.stackSize == amount)
        {
            RemoveDisplayItem(data as ItemData);
        }
    }

    protected virtual void SetupWindow()
    {
        isActive = StartWindowActive;
        ContainerDisplayObject.SetActive(true);
    }

    private void GetDisplayActions()
    {
        if (GetComponentsInChildren<ContainerDisplayAction>().Length != 0)
        {
            foreach (ContainerDisplayAction action in GetComponentsInChildren<ContainerDisplayAction>())
            {
                disaplyActions.Add(action);
            }
        }

        if (GetComponents<ContainerDisplayAction>().Length != 0)
        {
            foreach (ContainerDisplayAction action in GetComponents<ContainerDisplayAction>())
            {
                disaplyActions.Add(action);
            }
        }
    }

    public virtual void ShowWindow()
    {
        if (isActive) return;
        isActive = true;
        foreach (ContainerDisplayAction action in disaplyActions)
        {
            action.Activate();
        }
    }

    public virtual void HideWindow()
    {
        if (!isActive) return;
        foreach (ContainerDisplayAction action in disaplyActions)
        {
            action.Deactivate();
        }
        isActive = false;
    }

    public bool IsWindowActive()
    {
        return isActive;
    }

    public void SetWindowIsActive(bool state)
    {
        isActive = state;
    }

    public abstract void AddDisplayItem(ItemData itemData, Transform parent);
    public abstract void RemoveDisplayItem(ItemData itemData);
}
