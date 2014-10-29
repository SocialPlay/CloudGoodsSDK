using UnityEngine;
using System.Collections;

public class LoadStoreOnRegister : MonoBehaviour {

    public DisplayStoreItems displayStoreItems;

    void Start()
    {
        CloudGoods.OnRegisteredUserToSession += UserRegistered;
    }

    void UserRegistered(string userID)
    {
        displayStoreItems.DisplayItems();
    }
}
