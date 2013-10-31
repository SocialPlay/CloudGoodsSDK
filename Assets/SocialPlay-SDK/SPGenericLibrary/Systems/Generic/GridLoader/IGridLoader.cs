using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace SocialPlay.Generic
{
	public interface IGridLoader
	{
        event Action<JObject, GameObject> ItemAdded;
        void LoadGrid(string data);
	}
}
