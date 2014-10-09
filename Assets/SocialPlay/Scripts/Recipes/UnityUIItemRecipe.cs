using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnityUIItemRecipe : MonoBehaviour, IItemRecipe {

    public Text recipeName;
    public RawImage recipeItemImage;

    public GameObject IngredientsGrid;
    public GameObject IngredientPrefab;

    RecipeInfo recipeInfo;

    public void LoadItemRecipe(RecipeInfo newRecipeInfo)
    {
        recipeName.text = newRecipeInfo.name;

        SP.GetItemTexture(newRecipeInfo.imgURL, OnReceivedRecipeImage);

        LoadIngredients(newRecipeInfo.IngredientDetails);
    }

    void LoadIngredients(List<IngredientDetail> itemIngredients)
    {
        foreach (IngredientDetail ingredient in itemIngredients)
        {
            GameObject ingredientObj = (GameObject)GameObject.Instantiate(IngredientPrefab);
            ingredientObj.transform.parent = IngredientsGrid.transform;

            RecipeIngredient recipeIngredient = ingredientObj.GetComponent<RecipeIngredient>();
            recipeIngredient.LoadIngredient(ingredient);
        }
    }

    void OnReceivedRecipeImage(ImageStatus imgStatus, Texture2D img)
    {
        recipeItemImage.texture = img;
    }
}
