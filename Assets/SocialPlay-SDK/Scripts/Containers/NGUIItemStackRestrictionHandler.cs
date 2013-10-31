using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;

public class NGUIItemStackRestrictionHandler : ItemStackRestrictionHandler
{
    protected override List<ItemContainerStackRestrictions> GetRestrictionsFor(ItemContainer target)
    {
        List<ItemContainerStackRestrictions> restrictions = new List<ItemContainerStackRestrictions>();
        foreach (ItemContainerStackRestrictions res in target.GetComponentsInChildren<ItemContainerStackRestrictions>())
        {
            restrictions.Add(res);
        }
        return restrictions;
    }
}
