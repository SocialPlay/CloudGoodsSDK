using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ContainerDisplay : MonoBehaviour
{
    internal UIGrid viewArea;
    public GameObject ContainerDisplayObject;
    public ItemContainer itemContainer = null;
    public bool StartWindowActive = true;

    protected bool isActive = true;

    private List<ContainerDisplayAction> disaplyActions = new List<ContainerDisplayAction>();


    protected void OnEnable()
    {
        itemContainer.AddedItem += AddedItem;
        itemContainer.RemovedItem += RemovedItem;
    }


    protected void OnDisable()
    {
        itemContainer.AddedItem -= AddedItem;
        itemContainer.RemovedItem -= RemovedItem;
    }

    protected void Start()
    {
        SetupWindow();
    }

    protected void Update()
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

    protected void AddedItem(ItemData itemData, bool isSave)
    {
        itemData.transform.parent = viewArea.transform;
        itemData.transform.localPosition = new Vector3(0, 0, -1);
        itemData.transform.localScale = Vector3.one;
        foreach (UIWidget item in itemData.GetComponentsInChildren<UIWidget>())
        {
            item.enabled = true;
        }
        foreach (MonoBehaviour item in itemData.GetComponentsInChildren<MonoBehaviour>())
        {
            if (item != null)
            {
                item.enabled = true;
            }
        }
        viewArea.repositionNow = true;
    }

    protected void RemovedItem(ItemData itemData, int amount, bool isBeingMoved)
    {
        if (!isBeingMoved)
        {
            if (itemData.stackSize - amount < 0)
            {
                Destroy(itemData.gameObject);
                viewArea.repositionNow = true;
            }
        }
        else if (amount == -1 || itemData.stackSize == amount)
        {
            Destroy(itemData.gameObject);
            viewArea.repositionNow = true;
        }
    }

    protected virtual void SetupWindow()
    {
        isActive = StartWindowActive;
        ContainerDisplayObject.SetActive(true);
        viewArea = ContainerDisplayObject.GetComponentInChildren<UIGrid>();  
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

    public void ShowWindow()
    {
        if (isActive) return;
        isActive = true;
        foreach (ContainerDisplayAction action in disaplyActions)
        {
            action.Activate();
        }
    }

    public void HideWindow()
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

}
