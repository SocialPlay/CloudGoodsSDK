using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public interface IItemStatsConverter
{
    Dictionary<string, float> Generate(JArray statsDataObject);
}