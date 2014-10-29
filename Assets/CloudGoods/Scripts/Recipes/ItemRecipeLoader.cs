using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemRecipeLoader : MonoBehaviour {

    public GameObject RecipePrefab;

    public GameObject RecipeGrid;

    public RecipeDetailsWindow recipeDetailsWindow;

    public List<GameObject> currentItemRecipes = new List<GameObject>();


    public void LoadItemRecipes()
    {
        ItemRecipeCache.instance.GetRecipes(OnReceivedItemRecipes);
        CloudGoods.GetOwnerItems(CloudGoods.user.userGuid, "User", 0, OnReceivedOwnerItems);
    }

    void OnReceivedOwnerItems(List<ItemData> ownerItems)
    {
        recipeDetailsWindow.ownerItems = ownerItems;
    }

    void OnReceivedItemRecipes(List<RecipeInfo> newRecipes)
    {
        ClearCurrentItemRecipes();

        foreach (RecipeInfo newRecipe in newRecipes)
        {
            GameObject newRecipeObj = (GameObject)GameObject.Instantiate(RecipePrefab);
            newRecipeObj.transform.parent = RecipeGrid.transform;

            UnityUIItemRecipe itemRecipe = newRecipeObj.GetComponent<UnityUIItemRecipe>();
            itemRecipe.LoadItemRecipe(newRecipe);
            itemRecipe.recipeDetailsWindow = recipeDetailsWindow;

            currentItemRecipes.Add(newRecipeObj);
        }

        RecipeGrid.transform.position += new Vector3(0, -70, 0);
    }

    void ClearCurrentItemRecipes()
    {
        foreach (GameObject recipe in currentItemRecipes)
        {
            Destroy(recipe);
        }
        currentItemRecipes.Clear();
    }
}
