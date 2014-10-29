using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestConvertToListRecipeInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CloudGoods.GetGameRecipes(OnReceivedRecipes);
	}

    void OnReceivedRecipes(List<RecipeInfo> recipes)
    {
        if (recipes[0].name == "Pistol")
            IntegrationTest.Pass(gameObject);
        else
            IntegrationTest.Fail(gameObject);
    }
	
}
