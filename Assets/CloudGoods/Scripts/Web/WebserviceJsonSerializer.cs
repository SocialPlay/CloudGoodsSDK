using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;

public class WebserviceJsonSerializer : MonoBehaviour {

    public string SercurePayloadSerializer(List<WebModels.ItemsInfo> items)
    {
        //StringBuilder sb = new StringBuilder();
        //JsonWriter writer = new JsonWriter(sb);

        SecurePayload payload = new SecurePayload();
        payload.Token = "new Token";
        payload.RequestObject = items;

        string jsonItems = JsonMapper.ToJson(payload);
        Debug.Log(jsonItems);

        return jsonItems;
    }

    public class SecurePayload
    {
        public string Token;
        public List<WebModels.ItemsInfo> RequestObject;
    }

}
