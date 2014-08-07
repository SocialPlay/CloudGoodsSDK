using UnityEngine;
using System.Collections;

public class DisplayStoreOnStart : MonoBehaviour {

    public DisplayStore displayStore;

    void Start()
    {
        displayStore.DisplayStoreItems();
    }
}
