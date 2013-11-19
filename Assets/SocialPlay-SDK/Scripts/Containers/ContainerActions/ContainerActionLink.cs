using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocialPlay.ItemSystems;

public class ContainerActionLink : MonoBehaviour
{

    //public List<int> ClassIDList;
    //public List<string> TagList;
    public List<ContianerItemFilter> Filters = new List<ContianerItemFilter>();


    public InputTypes ContainerActionInput;

    public ContainerActions ContainerAction;

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
        foreach (ContianerItemFilter filter in Filters)
        {
            if (!filter.CheckFilter(itemData))
            {
                return;
            }
        }
        ContainerAction.DoAction(itemData);
    }
}

public enum InputTypes
{
    click,
    doubleClick,
    rightClick,
    keyBindingPressed
}
