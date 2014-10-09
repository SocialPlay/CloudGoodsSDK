using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestBundle : MonoBehaviour {

    public ItemRecipeLoader itemRecipeLoader;

	// Use this for initialization
	void Start () {
        itemRecipeLoader.LoadItemRecipes();
	}

}
