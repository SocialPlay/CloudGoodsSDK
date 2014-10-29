using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GivePlayerItemTest : MonoBehaviour
{


    public List<WebModels.ItemsInfo> itemsToGive = new List<WebModels.ItemsInfo>();
    public ContainerRefresh refresher;

    public void GivePlayerItemButton()
    {
        Debug.Log("Test");
        CloudGoods.GiveOwnerItems("", WebModels.OwnerTypes.User, itemsToGive, RecivedItem);
    }

    void RecivedItem(string msg)
    {
        if (msg == "\"Ok\"")
        {
            refresher.RefreshContianer();
        }
        else
        {
            Debug.LogError(msg);
        }
    }
}
