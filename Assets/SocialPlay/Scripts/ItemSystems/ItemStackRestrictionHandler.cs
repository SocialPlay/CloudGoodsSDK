using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public abstract class ItemStackRestrictionHandler
{
    protected List<ItemContainerStackRestrictions> restrictions = new List<ItemContainerStackRestrictions>();


    public virtual int GetRestrictedAmount(ItemData data,ItemContainer target)
    {
        restrictions = GetRestrictionsFor(target);
        foreach (ItemContainerStackRestrictions restriction in restrictions)
        {
            int restrictedAmount = restriction.GetRestrictionForType(data.classID);
            if (restrictedAmount!= -1)
            {
                return restrictedAmount;
            }             
        }
        return -1;
    }

    protected virtual List<ItemContainerStackRestrictions> GetRestrictionsFor(ItemContainer target)
    {
        return restrictions;
    }

}
