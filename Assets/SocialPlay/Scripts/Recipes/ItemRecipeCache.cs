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
            CloudGoods.GetGameRecipes((x) =>
                {
                    CachedRecipes = x;
                    callback(x);
                }
            );
        }
    }
}