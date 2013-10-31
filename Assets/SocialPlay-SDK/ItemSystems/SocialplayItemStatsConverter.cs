using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SocialplayItemStatsConverter : IItemStatsConverter
{
    public Dictionary<string, float> Generate(JArray statsDataObject)
    {
        Dictionary<string, float> statPair = new Dictionary<string, float>();

        for (int i = 0; i < statsDataObject.Count; i++)
        {
            statPair.Add(statsDataObject[i]["Name"].ToString(), float.Parse(statsDataObject[i]["Value"].ToString()));
        }

        return statPair;
    }
}

