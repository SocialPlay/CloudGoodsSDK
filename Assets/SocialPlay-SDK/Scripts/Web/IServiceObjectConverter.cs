using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SocialPlay.Data;

public interface IServiceObjectConverter {

    List<ItemData> ConvertToItemDataList(string ObjectData);

    Guid ConvertToGuid(string dataString);

    string ConvertToString(string dataString);

    List<StoreItemInfo> ConvertToStoreItems(string dataString);

    WebserviceCalls.UserInfo ConvertToUserInfo(string dataString);

    List<RecipeInfo> ConvertToListRecipeInfo(string dataString);

    List<ItemBundle> ConvertToListItemBundle(string dataString);

    List<CreditBundleItem> ConvertToListCreditBundleItem(string dataString);

    MoveMultipleItemsResponse ConvertToMoveMultipleItemsResponse(string dataString);

    SPLogin.SPLogin_Responce ConvertToSPLoginResponse(string dataString);

}
