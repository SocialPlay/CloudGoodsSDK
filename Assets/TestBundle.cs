using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestBundle : MonoBehaviour {

    public ItemRecipeLoader itemRecipeLoader;

	void Start () {
        SP.OnRegisteredUserToSession += OnUserRegistered;
	}

    void OnUserRegistered(string data)
    {
        itemRecipeLoader.LoadItemRecipes();
    }

}
