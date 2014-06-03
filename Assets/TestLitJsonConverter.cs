using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SocialPlay.Data;

public class TestLitJsonConverter : MonoBehaviour {

	void Start () {
        SP.GetOwnerItems("", "", 0, Guid.Empty, OnReceivedItemDataList);
        //SP.MoveItemStack(Guid.Empty, 0, "", "", Guid.Empty, 0, OnReceivedGuid);
        //SP.GetStoreItems("", OnReceivedStoreItems);
        //SP.GetGameRecipes("", OnReceivedListRecipes);
        //SP.GetCreditBundles("", 0, OnReceivedCreditBundles);
        //SP.Login(Guid.Empty, "", "", OnReceivedLoginResponse);
        //SP.GetUserFromWorld(Guid.Empty, 0, "", "", "", OnReceivedUserInfo);
        //SP.GetItemBundles("", OnReceivedItemBundles);
	}

    void OnReceivedItemBundles(List<ItemBundle> itemBundles)
    {
        foreach (ItemBundle bundle in itemBundles)
        {
            Debug.Log("Item Bundles: " + bundle.Name + " Bundle Item: " + bundle.bundleItems[0].Name + " BundleItemDetail: " + bundle.bundleItems[0].bundleItemDetails[0].BundleDetailName);
        }
    }

    void OnReceivedUserInfo(SP.UserInfo userInfo)
    {
        Debug.Log("User info: " + userInfo.userName);
    }

    void OnReceivedLoginResponse(SP.UserResponse response)
    {
        Debug.Log("Login Response: " + response.code + " UserInfo: " + response.userInfo.userName);
    }

    void OnReceivedMoveMultipleItemsResponse(MoveMultipleItemsResponse moveItems)
    {
        Debug.Log("Moved Item Stack ID: " + moveItems.movedItems[0].OriginalStackID + " Moved Item New Stack ID: " + moveItems.movedItems[0].NewStackID);
    }

    void OnReceivedCreditBundles(List<CreditBundleItem> creditBundles)
    {
        foreach (CreditBundleItem creditBundle in creditBundles)
        {
            Debug.Log("CreditBundles: " + creditBundle.ID + " Cost: " + creditBundle.Cost);
        }
    }

    void OnReceivedGuid(Guid newGuid)
    {
        Debug.Log("Guid: " + newGuid);
    }

    void OnReceivedListRecipes(List<RecipeInfo> recipes)
    {
        foreach (RecipeInfo recipe in recipes)
        {
            Debug.Log("Recipes: " + recipe.name + " ingredient: " + recipe.IngredientDetails[0].name);

        }
    }

    void OnReceivedStoreItems(List<StoreItemInfo> storeItems)
    {
        foreach (StoreItemInfo storeItem in storeItems)
        {
            Debug.Log("Store Items: " + storeItem.itemName);
        }
    }

    void OnReceivedItemDataList(List<ItemData> listItemData)
    {
        foreach (ItemData itemData in listItemData)
        {
            Debug.Log("Item Data list: " + itemData.itemName);

            foreach (KeyValuePair<string, float> entry in itemData.stats)
            {
                Debug.Log("Key: " + entry.Key + " Value: " + entry.Value);
            }
        }
    }
}
