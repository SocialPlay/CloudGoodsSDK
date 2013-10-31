using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagItemSelector : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable tagList, bool IsInverted = false)
    {
        foreach (string tag in tagList)
        {
            if (item.tags.Contains(tag))
            {
                if (!IsInverted)
                    return true;
                else
                {
                    return false;
                }
            }
        }
        if (!IsInverted)
            return false;
        else
        {
            return true;
        }
    }
}
