﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassIDRestriction : MonoBehaviour, IContainerRestriction {

    public List<int> classIDList = new List<int>();
    public bool IsExcluded = false;

    ItemContainer restrictedContainer;

    void Awake()
    {
        restrictedContainer = GetComponent<ItemContainer>();
        restrictedContainer.containerAddRestrictions.Add(this);
    }

    public bool IsRestricted(ContainerAction action, ItemData itemData)
    {
        if (IsExcluded)
        {
            foreach (int classID in classIDList)
            {
                if (itemData.classID == classID)
                    return true;
            }

            return false;
        }
        else
        {
            foreach (int classID in classIDList)
            {
                if (itemData.classID == classID)
                    return false;
            }
            return true;
        }
    }
}
