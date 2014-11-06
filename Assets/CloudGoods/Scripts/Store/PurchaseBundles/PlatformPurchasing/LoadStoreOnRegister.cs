using UnityEngine;
using System.Collections;

public class LoadStoreOnRegister : MonoBehaviour {

    public StoreInitializer displayStoreItems;

    void Start()
    {
        CloudGoods.OnRegisteredUserToSession += UserRegistered;
    }

    void UserRegistered(string userID)
    {
        displayStoreItems.InitializeStore();
    }
}
