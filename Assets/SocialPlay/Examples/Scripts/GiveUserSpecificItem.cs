using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveUserSpecificItem : MonoBehaviour {

    public int Amount = 0;
    public int ItemID = 0;
    public int Location = 0;

    public void GiveUserItem()
    {
        WebModels.ItemsInfo itemsInfo = new WebModels.ItemsInfo();
        itemsInfo.amount = Amount;
        itemsInfo.ItemID = ItemID;
        itemsInfo.location = Location;

        List<WebModels.ItemsInfo> items = new List<WebModels.ItemsInfo>();
        items.Add(itemsInfo);

        Debug.Log(CloudGoods.user.userGuid);

        CloudGoods.GiveOwnerItems(CloudGoods.user.userGuid, WebModels.OwnerTypes.User, items, OnReceivedUserItems);
    }

    void OnReceivedUserItems(string data)
    {
        Debug.Log(data);
    }
}
