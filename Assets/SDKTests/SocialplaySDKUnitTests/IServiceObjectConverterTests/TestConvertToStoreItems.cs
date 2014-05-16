using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestConvertToStoreItems : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    WebserviceCalls.webservice.GetStoreItems("", OnReceivedStoreItems);
	}
	
	void OnReceivedStoreItems(List<StoreItemInfo> storeItemInfo)
    {
        if (storeItemInfo[0].ID == 409)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
