using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;
using Newtonsoft.Json;

public class NewtonsoftJsonObjectConverter : IServiceObjectConverter {

    public List<ItemData> ConvertToItemDataList(string itemDataString)
    {
        string data = "";
        ItemDataList itemDataList = null;

        /*if (itemDataString == "\"[]\"")
        {
            List<ItemData> convertedItems = new List<ItemData>();
        }*/

        data = JToken.Parse(itemDataString).ToString();

        itemDataList = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemDataList>(data);

        List<ItemData> items = SP.itemDataConverter.ConvertItems(itemDataList);

        return items;
    }

    public Guid ConvertToGuid(string dataString)
    {
        Debug.Log(dataString);
        Guid tmpGuid = new Guid(JToken.Parse(dataString).ToString());
        Debug.Log("test guid: " + tmpGuid.ToString());
        return tmpGuid;
    }

    public string ConvertToString(string dataString)
    {
        string convertedString = JToken.Parse(dataString).ToString();

        return convertedString;
    }

    public List<StoreItem> ConvertToStoreItems(string dataString)
    {
        List<StoreItem> storeItems = new List<StoreItem>();

        JToken token = JToken.Parse(dataString);

        JArray storeItemsJsonArray = JArray.Parse(token.ToString());


        for (int i = 0; i < storeItemsJsonArray.Count; i++)
        {
            StoreItem storeItemInfo = new StoreItem();
            storeItemInfo.ID = int.Parse(storeItemsJsonArray[i]["ID"].ToString());
            storeItemInfo.itemName = storeItemsJsonArray[i]["Name"].ToString();
            storeItemInfo.itemID = int.Parse(storeItemsJsonArray[i]["ItemID"].ToString());
            storeItemInfo.premiumCurrencyValue = int.Parse(storeItemsJsonArray[i]["CreditValue"].ToString());
            storeItemInfo.standardCurrencyValue = int.Parse(storeItemsJsonArray[i]["CoinValue"].ToString());

            JArray storeItemDetailArray = JArray.Parse(storeItemsJsonArray[i]["Detail"].ToString());

            List<StoreItemDetail> storeItemDetails = new List<StoreItemDetail>();

            for (int j = 0; j < storeItemDetailArray.Count; j++)
            {
                StoreItemDetail detail = new StoreItemDetail();
                detail.propertyName = storeItemDetailArray[j]["Name"].ToString();
                detail.propertyValue = int.Parse(storeItemDetailArray[j]["Value"].ToString());
                detail.invertEnergy = (bool)storeItemDetailArray[j]["InvertEnergy"];

                storeItemDetails.Add(detail);
            }

            storeItemInfo.itemDetail = storeItemDetails;

            //TODO implement addDate of store item
            //storeItemInfo.addedDate = (DateTime)storeItemsJsonArray[i]["AddedDate"];

            JArray tagsArray = JArray.Parse(storeItemsJsonArray[i]["tags"].ToString());
            for (int k = 0; k < tagsArray.Count; k++)
            {
                storeItemInfo.tags.Add(tagsArray[k].ToString());
            }

            storeItemInfo.imageURL = storeItemsJsonArray[i]["Image"].ToString();

            storeItems.Add(storeItemInfo);
        }

        return storeItems;
    }

    public SocialPlayUser ConvertToUserInfo(string ObjectData)
    {
        JToken token = JToken.Parse(ObjectData);
		SocialPlayUser userGuid = JsonConvert.DeserializeObject<SocialPlayUser>(token.ToString());

        return userGuid;
    }

    public List<RecipeInfo> ConvertToListRecipeInfo(string dataString)
    {
        List<RecipeInfo> recipeList = new List<RecipeInfo>();

        JToken token = JToken.Parse(dataString);

        JArray recipesArray = JArray.Parse(token.ToString());

        Debug.Log("Recipes count:" + recipesArray.Count);

        for (int i = 0; i < recipesArray.Count; i++)
        {
            RecipeInfo recipeInfo = new RecipeInfo();

            JToken recipeToken = recipesArray[i];

            recipeInfo.recipeID = int.Parse(recipeToken["ID"].ToString());
            recipeInfo.name = recipeToken["Name"].ToString();
            recipeInfo.energy = int.Parse(recipeToken["Energy"].ToString());
            recipeInfo.description = recipeToken["Description"].ToString();
            recipeInfo.imgURL = recipeToken["Image"].ToString();

            JArray ingredientsArray = (JArray)recipeToken["Recipe"];
            recipeInfo.IngredientDetails = new List<IngredientDetail>();

            for (int j = 0; j < ingredientsArray.Count; j++)
            {
                IngredientDetail ingredientDetail = new IngredientDetail();

                JToken ingredientToken = ingredientsArray[j];

                ingredientDetail.ingredientID = int.Parse(ingredientToken["ID"].ToString());
                ingredientDetail.amount = int.Parse(ingredientToken["Amount"].ToString());
                ingredientDetail.energy = int.Parse(ingredientToken["Energy"].ToString());
                ingredientDetail.imgURL = ingredientToken["Image"].ToString();
                ingredientDetail.name = ingredientToken["Name"].ToString();

                recipeInfo.IngredientDetails.Add(ingredientDetail);
            }

            JArray detailsArray = JArray.Parse(recipeToken["Detail"].ToString());
            recipeInfo.RecipeDetails = new List<ItemDetail>();

            for (int k = 0; k < detailsArray.Count; k++)
            {
                JToken detailToken = detailsArray[k];

                ItemDetail itemDetail = new ItemDetail();

                itemDetail.name = detailToken["Name"].ToString();
                itemDetail.value = float.Parse(detailToken["Value"].ToString());

                recipeInfo.RecipeDetails.Add(itemDetail);
            }

            recipeList.Add(recipeInfo);
        }

        return recipeList;
    }

    public List<ItemBundle> ConvertToListItemBundle(string dataString)
    {
        List<ItemBundle> ItemBundles = new List<ItemBundle>();

        JToken itemBundleParse = JToken.Parse(dataString);
        JArray itemBundleObj = JArray.Parse(itemBundleParse.ToString());

        for (int i = 0; i < itemBundleObj.Count; i++)
        {
            ItemBundle itemBundle = JsonConvert.DeserializeObject<ItemBundle>(itemBundleObj[i].ToString());

            JArray BundleItemArray = JArray.Parse(itemBundleObj[i]["items"].ToString());

            for (int j = 0; j < BundleItemArray.Count; j++)
            {
                BundleItem bundleItem = JsonConvert.DeserializeObject<BundleItem>(BundleItemArray[j].ToString());

                JArray bundleDetailsArray = JArray.Parse(BundleItemArray[j]["Detail"].ToString());

                for (int k = 0; k < bundleDetailsArray.Count; k++)
                {
                    BundleItemDetails bundleDetails = new BundleItemDetails();
                    bundleDetails.BundleDetailName = bundleDetailsArray[k]["Name"].ToString();
                    bundleDetails.Value = int.Parse(bundleDetailsArray[k]["Value"].ToString());

                    bundleItem.bundleItemDetails.Add(bundleDetails);
                }

                itemBundle.bundleItems.Add(bundleItem);
            }

            ItemBundles.Add(itemBundle);
        }

        return ItemBundles;
    }

    public List<PaidCurrencyBundleItem> ConvertToListPaidCurrencyBundleItem(string dataString)
    {
        List<PaidCurrencyBundleItem> PaidCurrencyBundles = new List<PaidCurrencyBundleItem>();

        JToken PaidCurrencyBundleParse = JToken.Parse(dataString);
        JArray PaidCurrencyBundleObj = JArray.Parse(PaidCurrencyBundleParse.ToString());

        for (int i = 0; i < PaidCurrencyBundleObj.Count; i++)
        {
            PaidCurrencyBundleItem PaidCurrencyBundle = new PaidCurrencyBundleItem();
            PaidCurrencyBundle.Amount = int.Parse(PaidCurrencyBundleObj[i]["CreditAmount"].ToString());
            PaidCurrencyBundle.Cost = PaidCurrencyBundleObj[i]["Cost"].ToString();
            PaidCurrencyBundle.CurrencyIcon = PaidCurrencyBundleObj[i].ToString();
            PaidCurrencyBundle.ID = int.Parse(PaidCurrencyBundleObj[i]["ID"].ToString());
            PaidCurrencyBundle.CurrencyName = "$";

            PaidCurrencyBundles.Add(PaidCurrencyBundle);
        }

        return PaidCurrencyBundles;
    }

    public MoveMultipleItemsResponse ConvertToMoveMultipleItemsResponse(string dataString)
    {
        JToken token = JToken.Parse(dataString);
        MoveMultipleItemsResponse infos = JsonConvert.DeserializeObject<MoveMultipleItemsResponse>(token.ToString());

        return infos;
    }

    public UserResponse ConvertToSPLoginResponse(string dataString)
    {
        JToken token = JToken.Parse(dataString);

        UserResponse responce = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResponse>(token.ToString());

		SocialPlayUser userinfo = Newtonsoft.Json.JsonConvert.DeserializeObject<SocialPlayUser>(responce.message);

        responce.userInfo = userinfo;

        return responce;
    }

    public WorldCurrencyInfo ConvertToWorldCurrencyInfo(string dataString)
    {
        return new WorldCurrencyInfo();
    }
}
