using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemQuailityColorSelector : MonoBehaviour
{

    public static ItemQuailityColorSelector selector;

    static Color[] defaultColors = { Color.gray, Color.white, Color.green, new Color(0.45f, 0.2f, 1), new Color(1, 0.6f, 0.2f) };
    public List<Color> qualityColors = new List<Color>(defaultColors);

    public List<ColorByTags> tagOverrides = new List<ColorByTags>();

    public void Awake()
    {
        if (selector == null)
        {
            selector = this;
        }
    }

    public static Color GetColorQuality(int colorQuality)
    {
        try
        {
            return selector.GetColor(colorQuality);
        }
        catch
        {
            return Color.white;
        }
    }



    public static Color GetColorForItem(ItemData item)
    {
        try
        {

            foreach (string tag in item.tags)
            {
                foreach (ColorByTags tagOverride in selector.tagOverrides)
                {
                    if (tagOverride.tags.Contains(tag))
                    {
                        return tagOverride.color;
                    }
                }
            }

            return selector.GetColor(item.quality);
        }
        catch 
        {
            return Color.white;
        }
    }


    protected virtual Color GetColor(int colorQuality)
    {
        if (colorQuality > qualityColors.Count)
        {
            return Color.white;
        }

        return qualityColors[colorQuality];

        //switch (colorQuality)
        //{
        //    case 1:
        //        return Color.gray;
        //    case 2:
        //        return Color.white;
        //    case 3:
        //        return Color.green;
        //    case 4:
        //        return new Color(0.45f, 0.2f, 1);
        //    case 5:
        //        return new Color(1, 0.6f, 0.2f);
        //    default:
        //        return Color.white;
        //}
    }


    [System.Serializable]
    public class ColorByTags
    {
        public string name;
        public List<string> tags = new List<string>();
        public Color color;
    }
}
