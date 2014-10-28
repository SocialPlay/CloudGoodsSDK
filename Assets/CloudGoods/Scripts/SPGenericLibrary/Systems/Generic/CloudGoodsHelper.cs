using UnityEngine;
using System.Collections;

public class CloudGoodsHelper
{
    public static void SetLayerRecursively(GameObject obj, int layer)
    {
        foreach (Transform T in obj.GetComponentsInChildren<Transform>())
        {
            T.gameObject.layer = layer;
        }
    }

    public static string FormatStringToLength(string original, int length = 45, string newLineChar = "\n")
    {
        string formated = string.Empty;
        string[] words = original.Split(' ');
        int CurrentLineLength = 0;
        foreach (string word in words)
        {
            if (CurrentLineLength + word.Length >= length)//Starts a new line.
            {
                formated = formated.Insert(formated.Length, "\n");
                CurrentLineLength = 0;
            }
            else
            {
                if (formated.Length != 0)
                {
                    formated = formated.Insert(formated.Length, " ");
                    CurrentLineLength++;
                }
            }
            CurrentLineLength += word.Length;
            formated = formated.Insert(formated.Length, word);
        }

        return formated;
    }

    public static string SetTextColor(Color color)
    {
        //return string.Format("[{0}]", NGUIText.EncodeColor(color));
        return "";
    }

    public static string SetTextColor(string passedString, Color color)
    {
        //return string.Format("[{0}]{1}[-]", NGUIText.EncodeColor(color), passedString);
        return "";
    }

    /// <summary>
    /// Returns a random point on a perimeter. any axis that is set to 0 will be set to zero.
    /// </summary>
    /// <param Email="size">the size of the circle to use</param>
    /// <param Email="shape">sets the shape of the circle (0 sets thats axis to not used)</param>
    /// <returns>random point on outside of a parameter</returns>
    public static Vector3 GetRandomPointOnParameter(float size, Vector3 shape)
    {
        return GetRandomPointOnParameter(size, shape, Vector3.zero);
    }


    /// <summary>
    /// Returns a random point on a perimeter. any axis that is set to 0 will be set to zero.
    /// </summary>
    /// <param Email="size">the size of the circle to use</param>
    /// <param Email="shape">sets the shape of the circle (0 sets thats axis to not used)</param>
    /// <param Email="origin">the location to center the point on</param>
    /// <returns>random point on outside of a parameter</returns>
    public static Vector3 GetRandomPointOnParameter(float size, Vector3 shape, Vector3 origin)
    {
        Vector3 point = (Random.onUnitSphere * size);
        point.x *= shape.x;
        point.y *= shape.y;
        point.z *= shape.z;
        return point + origin;
    }


    /// <summary>   
    /// Gets a rtandom point inside a Sphere based on a aset shape.
    /// </summary>
    /// <param Email="size">the size of the sphere</param>
    /// <param Email="shape">the shape of the Sphere (zero will flatten the sphere to a axis)</param>
    /// <returns>random point inside a sphere</returns>
    public static Vector3 GetRandomPointInSphere(float size, Vector3 shape)
    {
        return GetRandomPointInSphere(size, shape, Vector3.zero);
    }


    /// <summary>   
    /// Gets a rtandom point inside a Sphere based on a aset shape.
    /// </summary>
    /// <param Email="size">the size of the sphere</param>
    /// <param Email="shape">the shape of the Sphere (zero will flatten the sphere to a axis)</param>
    /// <param Email="origin">the center of the random point</param>
    /// <returns>random point inside a sphere</returns>
    public static Vector3 GetRandomPointInSphere(float size, Vector3 shape, Vector3 origin)
    {
        Vector3 point = (Random.insideUnitSphere * size);
        point.x *= shape.x;
        point.y *= shape.y;
        point.z *= shape.z;
        return point + origin;
    }


}

public static class MyExtensions
{
    public static T GetIComponent<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponent(typeof(T)) as T;
    }

    public static T GetIComponentInChildren<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponentInChildren(typeof(T)) as T;
    }
}
