using System;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SocialPlay.Generic
{
	public interface IGridLoader
	{
        event Action<CreditBundleItem, GameObject> ItemAdded;
        void LoadGrid(List<CreditBundleItem> data);
	}
}
