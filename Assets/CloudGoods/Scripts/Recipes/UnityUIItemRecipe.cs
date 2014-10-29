using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnityUIItemRecipe : MonoBehaviour, IItemRecipe {

    public Text recipeName;
    public RawImage recipeItemImage;

    public GameObject IngredientsGrid;
    public GameObject IngredientPrefab;

    public RecipeDetailsWindow recipeDetailsWindow;

    RecipeInfo recipeInfo;

    public void LoadItemRecipe(RecipeInfo newRecipeInfo)
    {
        recipeInfo = newRecipeInfo;

        recipeName.text = recipeInfo.name;

        CloudGoods.GetItemTexture(recipeInfo.imgURL, OnReceivedRecipeImage);

        LoadIngredients(recipeInfo.IngredientDetails);
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

    public void OnRecipeButtonClicked()
    {
        recipeDetailsWindow.OpenRecipeDetailsWindow(recipeInfo);
    }
}
