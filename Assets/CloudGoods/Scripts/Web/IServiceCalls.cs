using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SocialPlay.Data;

public interface IServiceCalls
{
    void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<List<ItemData>> callback, string ANDTags = "", string ORTags = "");

    void GetOwnerItems(string ownerID, string ownerType, int location, Action<List<ItemData>> callback);

    void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, int DestinationLocation, Action<Guid> callback);

    void GetUserFromWorld(CloudGoodsPlatform platform, string platformUserID, string userName, string userEmail, Action<CloudGoodsUser> callback);

    void GetStoreItems(Action<List<StoreItem>> callback);

    void GetFreeCurrencyBalance(int accessLocation, Action<string> callback);

    void GetPaidCurrencyBalance(Action<string> callback);

    void RegisterGameSession(int instanceID, Action<Guid> callback);

    void GetGameRecipes(Action<List<RecipeInfo>> callback);

    void StoreItemPurchase(int itemID, int amount, string paymentType, int saveLocation, Action<string> callback);

    void GetItemBundles(Action<List<ItemBundle>> callback);

    void PurchaseItemBundles(int bundleID, string paymentType, int location, Action<string> callback);

	void GetCreditBundles(CloudGoodsPlatform platform, Action<List<PaidCurrencyBundleItem>> callback);

    void PurchaseCreditBundles(string payload, Action<string> callback);

    void GetAccessPinFromGuid(string userPin, Action<string> callback);

    void GetAccessPinForUser(string UserId, Action<string> callback);

    void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, int DestinationLocation, Action<MoveMultipleItemsResponse> callback);

    void RemoveItemStack(Guid StackRemove, Action<string> callback);

    void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback);

    void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback);

    void CompleteQueueItem(int QueueID, int percentScore, int location, Action<string> callback);

    void AddInstantCraftToQueue(int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback);

    void Login(string userEmail, string password, Action<UserResponse> callback);

    void Register(string userEmail, string password, string userName, Action<UserResponse> callback);

    void ForgotPassword(string userEmail, Action<UserResponse> callback);

    void ResendVerificationEmail(string userEmail, Action<UserResponse> callback);

    void GiveOwnerItems(string ownerID, WebModels.OwnerTypes OwnerType, List<WebModels.ItemsInfo> listOfItems, Action<string> callback);
}

