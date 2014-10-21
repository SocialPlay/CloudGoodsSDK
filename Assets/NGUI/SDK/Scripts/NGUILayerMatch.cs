using UnityEngine;
using System.Collections;


public class NGUILayerMatch : MonoBehaviour
{
    public int layer;
    void Awake()
    {
        NGUITools.SetLayer(this.gameObject, layer);
    }
}
