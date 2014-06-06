using UnityEngine;
using System.Collections;
using System;

public class TestPlatformLogin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SP.LoginWithPlatformUser(SocialPlayPlatform.SocialPlay, "abcdefg", "LionelAndrew");
	}

}
