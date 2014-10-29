using UnityEngine;
using System.Collections;
using System;

public class TestConvertToGuid : MonoBehaviour {

    Guid testGuid = new Guid("73bcdbe5-48b8-4e8e-97bb-7fbb5b4b6155");

	// Use this for initialization
	void Start () {
		CloudGoods.MoveItemStack(Guid.Empty, 0, "", "", 0, OnGuidCallback);
	}

    void OnGuidCallback(Guid guid)
    {
        if (testGuid == guid)
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
