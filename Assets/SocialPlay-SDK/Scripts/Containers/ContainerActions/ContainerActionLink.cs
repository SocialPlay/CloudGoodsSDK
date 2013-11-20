using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;

public class ContainerActionLink : MonoBehaviour
{

    //public List<int> ClassIDList;
    //public List<string> TagList;
    public List<ContainerItemFilter> filters = new List<ContainerItemFilter>();


    public InputTypes ContainerActionInput;

    public ContainerActions containerAction;

    ItemContainer container;

    void Start()
    {
        container = GetComponent(typeof(ItemContainer)) as ItemContainer;
        SetEventSubscriptionForInput();
    }

    void SetEventSubscriptionForInput()
    {
        switch (ContainerActionInput)
        {
            case InputTypes.click:
                container.ItemSingleClicked += PerformActionWithItem;
                break;
            case InputTypes.doubleClick:
                container.ItemDoubleClicked += PerformActionWithItem;
                break;
            case InputTypes.rightClick:
                container.ItemRightClicked += PerformActionWithItem;
                break;
            case InputTypes.keyBindingPressed:
                container.ItemKeyBindingClicked += PerformActionWithItem;
                break;
        }
    }

    void PerformActionWithItem(ItemData itemData)
    {
        bool isfilteredOut = false;
        foreach (ContainerItemFilter filter in filters)
        {
            if (!filter.IsItemFilteredIn(itemData))
            {
                isfilteredOut = true;
                break;
            }
        }
        if (!isfilteredOut)
            containerAction.DoAction(itemData);

        container.FinishActionCycle();
    }
}

public enum InputTypes
{
    click,
    doubleClick,
    rightClick,
    keyBindingPressed
}
