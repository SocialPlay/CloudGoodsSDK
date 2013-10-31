using UnityEngine;
using System;
using Newtonsoft.Json.Linq;



class NGUIGridLoader : MonoBehaviour, SocialPlay.Generic.IGridLoader
{
    public GameObject grid = null;
    public GameObject itemPrefab = null;

    public event Action<JObject, GameObject> ItemAdded;

    public void LoadGrid(string data)
    {
        JArray array = JArray.Parse(JToken.Parse(data).ToString());

        foreach (JObject item in array)
        {
            GameObject gItem = NGUITools.AddChild(grid, itemPrefab);
            if (ItemAdded != null)
                ItemAdded(item, gItem);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }
}

