using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagItemFilter : ContianerItemFilter
{

    public List<string> tags = new List<string>();

    override public bool CheckFilter(ItemData item)
    {
        bool found = false;
        foreach (string tag in tags)
        {
            if (item.tags.Contains(tag))
            {
                found = true;
            }
        }
        if (type == InvertedState.excluded)
        {
            found = !found;
        }
        return found;
    }
}
