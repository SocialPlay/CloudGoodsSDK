using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[System.Serializable]
public class ItemContainerStackRestrictions : MonoBehaviour
{
    public int restrictionType;
    public int restrictionAmount;

    public virtual int GetRestrictionForType(int type)
    {
        if (restrictionType == type || restrictionType == -1)
        {
            return restrictionAmount;
        }
        return -1;
    }

}

