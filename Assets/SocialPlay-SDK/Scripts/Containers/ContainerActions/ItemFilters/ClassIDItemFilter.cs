using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassIDItemFilter : ContianerItemFilter 
{
    public List<int> classIDs;

    override public bool CheckFilter(ItemData item)
    {
        bool found = false;
        foreach (int classID in classIDs)
        {
            if (item.classID == classID)
            {
                found= true;
                break;
            }
        }
        if (type== InvertedState.excluded)
        {
            found = !found;
        }
        return found;
    }
}
