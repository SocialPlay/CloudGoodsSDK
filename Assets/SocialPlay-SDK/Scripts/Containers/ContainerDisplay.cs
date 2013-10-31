using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public abstract class ContainerDisplay : MonoBehaviour
{
    [HideInInspector]
    public bool isWindowActive = false;
    public GameObject containerDisplay;
    public ItemContainer ItemContainer = null;

    private List<ContainerDisplayAction> disaplyActions = new List<ContainerDisplayAction>();

    public virtual void SetupWindow()
    {
        isWindowActive = true;
        containerDisplay.SetActive(true);
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
        isWindowActive = true;
        foreach (ContainerDisplayAction action in disaplyActions)
        {
            action.Activate();
        }
    }

    public virtual void HideWindow()
    {
        foreach (ContainerDisplayAction action in disaplyActions)
        {
            action.Deactivate();
        }
        isWindowActive = false;
    }

    public abstract void AddDisplayItem(ItemData itemData, Transform parent);
    public abstract void RemoveDisplayItem(ItemData itemData);
}
