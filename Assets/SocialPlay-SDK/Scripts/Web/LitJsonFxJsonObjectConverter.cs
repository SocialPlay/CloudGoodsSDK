using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SocialPlay.Data;
using LitJson;
using JsonFx.Json;

public class LitJsonFxJsonObjectConverter : IServiceObjectConverter {

    public List<ItemData> ConvertToItemDataList(string ObjectData)
    {
        ItemDataList itemDataList = new SocialPlay.Data.ItemDataList();

        string parsedString = ParseString(ObjectData);

        Debug.Log(parsedString);

        ItemDataList itemsData = LitJson.JsonMapper.ToObject<ItemDataList>(ObjectData);

        //for (int i = 0; i < itemsData.Count; i++)
        //{
        //    SocialPlay.Data.ItemData itemData = new SocialPlay.Data.ItemData();
        //    itemData.StackLocationID = new Guid(itemsData[i]["StackLocationID"].ToString());
        //    itemData.Amount = int.Parse(itemsData[i]["Amount"].ToString());
        //    itemData.Detail = itemsData[i]["Detail"].ToString();
        //    itemData.ItemID = int.Parse(itemsData[i]["ItemID"].ToString());
        //    itemData.Type = int.Parse(itemsData[i]["Type"].ToString());
        //    itemData.Location = int.Parse(itemsData[i]["Location"].ToString());
        //    itemData.BaseItemEnergy = int.Parse(itemsData[i]["BaseItemEnergy"].ToString());
        //    itemData.Energy = int.Parse(itemsData[i]["Energy"].ToString());
        //    itemData.SellPrice = int.Parse(itemsData[i]["SellPrice"].ToString());
        //    itemData.Name = itemsData["Name"].ToString();
        //    itemData.Image = itemsData["Image"].ToString();
        //    itemData.Quality = int.Parse(itemsData[i]["Quality"].ToString());
        //    itemData.Behaviours = itemsData[i]["Behaviours"].ToString();
        //    itemData.BaseItemID = int.Parse(itemsData[i]["BaseItemID"].ToString());
        //    itemData.Description = itemsData[i]["Description"].ToString();
        //    itemData.AssetBundleName = itemsData[i]["AssetBundleName"].ToString();
        //    itemData.Tags = itemsData[i]["Tags"].ToString();

        //    itemDataList.Add(itemData);
        //}

        List<ItemData> items = ItemConversion.converter.ConvertItems(itemDataList);

        return items;
    }

    public Guid ConvertToGuid(string dataString)
    {
        string guidString = ParseString(dataString);
        Guid newGuid = new Guid(guidString);
        return newGuid;
    }

    public string ConvertToString(string dataString)
    {
        string newString = ParseString(dataString);
        return newString;
    }

    public List<StoreItemInfo> ConvertToStoreItems(string dataString)
    {
        string storeString = ParseString(dataString);

        List<StoreItemInfo> storeItems = new List<StoreItemInfo>();

        JsonData storeItemsJsonArray = LitJson.JsonMapper.ToObject(storeString);


        for (int i = 0; i < storeItemsJsonArray.Count; i++)
        {
            StoreItemInfo storeItemInfo = new StoreItemInfo();
            storeItemInfo.ID = int.Parse(storeItemsJsonArray[i]["ID"].ToString());
            storeItemInfo.itemName = storeItemsJsonArray[i]["Name"].ToString();
            storeItemInfo.itemID = int.Parse(storeItemsJsonArray[i]["ItemID"].ToString());
            storeItemInfo.creditValue = int.Parse(storeItemsJsonArray[i]["CreditValue"].ToString());
            storeItemInfo.coinValue = int.Parse(storeItemsJsonArray[i]["CoinValue"].ToString());

            JsonData storeItemDetailArray = LitJson.JsonMapper.ToObject(storeItemsJsonArray[i]["Detail"].ToString());

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

            //TODO add tag support
            //JsonData tagsArray = LitJson.JsonMapper.ToObject(storeItemsJsonArray[i]["tags"].ToString());
            //for (int k = 0; k < tagsArray.Count; k++)
            //{
            //    storeItemInfo.tags.Add(tagsArray[k].ToString());
            //}

            storeItemInfo.imageURL = storeItemsJsonArray[i]["Image"].ToString();

            storeItems.Add(storeItemInfo);
        }

        return storeItems;
    }

    public WebserviceCalls.UserInfo ConvertToUserInfo(string dataString)
    {
        string userInfoString = ParseString(dataString);

        JsonData data = LitJson.JsonMapper.ToObject(userInfoString);

        Debug.Log(data["userGuid"].ToString());

        WebserviceCalls.UserInfo userinfo = new WebserviceCalls.UserInfo(data["userGuid"].ToString(), data["userName"].ToString(), "");

        if (data["userEmail"] != null)
            userinfo.userEmail = data["userEmail"].ToString();

        return userinfo;
    }

    public List<RecipeInfo> ConvertToListRecipeInfo(string dataString)
    {
        string parsedString = ParseString(dataString);

        List<RecipeInfo> recipeList = new List<RecipeInfo>();

        JsonData recipesArray = LitJson.JsonMapper.ToObject(parsedString);


        for (int i = 0; i < recipesArray.Count; i++)
        {
            RecipeInfo recipeInfo = new RecipeInfo();

            JsonData recipeToken = recipesArray[i];

            recipeInfo.recipeID = int.Parse(recipeToken["ID"].ToString());
            recipeInfo.name = recipeToken["Name"].ToString();
            recipeInfo.energy = int.Parse(recipeToken["Energy"].ToString());
            recipeInfo.description = recipeToken["Description"].ToString();
            recipeInfo.imgURL = recipeToken["Image"].ToString();

            JsonData ingredientsArray = recipeToken["Recipe"];
            recipeInfo.IngredientDetails = new List<IngredientDetail>();

            for (int j = 0; j < ingredientsArray.Count; j++)
            {
                IngredientDetail ingredientDetail = new IngredientDetail();

                JsonData ingredientToken = ingredientsArray[j];

                ingredientDetail.ingredientID = int.Parse(ingredientToken["ID"].ToString());
                ingredientDetail.amount = int.Parse(ingredientToken["Amount"].ToString());
                ingredientDetail.energy = int.Parse(ingredientToken["Energy"].ToString());
                ingredientDetail.imgURL = ingredientToken["Image"].ToString();
                ingredientDetail.name = ingredientToken["Name"].ToString();

                recipeInfo.IngredientDetails.Add(ingredientDetail);
            }

            JsonData detailsArray = LitJson.JsonMapper.ToObject(recipeToken["Detail"].ToString());
            recipeInfo.RecipeDetails = new List<ItemDetail>();

            for (int k = 0; k < detailsArray.Count; k++)
            {
                JsonData detailToken = detailsArray[k];

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

        string parsedString = ParseString(dataString);

        JsonData itemBundleData = LitJson.JsonMapper.ToObject(parsedString);

        for (int i = 0; i < itemBundleData.Count; i++)
        {
            ItemBundle itemBundle = new ItemBundle();
            itemBundle.ID = int.Parse(itemBundleData[i]["ID"].ToString());
            itemBundle.Name = itemBundleData[i]["Name"].ToString();
            itemBundle.Description = itemBundleData[i]["Description"].ToString();
            itemBundle.CreditPrice = int.Parse(itemBundleData[i]["CreditPrice"].ToString());
            itemBundle.CoinPrice = int.Parse(itemBundleData[i]["CoinPrice"].ToString());
            itemBundle.State = int.Parse(itemBundleData[i]["State"].ToString());

            //TODO Implement itembundle behaviours

            JsonData BundleItemData = itemBundleData[i]["items"];

            for (int j = 0; j < BundleItemData.Count; j++)
            {
                BundleItem bundleItem = new BundleItem();
                bundleItem.Quantity = int.Parse(BundleItemData[j]["Quantity"].ToString());
                bundleItem.Name = BundleItemData[j]["Name"].ToString();
                bundleItem.Image = BundleItemData[j]["Image"].ToString();
                bundleItem.Description = BundleItemData[j]["Description"].ToString();
                bundleItem.Quality = int.Parse(BundleItemData[j]["Quality"].ToString());

                JsonData bundleItemDetailData = LitJson.JsonMapper.ToObject(BundleItemData[j]["Detail"].ToString());

                for (int k = 0; k < bundleItemDetailData.Count; k++)
                {
                    BundleItemDetails bundleDetails = new BundleItemDetails();
                    bundleDetails.BundleDetailName = bundleItemDetailData[k]["Name"].ToString();
                    bundleDetails.Value = int.Parse(bundleItemDetailData[k]["Value"].ToString());

                    bundleItem.bundleItemDetails.Add(bundleDetails);
                }

                itemBundle.bundleItems.Add(bundleItem);
            }

            ItemBundles.Add(itemBundle);
        }

        return ItemBundles;
    }

    public List<CreditBundleItem> ConvertToListCreditBundleItem(string dataString)
    {
        List<CreditBundleItem> creditBundles = new List<CreditBundleItem>();

        string parsedString = ParseString(dataString);

        JsonData creditBundleObj = LitJson.JsonMapper.ToObject(parsedString);

        for (int i = 0; i < creditBundleObj.Count; i++)
        {
            CreditBundleItem creditBundle = new CreditBundleItem();
            creditBundle.Amount = int.Parse(creditBundleObj[i]["CreditAmount"].ToString());
            creditBundle.Cost = creditBundleObj[i]["Cost"].ToString();
            creditBundle.CurrencyIcon = creditBundleObj[i].ToString();
            creditBundle.ID = int.Parse(creditBundleObj[i]["ID"].ToString());
            creditBundle.CurrencyName = "$";

            creditBundles.Add(creditBundle);
        }

        return creditBundles;
    }
    public MoveMultipleItemsResponse ConvertToMoveMultipleItemsResponse(string dataString)
    {
        MoveMultipleItemsResponse infos = LitJson.JsonMapper.ToObject<MoveMultipleItemsResponse>(dataString);

        return infos;
    }

    public SPLogin.SPLogin_Responce ConvertToSPLoginResponse(string dataString)
    {
        string parsedString = ParseString(dataString);

        JsonData data = LitJson.JsonMapper.ToObject(parsedString);

        JsonData userData = LitJson.JsonMapper.ToObject(data["message"].ToString());

        SPLogin.UserInfo userInfo = new SPLogin.UserInfo(new Guid(userData["ID"].ToString()), userData["name"].ToString(), userData["email"].ToString());

        SPLogin.SPLogin_Responce responce = new SPLogin.SPLogin_Responce(int.Parse(data["code"].ToString()), data["message"].ToString(), userInfo);

        return responce;
    }

    string ParseString(string dataString)
    {
        string parseString = dataString.Remove(0, 1);
        parseString = parseString.Remove(parseString.Length - 1, 1);

        parseString = parseString.Replace("\\\"", "\"");
        parseString = parseString.Replace("\\\\", "\\");


        return parseString;
    }

}
