using UnityEngine;
using System.Collections;
using System;
using LitJson;
using SocialPlay.Data;
using System.Collections.Generic;
using JsonFx.Json;

public class TestWebserviceJsonDeserialize : MonoBehaviour {

    public UILabel label;

	// Use this for initialization
	void Start () {
        WebserviceCalls.webservice.GetOwnerItems("1645c50a-538c-45b5-97f7-492b448f3c20", "User", 0, new Guid("2882f02f-4e91-4329-a051-0de9301b3e79"), ConvertJsonStringIntoData);

        //WebserviceCalls.webservice.GetStoreItems("2882f02f-4e91-4329-a051-0de9301b3e79", ConvertJsonStringIntoData);

        //WebserviceCalls.webservice.GenerateItemsAtLocation("1645c50a-538c-45b5-97f7-492b448f3c20", "User", 0, new Guid("2882f02f-4e91-4329-a051-0de9301b3e79"), 1, 1000, ConvertJsonStringIntoData, null, null);

        //WebserviceCalls.webservice.SPLogin_UserLogin(new Guid("2882f02f-4e91-4329-a051-0de9301b3e79"), "lionel.sy@gmail.com", "123456", ConvertJsonStringIntoData);
	}

    void ConvertJsonStringIntoData(string JsonString)
    {
        Debug.Log(JsonString);

        object jsonObj = JsonFx.Json.JsonReader.Deserialize(JsonString);

        Debug.Log(jsonObj);

        JsonData data = LitJson.JsonMapper.ToObject(jsonObj.ToString());

        for (int i = 0; i < data.Count; i++)
        {
            label.text += data[i]["Name"] + "\n";
        }
    }
}
