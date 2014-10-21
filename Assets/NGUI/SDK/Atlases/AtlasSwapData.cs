using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class AtlasSwapData : MonoBehaviour
{
    public UIAtlas mainAtlas = null;
    public UIAtlas subAtlas = null;

    public UIFont titleFont = null, swapTitleFont = null;
    public UIFont normalFont =null, swapNormalFont = null;
    public List<GameObject> allEffectedObjects = new List<GameObject>();
    public List<elementColorName> allColors = new List<elementColorName>();
    public List<elementColorName> coloredTags = new List<elementColorName>();

    public void ResetAll()
    {
        coloredTags.Clear();
        allColors.Clear();
        allEffectedObjects.Clear();
        subAtlas = null;
        mainAtlas = null;
        titleFont = null;
        normalFont = null;
        swapTitleFont = null;
        swapNormalFont = null;
    }

}

[System.Serializable]
public class elementColorName
{
    public string name;
    public bool isUsed = false;
    public Color color;
    public List<string> effectedItems = new List<string>();
    public bool isFoldedOut = false;


    public elementColorName(string na)
    {
        isUsed = false;
        name = na;
        color = Color.white;
        effectedItems = new List<string>();
        isFoldedOut = false;
    }

    public elementColorName(string na, Color ca)
    {
        isUsed = false;
        name = na;
        color = ca;
        effectedItems = new List<string>();
        isFoldedOut = false;
    }



    public override bool Equals(object obj)
    {
        if (obj.GetType() == typeof(elementColorName)) return this.name == (obj as elementColorName).name;
        if (obj.GetType() == typeof(string)) return this.name == (obj as string);
        return false;

    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
