using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class resolutionSize
{
    public int height;
    public int width;
    public Vector3 scaleChange;
}


public class UIResolutionScale : MonoBehaviour
{

    public resolutionSize[] resolutions;


    // Use this for initialization
    void Start()
    {

        foreach (resolutionSize item in resolutions)
        {
            if (Screen.width <= item.width && Screen.height <= item.height)
            {
                transform.localScale = item.scaleChange;
                transform.localScale = item.scaleChange;
            }
        }
    }
}
