using UnityEngine;
using System.Collections;

public class LoadStoreOnRegisterExample : MonoBehaviour {

    public StoreInitializer storeitems;

	// Use this for initialization
	void Awake () {
	    CloudGoods.OnRegisteredUserToSession += OnUserRegistered;
	}

    void OnUserRegistered(string userGuid)
    {
        storeitems.InitializeStore();
    }

}
