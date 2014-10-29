using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RecipeWindowIngredient : MonoBehaviour {

    public RawImage ingredientImage;
    public Text ingredientAmount;


    public void LoadIngredient(IngredientDetail ingredientDetail, int containerAmountOfIngredient)
    {
        ingredientAmount.text = containerAmountOfIngredient+ " of " + ingredientDetail.amount.ToString();

        if (containerAmountOfIngredient < ingredientDetail.amount)
            ingredientAmount.color = Color.red;

        CloudGoods.GetItemTexture(ingredientDetail.imgURL, OnReceivedIngredientImage);
    }

    void OnReceivedIngredientImage(ImageStatus status, Texture2D texture)
    {
        ingredientImage.texture = texture;
    }
}
