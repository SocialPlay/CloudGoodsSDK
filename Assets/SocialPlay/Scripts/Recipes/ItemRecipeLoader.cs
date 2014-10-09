using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemRecipeLoader : MonoBehaviour {

    public GameObject RecipePrefab;

    public GameObject RecipeGrid;

    public void LoadItemRecipes()
    {
        ItemRecipeCache.instance.GetRecipes(OnReceivedItemRecipes);
    }

    void OnReceivedItemRecipes(List<RecipeInfo> newRecipes)
    {
        foreach (RecipeInfo newRecipe in newRecipes)
        {
            GameObject newRecipeObj = (GameObject)GameObject.Instantiate(RecipePrefab);
            newRecipeObj.transform.parent = RecipeGrid.transform;

            UnityUIItemRecipe itemRecipe = newRecipeObj.GetComponent<UnityUIItemRecipe>();
            itemRecipe.LoadItemRecipe(newRecipe);
        }
    }
}
