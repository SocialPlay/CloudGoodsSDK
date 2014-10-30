using UnityEngine;
using System.Collections;
using System;

public class TestConvertToUserInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CloudGoods.GetUserFromWorld(CloudGoodsPlatform.SocialPlay, "", "", "", OnReceivedUserInfo);
	}
	
	// Update is called once per frame
    void OnReceivedUserInfo(CloudGoodsUser userInfo)
    {
        if (userInfo.userGuid == "c6afc667-bf54-4948-ad00-530b539f4122")
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
}
