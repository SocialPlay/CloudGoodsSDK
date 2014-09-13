﻿using UnityEngine;
using System.Collections;

public class LoadStoreOnRegisterExample : MonoBehaviour {

    public UnityUIDisplayItemBundles itemBundles;
    public DisplayStoreItems storeitems;

	// Use this for initialization
	void Awake () {
	    SP.OnRegisteredUserToSession += OnUserRegistered;
	}

    void OnUserRegistered(string userGuid)
    {
        itemBundles.GetItemBundles();
        storeitems.DisplayItems();
    }

}
