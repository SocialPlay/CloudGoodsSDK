using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TestConvertToItemDataList : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        CloudGoods.GenerateItemsAtLocation("", 0, 0, 0, OnReceivedGeneratedItems, "");
    }

    void OnReceivedGeneratedItems(List<ItemData> receivedItems)
    {
        if (receivedItems[0].itemName == "Steel")
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
