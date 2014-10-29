using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ItemComponentSetup : MonoBehaviour
{
    public enum SetupByType
    {
        classID, tag, behaviour, varianceID,itemID
    }


    // public SetupByType selectorType = SetupByType.tag;

    public List<ComponentPair> itemComponents;
    public ItemFilterSystem filter = new ItemFilterSystem();

    [System.Serializable]
    public class ComponentPair
    {
        public MonoBehaviour component;
        public AddComponetTo destination;
    }

}

public enum AddComponetTo
{
    both, container, prefab
}