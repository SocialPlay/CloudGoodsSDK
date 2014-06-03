using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

public class ItemRecipeCache : MonoBehaviour {

    public static ItemRecipeCache instance;

    void Awake()
    {
        instance = this;
    }

    public List<RecipeInfo> CachedRecipes;

    public void GetRecipes(Action<List<RecipeInfo>> callback)
    {
        if (CachedRecipes != null)
        {
            callback(CachedRecipes);
        }
        else
        {
            SP.GetGameRecipes(ItemSystemGameData.AppID.ToString(), (x) =>
                    {
                        CachedRecipes = x;
                        callback(x);
                    }
                );
        }
    }
}

public class RecipeInfo
{
    public int recipeID;
    public string name;
    public int energy;
    public string description;
    public string imgURL;

    public List<ItemDetail> RecipeDetails;
    public List<IngredientDetail> IngredientDetails;
}

public class IngredientDetail
{
    public string name;
    public int ingredientID;
    public int amount;
    public int energy;
    public string imgURL;
}

public class ItemDetail
{
    public string name;
    public float value;
}
