using UnityEngine;
using System.Collections;


public static class RichTextExtensions
{
    //public static string ColorRichText(this Color color, string str)
    //{
    //    Color32 tmp = color;
    //    string hex = tmp.r.ToString("X2") + tmp.g.ToString("X2") + tmp.b.ToString("X2");
    //    return "<color=#" + hex + ">" + str + "</color>";
    //}

    public static string ToRichColor(this string str, Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return "<color=#" + hex + ">" + str + "</color>";
    }

    public static string ToRichColor(this bool value)
    {
        return value.ToRichColor(Color.green, Color.red);
    }

    public static string ToRichColor(this bool value, Color32 trueColor, Color32 falseColor)
    {
        Color color = value ? trueColor : falseColor;
        return value.ToString().ToRichColor(color);
    }
}
