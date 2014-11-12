using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RecipeDetailsWindow : MonoBehaviour {

    public GameObject recipeInfoWindow;

    public Text RecipeItemName;
    public RawImage RecipeItemImage;

    public Text recipeItemDetails;

    public GameObject IngredientPrefab;
    public GameObject IngredientsGrid;

    public GameObject craftingButton;

    public List<ItemData> ownerItems;

    List<GameObject> currentIngredients = new List<GameObject>();

    public void OpenRecipeDetailsWindow(RecipeInfo recipeInfo)
    {
        bool HasEnoughResources = true;

        recipeInfoWindow.SetActive(true);

        RecipeItemName.text = recipeInfo.name;

        CloudGoods.GetItemTexture(recipeInfo.imgURL, OnReceivedRecipeItemImage);

        recipeItemDetails.text = "";

        foreach (ItemDetail itemDetail in recipeInfo.RecipeDetails)
        {
            recipeItemDetails.text += itemDetail.name + " : " + itemDetail.value + "\n";
        }

        ClearIngredients();

        foreach (IngredientDetail ingredient in recipeInfo.IngredientDetails)
        {
            GameObject ingredientObj = (GameObject)GameObject.Instantiate(IngredientPrefab);
            ingredientObj.transform.parent = IngredientsGrid.transform;

            HasEnoughResources = HasAvailableIngredientsForRecipe(ingredientObj, ingredient);
        }

        if (HasEnoughResources)
            craftingButton.SetActive(true);
        else
            craftingButton.SetActive(false);
    }

    bool HasAvailableIngredientsForRecipe(GameObject ingredientObj, IngredientDetail ingredient)
    {
        bool hasResoucres = true;
        int ingredientsAmount = 0;

        currentIngredients.Add(ingredientObj);

        RecipeWindowIngredient recipeIngredient = ingredientObj.GetComponent<RecipeWindowIngredient>();

        foreach (ItemData itemData in ownerItems)
        {
            Debug.Log("inventory id: " + itemData.CollectionID + " , ingredient id: " + ingredient.ingredientID);

            if (itemData.CollectionID == ingredient.ingredientID)
            {
                Debug.Log("Checking for ingredient : " + ingredient.name + ", Inventory amount: " + itemData.stackSize + " Required amount : " + ingredient.amount);

                if (itemData.stackSize < ingredient.amount)
                    hasResoucres = false;

                ingredientsAmount = itemData.stackSize;
            }
        }

        recipeIngredient.LoadIngredient(ingredient, ingredientsAmount);

        if (ingredientsAmount <= 0)
            hasResoucres = false;

        return hasResoucres;
    }

    void ClearIngredients()
    {
        foreach (GameObject ingredient in currentIngredients)
        {
            Destroy(ingredient);
        }

        currentIngredients.Clear();
    }

    public void CloseRecipeDetailsWindow()
    {
        recipeInfoWindow.SetActive(false);
    }

    void OnReceivedRecipeItemImage(ImageStatus status, Texture2D img)
    {
        RecipeItemImage.texture = img;
    }
}
