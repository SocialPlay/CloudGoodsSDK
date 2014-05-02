using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        return selector.GetColor(colorQuality);
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


    public virtual Color GetColor(int colorQuality)
    {
        try
        {
            if (colorQuality > qualityColors.Count)
            {
                return Color.white;
            }

            return qualityColors[colorQuality];
        }
        catch
        {
            return Color.white;
        }
    }


    [System.Serializable]
    public class ColorByTags
    {
        public string name;
        public List<string> tags = new List<string>();
        public Color color;
    }
}
