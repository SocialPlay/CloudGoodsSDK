using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class OldItemStatsConverter : IItemStatsConverter
{
    public Dictionary<string, float> Generate(JArray statsDataObject)
    {
        Dictionary<string, float> statPair = new Dictionary<string, float>();
        //foreach (var info in statsDataObject)
        //{
        //    if (statsDataObject["CT"].ToString().Equals("NonUniqueItem"))
        //    {
        //        statPair.Add("Not Available", 0);
        //    }
        //    if (statsDataObject["CT"].ToString() == "Armour")
        //    {
        //        if (info.Key == "S")
        //            statPair.Add("Strength", float.Parse(info.Value.ToString()));
        //    }
        //    else
        //    {
        //        if (info.Key == "S")
        //            statPair.Add("Attack Rate", float.Parse(info.Value.ToString()) / 25);
        //    }

        //    if (info.Key == "D")
        //        statPair.Add("Defence", float.Parse(info.Value.ToString()));
        //    if (info.Key == "I")
        //        statPair.Add("Inteligence", float.Parse(info.Value.ToString()));
        //    if (info.Key == "P")
        //        statPair.Add("Attack Power", int.Parse(info.Value.ToString()));

        //    else if (info.Key == "CR")
        //        statPair.Add("Attack Range", float.Parse(info.Value.ToString()) / 10);
        //    else if (info.Key == "EA")
        //        statPair.Add("Effect Amount", float.Parse(info.Value.ToString()));
        //}
        return statPair;
    }
}
