using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using SocialPlay.Data;

public class MockWebItemService : MonoBehaviour, IServiceCalls
{
    public Action<string> OnErrorEvent { get; set; }
    public IServiceObjectConverter ServiceConverter { get; set; }

    void Awake()
    {
        ServiceConverter = new LitJsonFxJsonObjectConverter();
    }

    public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<List<ItemData>> callback, string ANDTags = "", string ORTags = "")
    {
        callback(ServiceConverter.ConvertToItemDataList("\"[{\"StackLocationID\":\"75fdc451-ba94-4d76-8928-85ab2261be1d\",\"Amount\":2,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":60}]\",\"ItemID\":47382,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":60,\"Energy\":60,\"SellPrice\":24,\"Name\":\"Steel\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":310,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"cec12951-a8f0-43bb-8686-8afbe941432e\",\"Amount\":3,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":50}]\",\"ItemID\":47381,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":50,\"Energy\":50,\"SellPrice\":20,\"Name\":\"Iron\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/953d4f54-c8fd-4977-b54e-cbbf6bdacd0d.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":309,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"6091e802-4820-4942-a66f-df4dcb977601\",\"Amount\":4,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":40}]\",\"ItemID\":47383,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":40,\"Energy\":40,\"SellPrice\":16,\"Name\":\"Wood\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/784b6c1f-b8ae-4ed1-a67f-16e350b21a79.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":311,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"f6605f73-c85e-4377-b762-35cef60ec598\",\"Amount\":7,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":10}]\",\"ItemID\":47349,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":10,\"Energy\":10,\"SellPrice\":4,\"Name\":\"Dollar\",\"Image\":\"http:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/ffa0c54d-12d7-454a-827f-e7a13f921b64.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":277,\"Description\":\"Use Dollars to purchase items from the Store\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"Dollar\\\",\\\"droppable\\\"]\"}]\""));
    }

    public void GetOwnerItems(string ownerID, string ownerType, int location, Action<List<ItemData>> callback)
    {
        callback(ServiceConverter.ConvertToItemDataList("\"[{\"StackLocationID\":\"75fdc451-ba94-4d76-8928-85ab2261be1d\",\"Amount\":2,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":60}]\",\"ItemID\":47382,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":60,\"Energy\":60,\"SellPrice\":24,\"Name\":\"Steel\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":310,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"cec12951-a8f0-43bb-8686-8afbe941432e\",\"Amount\":3,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":50}]\",\"ItemID\":47381,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":50,\"Energy\":50,\"SellPrice\":20,\"Name\":\"Iron\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/953d4f54-c8fd-4977-b54e-cbbf6bdacd0d.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":309,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"6091e802-4820-4942-a66f-df4dcb977601\",\"Amount\":4,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":40}]\",\"ItemID\":47383,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":40,\"Energy\":40,\"SellPrice\":16,\"Name\":\"Wood\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/784b6c1f-b8ae-4ed1-a67f-16e350b21a79.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":311,\"Description\":\"\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"droppable\\\",\\\"Resource\\\"]\"},{\"StackLocationID\":\"f6605f73-c85e-4377-b762-35cef60ec598\",\"Amount\":7,\"Detail\":\"[{\\\"Name\\\":\\\"Worth\\\",\\\"Value\\\":10}]\",\"ItemID\":47349,\"Type\":1183,\"Location\":0,\"BaseItemEnergy\":10,\"Energy\":10,\"SellPrice\":4,\"Name\":\"Dollar\",\"Image\":\"http:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/ffa0c54d-12d7-454a-827f-e7a13f921b64.png\",\"Quality\":1,\"Behaviours\":\"[]\",\"BaseItemID\":277,\"Description\":\"Use Dollars to purchase items from the Store\",\"AssetBundleName\":\"\",\"Tags\":\"[\\\"Dollar\\\",\\\"droppable\\\"]\"}]\""));
    }

    public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, int DestinationLocation, Action<Guid> callback)
    {
        callback(ServiceConverter.ConvertToGuid("\"73bcdbe5-48b8-4e8e-97bb-7fbb5b4b6155\""));
    }

	public void GetUserFromWorld(CloudGoodsPlatform platform, string platformUserID, string userName, string userEmail, Action<CloudGoodsUser> callback)
    {
        callback(ServiceConverter.ConvertToUserInfo("\"{\"userGuid\":\"c6afc667-bf54-4948-ad00-530b539f4122\",\"isNewUserToWorld\":false,\"userName\":\"Editor Test User\",\"userEmail\":null}\""));
    }

    public void GetStoreItems(Action<List<StoreItem>> callback)
    {
        callback(ServiceConverter.ConvertToStoreItems("\"[{\"ID\":409,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Class3Item1\",\"ItemID\":51829,\"CreditValue\":1,\"CoinValue\":110,\"AddDate\":\"2014-04-30T16:28:05.497\",\"Detail\":\"[{\\\"Name\\\":\\\"CP One\\\",\\\"Value\\\":\\\"7\\\",\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"CP Two\\\",\\\"Value\\\":\\\"20\\\",\\\"InvertEnergy\\\":true},{\\\"Name\\\":\\\"CP Three\\\",\\\"Value\\\":\\\"2\\\",\\\"InvertEnergy\\\":false}]\",\"Behaviours\":[],\"tags\":[]},{\"ID\":413,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Class2Item2\",\"ItemID\":51833,\"CreditValue\":9,\"CoinValue\":2138,\"AddDate\":\"2014-04-30T16:28:05.53\",\"Detail\":\"[{\\\"Name\\\":\\\"CP One\\\",\\\"Value\\\":\\\"10\\\",\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"CP Two\\\",\\\"Value\\\":\\\"25\\\",\\\"InvertEnergy\\\":false}]\",\"Behaviours\":[],\"tags\":[]},{\"ID\":417,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Class3Item3\",\"ItemID\":51837,\"CreditValue\":4,\"CoinValue\":818,\"AddDate\":\"2014-04-30T16:28:05.547\",\"Detail\":\"[{\\\"Name\\\":\\\"CP One\\\",\\\"Value\\\":\\\"50\\\",\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"CP Two\\\",\\\"Value\\\":\\\"25\\\",\\\"InvertEnergy\\\":true},{\\\"Name\\\":\\\"CP Three\\\",\\\"Value\\\":\\\"44\\\",\\\"InvertEnergy\\\":false}]\",\"Behaviours\":[],\"tags\":[]}]\""));
    }

    public void GetFreeCurrencyBalance(int accessLocation, Action<string> callback)
    {
        callback(ServiceConverter.ConvertToString("\"1337\""));
    }

    public void GetPaidCurrencyBalance(Action<string> callback)
    {

    }

    public void RegisterGameSession(int instanceID, Action<Guid> callback)
    {

    }

    public void GetGameRecipes(Action<List<RecipeInfo>> callback)
    {
        callback(ServiceConverter.ConvertToListRecipeInfo("\"[{\"ID\":280,\"Name\":\"Pistol\",\"Energy\":1073,\"Detail\":\"[{\\\"Name\\\":\\\"Damage\\\",\\\"Value\\\":30,\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"Fire Rate\\\",\\\"Value\\\":0.3}]\",\"Description\":\"Standard issue firearm.\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Recipe\":[{\"ID\":333,\"Amount\":59,\"Energy\":120,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Nuts\"},{\"ID\":311,\"Amount\":41,\"Energy\":40,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Wood\"}]},{\"ID\":282,\"Name\":\"Shotgun\",\"Energy\":1600,\"Detail\":\"[{\\\"Name\\\":\\\"Damage\\\",\\\"Value\\\":80,\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"Fire Rate\\\",\\\"Value\\\":1}]\",\"Description\":\"Standard issue shotgun with a 4 bullet spread.\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Recipe\":[{\"ID\":334,\"Amount\":50,\"Energy\":210,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Steel Plate\"},{\"ID\":333,\"Amount\":25,\"Energy\":120,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Nuts\"},{\"ID\":311,\"Amount\":25,\"Energy\":40,\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Name\":\"Wood\"}]}]\""));
    }

    public void StoreItemPurchase(int itemID, int amount, string paymentType, int saveLocation, Action<string> callback)
    {

    }

    public void GetItemBundles(Action<List<ItemBundle>> callback)
    {
        callback(ServiceConverter.ConvertToListItemBundle("\"[{\"ID\":1,\"Name\":\"Test Bundle\",\"Description\":\"Test bundle description\",\"Image\":\"\",\"CreditPrice\":5,\"CoinPrice\":100,\"items\":[{\"Quantity\":5,\"Name\":\"Class1Item1\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Detail\":\"[{\\\"Name\\\":\\\"CP One\\\",\\\"Value\\\":5,\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"CP Two\\\",\\\"Value\\\":20,\\\"InvertEnergy\\\":true}]\",\"Description\":\"\",\"Quality\":1,\"Behaviours\":[]},{\"Quantity\":8,\"Name\":\"Class1Item2\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Detail\":\"[{\\\"Name\\\":\\\"CP One\\\",\\\"Value\\\":25,\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"CP Two\\\",\\\"Value\\\":2,\\\"InvertEnergy\\\":true}]\",\"Description\":\"\",\"Quality\":1,\"Behaviours\":[]},{\"Quantity\":3,\"Name\":\"Class2Item1\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Detail\":\"[{\\\"Name\\\":\\\"CP One\\\",\\\"Value\\\":\\\"20\\\",\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"CP Two\\\",\\\"Value\\\":\\\"19\\\",\\\"InvertEnergy\\\":false}]\",\"Description\":\"\",\"Quality\":1,\"Behaviours\":[]}],\"State\":1},{\"ID\":2,\"Name\":\"1 Item Test Bundle\",\"Description\":\"Bundle containing a single item\",\"Image\":\"\",\"CreditPrice\":1,\"CoinPrice\":10,\"items\":[{\"Quantity\":100,\"Name\":\"Class1Item2\",\"Image\":\"https:\\/\\/socialplay.blob.core.windows.net\\/itemimages\\/2b828b8e-8c1e-4090-a0bc-c1677e258eba.png\",\"Detail\":\"[{\\\"Name\\\":\\\"CP One\\\",\\\"Value\\\":25,\\\"InvertEnergy\\\":false},{\\\"Name\\\":\\\"CP Two\\\",\\\"Value\\\":2,\\\"InvertEnergy\\\":true}]\",\"Description\":\"\",\"Quality\":1,\"Behaviours\":[]}],\"State\":1}]\""));
    }

    public void PurchaseItemBundles(int bundleID, string paymentType, int location, Action<string> callback)
    {

    }

    public void GetCreditBundles(CloudGoodsPlatform platform, Action<List<PaidCurrencyBundleItem>> callback)
    {
        callback(ServiceConverter.ConvertToListPaidCurrencyBundleItem("\"[{\"ID\":1,\"Name\":\"15 Credits\",\"Image\":null,\"Description\":null,\"Cost\":1.00,\"CreditAmount\":15},{\"ID\":2,\"Name\":\"30 Credits\",\"Image\":null,\"Description\":null,\"Cost\":2.00,\"CreditAmount\":30},{\"ID\":3,\"Name\":\"75 Credits\",\"Image\":null,\"Description\":null,\"Cost\":5.00,\"CreditAmount\":75},{\"ID\":4,\"Name\":\"150 Credits\",\"Image\":null,\"Description\":null,\"Cost\":10.00,\"CreditAmount\":150},{\"ID\":5,\"Name\":\"300 Credits\",\"Image\":null,\"Description\":null,\"Cost\":20.00,\"CreditAmount\":300},{\"ID\":6,\"Name\":\"750 Credits\",\"Image\":null,\"Description\":null,\"Cost\":50.00,\"CreditAmount\":750}]\""));
    }

    public void PurchaseCreditBundles(string payload, Action<string> callback)
    {

    }

    public void GetAccessPinFromGuid(string userPin, Action<string> callback)
    {

    }

    public void GetAccessPinForUser(string UserId, Action<string> callback)
    {

    }

    public void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, int DestinationLocation, Action<MoveMultipleItemsResponse> callback)
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

    public void CompleteQueueItem(int QueueID, int percentScore, int location, Action<string> callback)
    {

    }

    public void AddInstantCraftToQueue(int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback)
    {

    }

    public void Login(string userEmail, string password, Action<UserResponse> callback)
    {
        callback(ServiceConverter.ConvertToSPLoginResponse("\"{\"code\":0,\"message\":\"{\\\"ID\\\":\\\"1645c50a-538c-45b5-97f7-492b448f3c20\\\",\\\"name\\\":\\\"Test User\\\",\\\"email\\\":\\\"Testuser@gmail.com\\\"}\"}\""));
    }

    public void Register(string userEmail, string password, string userName, Action<UserResponse> callback)
    {

    }

    public void ForgotPassword(string userEmail, Action<UserResponse> callback)
    {

    }

    public void ResendVerificationEmail(string userEmail, Action<UserResponse> callback)
    {

    }


    public void GiveOwnerItems(string ownerID, WebModels.OwnerTypes OwnerType, List<WebModels.ItemsInfo> listOfItems, Action<string> callback)
    {
    }
}
