using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class ItemContainerManager
{
    public static ContainerAddState.ActionState MoveItem(ItemData movingItemData, ItemContainer lastContainer, ItemContainer targetContainer)
    {
        try
        {
            if (movingItemData == null)
                throw new Exception("Can Not Move null item");

            if (targetContainer == null)
                throw new Exception("Can not move item to null container");

            if (lastContainer != null)
            {
                Debug.Log(lastContainer.GetContainerRemoveState(movingItemData));
                if (lastContainer.GetContainerRemoveState(movingItemData) == false)
                {
                    return ContainerAddState.ActionState.No;
                }
            }

            ContainerAddState targetAddState = targetContainer.GetContainerAddState(movingItemData);

            switch (targetAddState.actionState)
            {
                case ContainerAddState.ActionState.Add:

                    int StackSize = movingItemData.stackSize;

                    Debug.Log("New item data befor remove: " + movingItemData.stackSize);

                    if (movingItemData.ownerContainer != null)
                    {
                        movingItemData.ownerContainer.Remove(movingItemData, true, targetAddState.possibleAddAmount);
                    }

                    targetContainer.Add(movingItemData, targetAddState.possibleAddAmount);

                    break;
                case ContainerAddState.ActionState.No:
                    break;
                default:
                    break;
            }

            return targetAddState.actionState;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);

            return ContainerAddState.ActionState.No;
        }
    }
}

