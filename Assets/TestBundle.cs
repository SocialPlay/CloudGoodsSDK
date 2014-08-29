using UnityEngine;
using System.Collections;

public class TestBundle : MonoBehaviour {

    public GameObject testObj;

	// Use this for initialization
	void Start () {
        SP.OnUserInfo += blah;
	}

    void blah(SocialPlayUser hlash)
    {
        testObj.SetActive(true);
    }
}
