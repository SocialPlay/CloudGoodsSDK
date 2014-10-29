using System;

using UnityEngine;

public class ItemContainerRestrictor : MonoBehaviour
{
    public enum RestrictorState
    {
        Normal,
        AddOnly,
        RemoveOnly,
        NoAction
    }

    public RestrictorState restrictorState = RestrictorState.Normal;

    public ItemContainer restrictedContainer;

    void Awake()
    {
        CheckForValidRestrictedContainer();
    }

    public void CheckForValidRestrictedContainer()
    {
        if (!restrictedContainer)
            throw new Exception("ItemContainerRestrictor could not find a container to restrict.");
    }


    public bool IsRestricted(ContainerAction action)
    {
        switch (restrictorState)
        {
            case RestrictorState.Normal:
                return false;                
            case RestrictorState.AddOnly:
                if (action == ContainerAction.add)
                {
                    return false;
                }
                return true;
            case RestrictorState.RemoveOnly:
                if (action == ContainerAction.remove)
                {
                    return false;
                }
                return true;
            case RestrictorState.NoAction:
                return true;           
        }
        return true;
    }
}