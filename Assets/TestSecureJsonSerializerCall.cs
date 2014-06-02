using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WebModels;

public class TestSecureJsonSerializerCall : MonoBehaviour {

    public WebserviceJsonSerializer seriliazer;

	// Use this for initialization
	void Start () {
        List<ItemsInfo> items = new List<ItemsInfo>();

        for(int i = 0; i < 3; i++)
        {
            ItemsInfo item = new ItemsInfo();
            item.ItemID = 100;
            item.location = 0;
            item.amount = 1;

            items.Add(item);
        }

        seriliazer.SercurePayloadSerializer(items);
	}

}
