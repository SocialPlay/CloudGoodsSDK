using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomPropertyDrawer(typeof(ItemComponentSetup.ComponentPair))]
class IngredientDrawer : PropertyDrawer
{

    AddComponetTo selected;
    SerializedProperty selectedType;
    SerializedProperty componentToAdd;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUIUtility.LookLikeControls();
        // EditorGUI.BeginProperty(position, label, property);
        // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect SelectionSize = new Rect(position.x, position.y, 70, position.height);
        Rect componentSize = new Rect(position.x + SelectionSize.width, position.y, position.width - SelectionSize.width, position.height);

        selectedType = property.FindPropertyRelative("destination");
        selected = (AddComponetTo)selectedType.enumValueIndex;
        selected = (AddComponetTo)EditorGUI.EnumPopup(SelectionSize, selected);
        selectedType.enumValueIndex = (int)selected;

        //EditorGUIUtility.LookLikeInspector();
        componentToAdd = property.FindPropertyRelative("component");
        componentToAdd.objectReferenceValue = EditorGUI.ObjectField(componentSize, componentToAdd.objectReferenceValue, typeof(MonoBehaviour), true);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        //EditorGUI.EndProperty();
    }
}

