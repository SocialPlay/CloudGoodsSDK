using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SocialPlay.Data;
using LitJson;

public class LitJsonFxJsonObjectConverter : IServiceObjectConverter
{

    public List<ItemData> ConvertToItemDataList(string ObjectData)
    {
        ItemDataList itemDataList = new SocialPlay.Data.ItemDataList();

        string parsedString = ParseString(ObjectData);

        if (parsedString == "[]")
            return new List<ItemData>();

        JsonReader reader = new JsonReader(parsedString);
        reader.Read();
        if (reader.Token.ToString() == "ArrayStart")
        {
            while (reader.Token.ToString() != "ArrayEnd")
            {
                reader.Read();

                CreateItemData(itemDataList, reader);
            }
        }
        else
        {
            CreateItemData(itemDataList, reader);
        }

        List<ItemData> items = CloudGoods.itemDataConverter.ConvertItems(itemDataList);

        return items;
    }

    private static void CreateItemData(ItemDataList itemDataList, JsonReader reader)
    {
        if (reader.Token.ToString() == "ObjectStart")
        {
            SocialPlay.Data.ItemData itemData = new SocialPlay.Data.ItemData();
            while (reader.Token.ToString() != "ObjectEnd")
            {
                reader.Read();

                if (reader.Token.ToString() == "PropertyName")
                {
                    string propertyString = reader.Value.ToString();

                    reader.Read();
                    if (propertyString == "StackLocationID")
                    {
                        itemData.StackLocationID = new Guid(reader.Value.ToString());
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
                        if (reader.Value != null)
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
                        if (reader.Value != null)
                            itemData.Tags = reader.Value.ToString();
                    }
                }
            }
            itemDataList.Add(itemData);
        }
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

    public bool ConvertToBool(string dataString)
    {
        return ParseBool(dataString);

    }


    public Dictionary<string, string> ConvertToDictionary(string dataString)
    {
        Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
        string parsedString = ParseString(dataString);
        JsonData dataArray = LitJson.JsonMapper.ToObject(parsedString);

        for (int i = 0; i < dataArray.Count; i++)
        {
            string key = dataArray[i]["Key"] != null ? dataArray[i]["Key"].ToString() : null;
            string value = dataArray[i]["Value"] != null ? dataArray[i]["Value"].ToString() : null;
            dataDictionary.Add(key, value);
        }
        return dataDictionary;
    }

    public List<StoreItem> ConvertToStoreItems(string dataString)
    {
        Debug.Log("Store call: " + dataString);

        string storeString = ParseString(dataString);

        List<StoreItem> storeItems = new List<StoreItem>();

        JsonData storeItemsJsonArray = LitJson.JsonMapper.ToObject(storeString);

        for (int i = 0; i < storeItemsJsonArray.Count; i++)
        {
            for (int e = 0, emax = storeItemsJsonArray[i].Count; e < emax; e++)
            {
                if (storeItemsJsonArray[i][e].IsArray)
                {
                    for (int x = 0, xmax = storeItemsJsonArray[i][e].Count; x < xmax; x++)
                    {
                        Debug.Log("storeItem " + storeItemsJsonArray[i][e][x]);
                    }
                }
            }
            StoreItem storeItemInfo = new StoreItem();
            storeItemInfo.addedDate = DateTime.Parse(storeItemsJsonArray[i]["AddDate"].ToString());
            Debug.Log("Added date: " + storeItemInfo.addedDate.ToString());
            storeItemInfo.ID = int.Parse(storeItemsJsonArray[i]["ID"].ToString());
            storeItemInfo.itemName = storeItemsJsonArray[i]["Name"].ToString();
            storeItemInfo.itemID = int.Parse(storeItemsJsonArray[i]["ItemID"].ToString());
            storeItemInfo.premiumCurrencyValue = int.Parse(storeItemsJsonArray[i]["CreditValue"].ToString());
            storeItemInfo.standardCurrencyValue = int.Parse(storeItemsJsonArray[i]["CoinValue"].ToString());

            JsonData storeItemDetailArray = LitJson.JsonMapper.ToObject(storeItemsJsonArray[i]["Detail"].ToString());

            List<StoreItemDetail> storeItemDetails = new List<StoreItemDetail>();

            for (int j = 0; j < storeItemDetailArray.Count; j++)
            {
                StoreItemDetail detail = new StoreItemDetail();
                detail.propertyName = storeItemDetailArray[j]["Name"].ToString();
                detail.propertyValue = (int)float.Parse(storeItemDetailArray[j]["Value"].ToString());

                //detail.invertEnergy = (bool)storeItemDetailArray[j]["InvertEnergy"];
                Debug.Log("storeitem detail : " + detail.propertyName);
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

    public CloudGoodsUser ConvertToUserInfo(string dataString)
    {
        string userInfoString = ParseString(dataString);
        JsonData data = LitJson.JsonMapper.ToObject(userInfoString);

        CloudGoodsUser userinfo = new CloudGoodsUser(data["userGuid"].ToString().Trim(), data["userName"].ToString(), "");

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
            itemBundle.State = (CloudGoodsBundle)Enum.Parse(typeof(CloudGoodsBundle), itemBundleData[i]["State"].ToString());
            itemBundle.Image = itemBundleData[i]["Image"].ToString();

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
                    bundleDetails.Value = float.Parse(bundleItemDetailData[k]["Value"].ToString());

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
        List<PaidCurrencyBundleItem> creditBundles = new List<PaidCurrencyBundleItem>();

        string parsedString = ParseString(dataString);

        JsonData creditBundleObj = LitJson.JsonMapper.ToObject(parsedString);

        for (int i = 0; i < creditBundleObj.Count; i++)
        {
            PaidCurrencyBundleItem creditBundle = new PaidCurrencyBundleItem();
            creditBundle.Amount = int.Parse(creditBundleObj[i]["CreditAmount"].ToString());
            creditBundle.Cost = creditBundleObj[i]["Cost"].ToString();
            creditBundle.CurrencyIcon = creditBundleObj[i]["Image"].ToString();
            creditBundle.Description = creditBundleObj[i]["Description"].ToString();
            creditBundle.ID = int.Parse(creditBundleObj[i]["ID"].ToString());
            creditBundle.CurrencyName = "$";
            creditBundle.BundleName = creditBundleObj[i]["Name"].ToString();

            for (int j = 0; j < creditBundleObj[i]["Data"].Count; j++)
            {
                creditBundle.CreditPlatformIDs.Add(creditBundleObj[i]["Data"][j]["Key"].ToString(), creditBundleObj[i]["Data"][j]["Value"].ToString());
            }

            creditBundles.Add(creditBundle);
        }

        return creditBundles;
    }

    public MoveMultipleItemsResponse ConvertToMoveMultipleItemsResponse(string dataString)
    {
        MoveMultipleItemsResponse infos = LitJson.JsonMapper.ToObject<MoveMultipleItemsResponse>(dataString);

        return infos;
    }

    public UserResponse ConvertToSPLoginResponse(string dataString)
    {
        string parsedString = ParseString(dataString);

        JsonData data = LitJson.JsonMapper.ToObject(parsedString);

        CloudGoodsUser userInfo = null;

        if (data["code"].ToString() == "0")
        {
            JsonData userData = LitJson.JsonMapper.ToObject(data["message"].ToString());
            userInfo = new CloudGoodsUser(userData["ID"].ToString(), userData["name"].ToString(), userData["email"].ToString());
        }

        UserResponse responce = new UserResponse(int.Parse(data["code"].ToString()), data["message"].ToString(), userInfo);

        return responce;
    }

    public WorldCurrencyInfo ConvertToWorldCurrencyInfo(string dataString)
    {
        JsonData worldCurrencyInfoObj = LitJson.JsonMapper.ToObject(dataString);

        WorldCurrencyInfo worldCurrencyInfo = new WorldCurrencyInfo();

        worldCurrencyInfo.FreeCurrencyName = worldCurrencyInfoObj["InGameCurrencyName"].ToString();
        worldCurrencyInfo.FreeCurrencyImage = worldCurrencyInfoObj["InGameCurrencyImage"].ToString();
        worldCurrencyInfo.PaidCurrencyName = worldCurrencyInfoObj["PaidCurrencyName"].ToString();
        worldCurrencyInfo.PaidCurrencyImage = worldCurrencyInfoObj["PaidCurrencyImage"].ToString();

        return worldCurrencyInfo;
    }

    public ConsumeResponse ConverToConsumeCreditsResponse(string dataString)
    {
        string parsedString = ParseString(dataString);

        JsonData jsonData = LitJson.JsonMapper.ToObject(parsedString);

        ConsumeResponse consumeResponse = new ConsumeResponse();

        consumeResponse.Result = int.Parse(jsonData["Result"].ToString());

        if (jsonData["Message"] != null)
            consumeResponse.Message = jsonData["Message"].ToString();

        consumeResponse.Balance = int.Parse(jsonData["Balance"].ToString());

        return consumeResponse;
    }

    public List<UserDataValue> ConvertToUserDataValueList(string dataString)
    {
        List<UserDataValue> allValues = new List<UserDataValue>();
        string parsedString = ParseString(dataString);
        JsonData dataArray = LitJson.JsonMapper.ToObject(parsedString);

        for (int i = 0; i < dataArray.Count; i++)
        {
            string userName = dataArray[i]["userName"] != null ? dataArray[i]["userName"].ToString() : null;
            int platformID = int.Parse(dataArray[i]["PlatformId"] != null ? dataArray[i]["PlatformId"].ToString() : "0");
            string platformUserID = dataArray[i]["PlatformUserId"] != null ? dataArray[i]["PlatformUserId"].ToString() : null;
            string userID = dataArray[i]["userID"] != null ? dataArray[i]["userID"].ToString() : null;
            string newValue = dataArray[i]["Value"] != null ? dataArray[i]["Value"].ToString() : null;
            UserDataValue value = new UserDataValue(userName, platformID, platformUserID, userID, newValue);
            allValues.Add(value);
        }
        return allValues;
    }

    string ParseString(string dataString)
    {
        string parseString = dataString.Remove(0, 1);
        parseString = parseString.Remove(parseString.Length - 1, 1);

        parseString = parseString.Replace("\\\"", "\"");
        parseString = parseString.Replace("\\\\", "\\");


        return parseString;
    }

    bool ParseBool(string dataString)
    {
        return bool.Parse(ParseString(dataString));
    }

}
