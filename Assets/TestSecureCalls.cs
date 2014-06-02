using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSecureCalls : MonoBehaviour {

	// Use this for initialization
	void Start () {

        List<WebModels.ItemsInfo> listOfItems = new List<WebModels.ItemsInfo>();

        WebModels.ItemsInfo item = new WebModels.ItemsInfo();
        item.amount = 1;
        item.ItemID = 106465;
        item.location = 0;
        listOfItems.Add(item);

        WebserviceCalls.webservice.GiveOwnerItems("ef595214-369f-4313-9ac7-b0036e5ac25c", WebModels.OwnerTypes.User, listOfItems, OnReceivedToken); 
	}

    void OnReceivedToken(string token)
    {
        Debug.Log("Return from secure call: " + token);
    }

}
