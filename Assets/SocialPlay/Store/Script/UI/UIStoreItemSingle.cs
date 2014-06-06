using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class UIStoreItemSingle : UIStoreItem
{
	public UILabel nameLabel;
	public UILabel descriptionLabel;

	public override void SetItemData(StoreItem item)
	{
		base.SetItemData(item);

		nameLabel.text = item.itemName;

		for (int i = 0, imax = item.itemDetail.Count; i < imax; i++)
		{
			Debug.Log("itemDetails " + item.itemDetail[i].propertyName+" / "+item.itemDetail[i].propertyValue);
		}
			
		//descriptionLabel.text = item.;
	}
}
