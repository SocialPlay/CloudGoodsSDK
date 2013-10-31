using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class ContainerAddState
{
    public enum ActionState { Add, Swap, No }
    public ActionState actionState;
    public int possibleAddAmount;
    public ItemData possibleSwapItem;


    public ContainerAddState(ActionState newState = ActionState.No, int possibleAddAmount = 0, ItemData possibleSwapItem = null)
    {
        this.actionState = newState;
        this.possibleAddAmount = possibleAddAmount;
        this.possibleSwapItem = possibleSwapItem;
    }
}

