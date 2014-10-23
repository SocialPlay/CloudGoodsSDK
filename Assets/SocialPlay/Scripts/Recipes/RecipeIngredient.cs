using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecipeIngredient : MonoBehaviour {

    public RawImage ingredientImage;
    public Text ingredientAmount;


    public void LoadIngredient(IngredientDetail ingredientDetail)
    {
        ingredientAmount.text = ingredientDetail.amount.ToString();

        CloudGoods.GetItemTexture(ingredientDetail.imgURL, OnReceivedIngredientImage);
    }

    void OnReceivedIngredientImage(ImageStatus status, Texture2D texture)
    {
        ingredientImage.texture = texture;
    }
}
