using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IServiceCalls {

    void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<string> callback, string ANDTags = "", string ORTags = "");

    void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<string> callback);

    void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback);

    void GetUserFromWorld(Guid appID, int platformID, string platformUserID, string userName, string userEmail, Action<string> callback);

    void GetStoreItems(string appID, Action<string> callback);

    void GetFreeCurrencyBalance(string userID, int accessLocation, string appID, Action<string> callback);

    void GetPaidCurrencyBalance(string userID, string appID, Action<string> callback);

    void RegisterGameSession(Guid userID, string AppID, int instanceID, Action<string> callback);

    void GetGameRecipes(string appID, Action<string> callback);

    void StoreItemPurchase(string URL, Guid userID, int itemID, int amount, string paymentType, Guid appID, int saveLocation, Action<string> callback);

    void GetCreditBundles(string URL, Action<string> callback);

    void GetAccessPinFromGuid(string userPin, Action<string> callback);

    void GetAccessPinForUser(string UserId, Action<string> callback);

    void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback);

    void RemoveItemStack(Guid StackRemove, Action<string> callback);

    void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback);

    void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback);

    void CompleteQueueItem(Guid gameID, int QueueID, int percentScore, int location, Action<string> callback);

    void AddInstantCraftToQueue(Guid gameID, Guid UserID, int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback);

    void SPLogin_UserLogin(Guid gameID, string userEmail, string password, Action<string> callback);

    void SPLogin_UserRegister(Guid gameID, string userEmail, string password, string userName, Action<string> callback);

    void SPLoginForgotPassword(Guid gameID, string userEmail, Action<string> callback);

    void SPLoginResendVerificationEmail(Guid gameID, string userEmail, Action<string> callback);
}
