using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SocialPlay.Data;
using LitJson;

public class LitJsonFxJsonObjectConverter : IServiceObjectConverter {

    public List<ItemData> ConvertToItemDataList(string ObjectData)
    {
        ItemDataList itemDataList = new SocialPlay.Data.ItemDataList();

        string parsedString = ParseString(ObjectData);

        if (parsedString == "[]")
            return new List<ItemData>();

        JsonReader reader = new JsonReader(parsedString);
        Debug.Log(parsedString);
        reader.Read();

        if (reader.Token.ToString() == "ArrayStart")
        {
            while (reader.Token.ToString() != "ArrayEnd")
            {
                reader.Read();

                if (reader.Token.ToString() == "ObjectStart")
                {
                    SocialPlay.Data.ItemData itemData = new SocialPlay.Data.ItemData();

                    while (reader.Token.ToString() != "ObjectEnd")
                    {
                        reader.Read();

                        if (reader.Token.ToString() == "PropertyName")
                        {
                            Debug.Log(reader.Value.ToString());
                            string propertyString = reader.Value.ToString();

                            reader.Read();

                            if (propertyString == "StackLocationID")
                            {
                                itemData.StackLocationID = new Guid(reader.Value.ToString());
                                Debug.Log(itemData.StackLocationID.ToString());
                            }
                            if (propertyString == "Amount")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.Amount = tmpInt;
                            }
                            if (propertyString == "Detail")
                            {
                                itemData.Detail = reader.Value.ToString();
                            }
                            if (propertyString == "ItemID")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.ItemID = tmpInt;
                            }
                            if (propertyString == "Type")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.Type = tmpInt;
                            }
                            if (propertyString == "Location")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.Location = tmpInt;
                            }
                            if (propertyString == "BaseItemEnergy")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.BaseItemEnergy = tmpInt;
                            }
                            if (propertyString == "Energy")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.Energy = tmpInt;
                            }
                            if (propertyString == "SellPrice")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.SellPrice = tmpInt;
                            }
                            if (propertyString == "Name")
                            {
                                itemData.Name = reader.Value.ToString();
                            }
                            if (propertyString == "Image")
                            {
                                itemData.Image = reader.Value.ToString();
                            }
                            if (propertyString == "Quality")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.Quality = tmpInt;
                            }
                            if (propertyString == "Behaviours")
                            {
                                itemData.Behaviours = reader.Value.ToString();
                            }
                            if (propertyString == "BaseItemID")
                            {
                                int tmpInt = 0;
                                int.TryParse(reader.Value.ToString(), out tmpInt);

                                itemData.BaseItemID = tmpInt;
                            }
                            if (propertyString == "Description")
                            {
                                itemData.Description = reader.Value.ToString();
                            }
                            if (propertyString == "AssetBundleName")
                            {
                                itemData.AssetBundleName = reader.Value.ToString();
                            }
                            if (propertyString == "Tags")
                            {
                                itemData.Tags = reader.Value.ToString();
                            }
                        }
                    }

                    itemDataList.Add(itemData);
                }
            }
        }

        List<ItemData> items = ItemConversion.converter.ConvertItems(itemDataList);

        return items;
    }


    public Guid ConvertToGuid(string dataString)
    {
        string guidString = ParseString(dataString);
        Debug.Log(guidString);
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

    public SP.UserInfo ConvertToUserInfo(string dataString)
    {
        Debug.Log(dataString);

        string userInfoString = ParseString(dataString);

        JsonData data = LitJson.JsonMapper.ToObject(userInfoString);

        Debug.Log(data["userGuid"].ToString());

        SP.UserInfo userinfo = new SP.UserInfo(data["userGuid"].ToString(), data["userName"].ToString(), "");

        if (data["userEmail"] != null) userinfo.userEmail = data["userEmail"].ToString();

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
            itemBundle.State = (SPBundle)Enum.Parse(typeof(SPBundle), itemBundleData[i]["State"].ToString());

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

    public SP.UserResponse ConvertToSPLoginResponse(string dataString)
    {
        string parsedString = ParseString(dataString);

        Debug.Log(parsedString);

        JsonData data = LitJson.JsonMapper.ToObject(parsedString);

		SP.UserInfo userInfo = null;

		if (data["code"].ToString() == "0")
		{
			JsonData userData = LitJson.JsonMapper.ToObject(data["message"].ToString());
			userInfo = new SP.UserInfo(userData["ID"].ToString(), userData["name"].ToString(), userData["email"].ToString());
		}

        SP.UserResponse responce = new SP.UserResponse(int.Parse(data["code"].ToString()), data["message"].ToString(), userInfo);

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
