using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SocialPlay.Data;

public class LitJsonFxJsonObjectConverter : IServiceObjectConverter {

    public List<ItemData> ConvertToItemDataList(string ObjectData)
    {
        return null;
    }

    public Guid ConvertToGuid(string dataString)
    {
        return Guid.Empty;
    }

    public string ConvertToString(string dataString)
    {
        return "";
    }

    public List<StoreItemInfo> ConvertToStoreItems(string dataString)
    {
        return new List<StoreItemInfo>();
    }

    public WebserviceCalls.UserInfo ConvertToUserInfo(string datString)
    {
        return new WebserviceCalls.UserInfo("", "", "");
    }

    public List<RecipeInfo> ConvertToListRecipeInfo(string dataString)
    {
        return new List<RecipeInfo>();
    }

    public List<ItemBundle> ConvertToListItemBundle(string dataString)
    {
        return new List<ItemBundle>();
    }

    public List<CreditBundleItem> ConvertToListCreditBundleItem(string dataString)
    {
        return new List<CreditBundleItem>();
    }
    public MoveMultipleItemsResponse ConvertToMoveMultipleItemsResponse(string dataString)
    {
        return new MoveMultipleItemsResponse();
    }

    public SPLogin.SPLogin_Responce ConvertToSPLoginResponse(string dataString)
    {
        return new SPLogin.SPLogin_Responce(0, "");
    }

}
