using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Newtonsoft.Json;

public class MockWebItemService : MonoBehaviour, IServiceCalls {

    void Awake()
    {
        WebserviceCalls.webservice = this;
    }

    public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<string> callback, string ANDTags = "", string ORTags = "")
    {
        callback("[{\"StackLocationID\":\"75fdc451-ba94-4d76-8928-85ab2261be1d\",\"Amount\":2,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":60}]\",\"ItemID\":47382,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":60,\"Energy\":60,\"SellPrice\":24,\"Name\":\"Steel\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":310,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"cec12951-a8f0-43bb-8686-8afbe941432e\",\"Amount\":3,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":50}]\",\"ItemID\":47381,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":50,\"Energy\":50,\"SellPrice\":20,\"Name\":\"Iron\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/953d4f54-c8fd-4977-b54e-cbbf6bdacd0d.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":309,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"6091e802-4820-4942-a66f-df4dcb977601\",\"Amount\":4,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":40}]\",\"ItemID\":47383,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":40,\"Energy\":40,\"SellPrice\":16,\"Name\":\"Wood\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/784b6c1f-b8ae-4ed1-a67f-16e350b21a79.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":311,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"f6605f73-c85e-4377-b762-35cef60ec598\",\"Amount\":7,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":10}]\",\"ItemID\":47349,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":10,\"Energy\":10,\"SellPrice\":4,\"Name\":\"Dollar\",\"Image\":\"http:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/ffa0c54d-12d7-454a-827f-e7a13f921b64.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":277,\"Description\":\"Use Dollars to purchase items from the Store\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"Dollar\\\",\\\"droppable\\\"]\"}]");
    }

    public void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<string> callback)
    { 

    }

    public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    {

    }

    public void GetUserFromWorld(Guid appID, int platformID, string platformUserID, string userName, string userEmail, Action<string> callback)
    {

    }

    public void GetStoreItems(string appID, Action<string> callback)
    {

    }

    public void GetFreeCurrencyBalance(string userID, int accessLocation, string appID, Action<string> callback)
    {

    }

    public void GetPaidCurrencyBalance(string userID, string appID, Action<string> callback)
    {

    }

    public void RegisterGameSession(Guid userID, string AppID, int instanceID, Action<string> callback)
    {

    }

    public void GetGameRecipes(string appID, Action<string> callback)
    {

    }

    public void StoreItemPurchase(string URL, Guid userID, int itemID, int amount, string paymentType, Guid appID, int saveLocation, Action<string> callback)
    {

    }

    public void GetCreditBundles(string URL, Action<string> callback)
    {

    }

    public void GetAccessPinFromGuid(string userPin, Action<string> callback)
    {

    }

    public void GetAccessPinForUser(string UserId, Action<string> callback)
    {

    }

    public void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    {

    }

    public void RemoveItemStack(Guid StackRemove, Action<string> callback)
    {

    }

    public void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback)
    {

    }

    public void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback)
    {

    }

    public void CompleteQueueItem(Guid gameID, int QueueID, int percentScore, int location, Action<string> callback)
    {

    }

    public void AddInstantCraftToQueue(Guid gameID, Guid UserID, int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback)
    {

    }

    public void SPLogin_UserLogin(Guid gameID, string userEmail, string password, Action<string> callback)
    {

    }

    public void SPLogin_UserRegister(Guid gameID, string userEmail, string password, string userName, Action<string> callback)
    {

    }

    public void SPLoginForgotPassword(Guid gameID, string userEmail, Action<string> callback)
    {

    }

    public void SPLoginResendVerificationEmail(Guid gameID, string userEmail, Action<string> callback)
    {

    }
}
