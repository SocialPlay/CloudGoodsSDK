using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ContainerActionLink : MonoBehaviour
{

    public InputTypes ContainerActionInput;

    public ContainerActions containerAction;

     ItemContainerDisplay container;

    void Start()
    {
        container = GetComponent<ItemContainerDisplay>();
        if (container == null)
        {
            container = GetComponentInChildren<ItemContainerDisplay>();
        }
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

    void PerformActionWithItem(ItemDataComponent itemData)
    {     
            containerAction.DoAction(itemData);
    }


}

public enum InputTypes
{
    click,
    doubleClick,
    rightClick,
    keyBindingPressed
}
