using UnityEngine;
using System.Collections;

public class TrashCanContainer : ItemContainer
{
    protected override ContainerAddState MyContainerAddState(ItemData modified)
    {
        return new ContainerAddState(ContainerAddState.ActionState.Add, modified.stackSize);
    }


    protected override void AddItem(ItemData modified, int amount = -1, bool isSave = true)
    {
        modified.ownerContainer.Remove(modified, false, modified.stackSize);
        WebserviceCalls.webservice.DeductStackAmount(modified.stackID, modified.stackSize, ReturnedString);
        Destroy(modified.gameObject);

    }

    protected override void RemoveItem(ItemData modified, bool isMoving, int amount = -1)
    {
        //Nothing to remove.
    }

    public override int Contains(ItemData modified)
    {
        return 0;
    }

    public override void Clear()
    {
        ///Nothing to clear.
    }

    void ReturnedString(string msg)
    {
    }
}
