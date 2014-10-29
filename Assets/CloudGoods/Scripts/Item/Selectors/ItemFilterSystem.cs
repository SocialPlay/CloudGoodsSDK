using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemFilterSystem
{

    public enum SetupByType
    {
        classID, tag, behaviour, varianceID, itemID
    }

    public SetupByType selectorType = SetupByType.tag;

    public bool isInverted = false;

    [HideInInspector]
    public List<int> classList = new List<int>();
    [HideInInspector]
    public List<int> varianceIDList = new List<int>();
    [HideInInspector]
    public List<int> itemIDList = new List<int>();
    [HideInInspector]
    public List<string> TagList = new List<string>();
    [HideInInspector]
    public List<string> behaviours = new List<string>();

    public bool IsSelected(ItemData item)
    {
        switch (selectorType)
        {
            case SetupByType.classID:
                return new ClassIDItemSelector().isItemSelected(item, classList, isInverted);
            case SetupByType.tag:
                return new TagItemSelector().isItemSelected(item, TagList, isInverted);
            case SetupByType.behaviour:
                return new BehaviourItemSelector().isItemSelected(item, behaviours, isInverted);
            case SetupByType.varianceID:
                return new varianceIDItemFilter().isItemSelected(item, varianceIDList, isInverted);
            case SetupByType.itemID:
                return new ItemIDItemFilter().isItemSelected(item, itemIDList, isInverted);
            default:
                return false;
        }
    }

    public class BehaviourPair
    {
        public int behaviourID;
        public int classID;

        public BehaviourPair()
        {
            behaviourID = 0;
            classID = 0;
        }
    }

}
