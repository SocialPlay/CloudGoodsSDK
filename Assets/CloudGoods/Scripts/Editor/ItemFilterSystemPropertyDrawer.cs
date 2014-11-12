using UnityEngine;
using System.Collections;
using UnityEditor;
using Newtonsoft.Json;

[CustomPropertyDrawer(typeof(ItemFilterSystem))]
public class ItemFilterSystemPropertyDrawer : PropertyDrawer
{

    GUIContent invertedInfo = new GUIContent("Do Not Allow", "If Set only items NOT in the filter will pass");

    ItemFilterSystem.SetupByType selected;
    SerializedProperty selectedType;
    SerializedProperty isInverted;
    SerializedProperty behaviours;
    SerializedProperty tags;
    SerializedProperty classID;
    SerializedProperty ItemID;
    SerializedProperty CollectionID;
    //SerializedProperty components;

    Rect displaySize = new Rect(0, 0, 350, 16 * 4);


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return displaySize.height;   
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        selectedType = property.FindPropertyRelative("selectorType");
        isInverted = property.FindPropertyRelative("isInverted");
        behaviours = property.FindPropertyRelative("behaviours");
        tags = property.FindPropertyRelative("TagList");
        classID = property.FindPropertyRelative("classList");
        ItemID = property.FindPropertyRelative("ItemIDList");
        CollectionID = property.FindPropertyRelative("CollectionIDList"); 

        Rect invertedRect = new Rect(position.x, position.y, position.width - 80, 16);
        Rect selectionRect = new Rect(position.x, invertedRect.yMax, position.width, 16);
        Rect SecondBitRect = new Rect(position.x, selectionRect.yMax, position.width, position.height - 32);
        EditorGUIUtility.LookLikeControls();
        selected = (ItemFilterSystem.SetupByType)selectedType.enumValueIndex;
        selected = (ItemFilterSystem.SetupByType)EditorGUI.EnumPopup(selectionRect, selected);
        selectedType.enumValueIndex = (int)selected;

        isInverted.boolValue = GUI.Toggle(invertedRect, isInverted.boolValue, invertedInfo);

        var ExtraRows = 0;
        switch (selected)
        {
            case ItemFilterSystem.SetupByType.classID:
                DrawIntList(SecondBitRect, classID, "Class");
                ExtraRows = 1 + classID.arraySize;
                break;
            case ItemFilterSystem.SetupByType.tag:
                DrawTag(SecondBitRect);
                ExtraRows = 1 + tags.arraySize;
                break;
            case ItemFilterSystem.SetupByType.behaviour:
                DrawBehaviourID(SecondBitRect);
                ExtraRows = 1 + behaviours.arraySize;
                break;
            case ItemFilterSystem.SetupByType.itemID:
                DrawIntList(SecondBitRect, ItemID,"Item");
                ExtraRows = 1 + ItemID.arraySize;
                break;
            case ItemFilterSystem.SetupByType.collectionID:
                DrawIntList(SecondBitRect, CollectionID, "Collection");
                ExtraRows = 1 + CollectionID.arraySize;
                break;
            default:
                break;
        }
        displaySize.height = 16 * (ExtraRows + 3);
    }


    void DrawIntList(Rect displaySize, SerializedProperty intList, string type)
    {
        EditorGUIUtility.LookLikeControls();
        var currentY = displaySize.y;

        for (int i = 0; i < intList.arraySize; i++)
        {
            Rect lineDisplayRect = new Rect(displaySize.x + displaySize.width - 130, currentY, 100, 16);
            Rect removeButtonRect = new Rect(lineDisplayRect.x + lineDisplayRect.width, currentY, 30, 16);

            int validateFirst = EditorGUI.IntField(lineDisplayRect, intList.GetArrayElementAtIndex(i).intValue);
            if (validateFirst > 1)
            {
                intList.GetArrayElementAtIndex(i).intValue = validateFirst;
            }
            if (GUI.Button(removeButtonRect, "-"))
            {
                intList.DeleteArrayElementAtIndex(i);
                return;
            }
            currentY += 16;
        }
        Rect AddButtonRect = new Rect(displaySize.x, currentY, displaySize.width, 16);

        if (GUI.Button(AddButtonRect, "+ Add New " + type))
        {
            intList.InsertArrayElementAtIndex(intList.arraySize);
        }
    }

    //void DrawClassID(Rect displaySize)
    //{
    //    EditorGUIUtility.LookLikeControls();
    //    var currentY = displaySize.y;

    //    for (int i = 0; i < classID.arraySize; i++)
    //    {
    //        Rect ClassIDRect = new Rect(displaySize.x + displaySize.width - 130, currentY, 100, 16);
    //        Rect RemoveButtonRect = new Rect(ClassIDRect.x + ClassIDRect.width, currentY, 30, 16);

    //        int validateFirst = EditorGUI.IntField(ClassIDRect, classID.GetArrayElementAtIndex(i).intValue);
    //        if (validateFirst > 1)
    //        {
    //            classID.GetArrayElementAtIndex(i).intValue = validateFirst;
    //        }
    //        if (GUI.Button(RemoveButtonRect, "-"))
    //        {
    //            classID.DeleteArrayElementAtIndex(i);
    //            return;
    //        }
    //        currentY += 16;
    //    }
    //    Rect AddButtonRect = new Rect(displaySize.x, currentY, displaySize.width, 16);

    //    if (GUI.Button(AddButtonRect, "+ Add New Class"))
    //    {
    //        classID.InsertArrayElementAtIndex(classID.arraySize);
    //    }
    //}

    void DrawTag(Rect displaySize)
    {
        var currentY = displaySize.y;
        for (int i = 0; i < tags.arraySize; i++)
        {
            Rect tagIDRect = new Rect(displaySize.x, currentY, displaySize.width - 30, 16);
            Rect RemoveButtonRect = new Rect(tagIDRect.x + tagIDRect.width, currentY, 30, 16);
            string validateFirst = EditorGUI.TextField(tagIDRect, tags.GetArrayElementAtIndex(i).stringValue);
            if (!string.IsNullOrEmpty(validateFirst))
            {
                tags.GetArrayElementAtIndex(i).stringValue = validateFirst;
            }
            if (GUI.Button(RemoveButtonRect, "-"))
            {
                tags.DeleteArrayElementAtIndex(i);

                return;
            }
            currentY += 16;
        }
        Rect AddButtonRect = new Rect(displaySize.x, currentY, displaySize.width, 16);
        if (GUI.Button(AddButtonRect, "+ Add New Tag"))
        {
            tags.InsertArrayElementAtIndex(tags.arraySize);

        }
    }

    void DrawBehaviourID(Rect displaySize)
    {
        var currentY = displaySize.y;
        for (int i = 0; i < behaviours.arraySize; i++)
        {
            Rect RemoveButtonRect = new Rect(displaySize.x + displaySize.width - 30, currentY, 30, 16);
            Rect behaviourIDRect = new Rect(RemoveButtonRect.x - 60, currentY, 60, 16);
            Rect behaviourLabelRect = new Rect(behaviourIDRect.x - 65, currentY, 65, 16);
            Rect ClassIDRect = new Rect(behaviourLabelRect.x - 60, currentY, 60, 16);
            Rect ClassLabelRect = new Rect(ClassIDRect.x - 35, currentY, 35, 16);



            string behaviourPrefromat = behaviours.GetArrayElementAtIndex(i).stringValue;
            ItemFilterSystem.BehaviourPair pair = JsonConvert.DeserializeObject<ItemFilterSystem.BehaviourPair>(behaviourPrefromat);
            if (pair == null)
            {
                pair = new ItemFilterSystem.BehaviourPair();
            }

            int behaviourClass = EditorGUI.IntField(ClassIDRect, pair.classID);
            GUI.Label(behaviourLabelRect, "Behaviour");
            int behaviourID = EditorGUI.IntField(behaviourIDRect, pair.behaviourID);
            GUI.Label(ClassLabelRect, "Class");
            if (GUI.Button(RemoveButtonRect, "-"))
            {
                behaviours.DeleteArrayElementAtIndex(i);
                return;
            }
            if (behaviourClass >= 0)
            {
                pair.classID = behaviourClass;
            }
            if (behaviourID >= 0)
            {
                pair.behaviourID = behaviourID;
            }
            behaviours.GetArrayElementAtIndex(i).stringValue = JsonConvert.SerializeObject(pair);
            currentY += 16;
        }
        Rect AddButtonRect = new Rect(displaySize.x, currentY, displaySize.width, 16);
        if (GUI.Button(AddButtonRect, "+ Add New Behaviour"))
        {
            behaviours.InsertArrayElementAtIndex(behaviours.arraySize);
        }


    }

}
