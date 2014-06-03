using System;
using System.Collections.Generic;
using SocialPlay.Generic;
using UnityEngine;
using SocialPlay.Data;
using Newtonsoft.Json;


public class WebItemService : IItemService
{
    public WebItemService(string newCloudGoodsURL)
    {
        cloudGoodsURL = newCloudGoodsURL;
    }

    public static string cloudGoodsURL = "http://192.168.0.197/webservice/cloudgoods/cloudgoodsservice.svc/";

    //public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<string> callback, string ANDTags = "", string ORTags = "")
    //{
    //    string url = string.Format("{0}GenerateItemsAtLocation?OwnerID={1}&OwnerType={2}&Location={3}&AppID={4}&MinimumEnergyOfItem={5}&TotalEnergyToGenerate={6}&ANDTags={7}&ORTags={8}", cloudGoodsURL, OwnerID, OwnerType, Location, AppID, MinimumEnergyOfItem, TotalEnergyToGenerate, ANDTags, ORTags);
    //    WWWPacket.Creat(url, callback);
    //}

    public void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<string> callback)
    {
        string url = string.Format("{0}GetOwnerItems?ownerID={1}&ownerType={2}&location={3}&AppID={4}", cloudGoodsURL, ownerID, ownerType, location, AppID.ToString());
        WWWPacket.Creat(url, callback);
    }

    //public void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    //{
    //    string url = string.Format("{0}MoveItemStacks?stacks={1}&DestinationOwnerID={2}&DestinationOwnerType={3}&AppID={4}&DestinationLocation={5}", cloudGoodsURL, stacks, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);
    //    WWWPacket.Creat(url, callback);
    //}

    //public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    //{
    //    string url = string.Format("{0}MoveItemStack?StackToMove={1}&MoveAmount={2}&DestinationOwnerID={3}&DestinationOwnerType={4}&AppID={5}&DestinationLocation={6}", cloudGoodsURL, StackToMove, MoveAmount, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);
    //    WWWPacket.Creat(url, callback);
    //}

    //public void RemoveItemStack(Guid StackRemove, Action<string> callback)
    //{
    //    string url = string.Format("{0}RemoveStackItem?stackID={1}", cloudGoodsURL, StackRemove);
    //    WWWPacket.Creat(url, callback);
    //}


    //public void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback)
    //{
    //    string url = string.Format("{0}DeductStackAmount?stackID={1}&amount={2}", cloudGoodsURL, StackRemove, amount);
    //    WWWPacket.Creat(url, callback);
    //}



    //public void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback)
    //{
    //    RemoveMultipleItems infos = new RemoveMultipleItems();
    //    infos.stacks = StacksToRemove;
    //    string stacksInfo = JsonConvert.SerializeObject(infos);
    //    string url = string.Format("{0}RemoveStackItems?stacks={1}", cloudGoodsURL, stacksInfo);
    //    WWWPacket.Creat(url, callback);
    //}

    //public void CompleteQueueItem(Guid gameID, int QueueID, int percentScore, int location, Action<string> callback)
    //{
    //    string url = string.Format("{0}CompleteQueueItem?gameID={1}&QueueID={2}&percentScore={3}&location={4}", cloudGoodsURL, gameID, QueueID, percentScore, location);
    //    WWWPacket.Creat(url, callback);
    //}

    //public void AddInstantCraftToQueue(Guid gameID, Guid UserID, int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback)
    //{
    //    string url = string.Format("{0}AddInstantCraftToQueue?gameID={1}&UserID={2}&ItemID={3}&Amount={4}&ItemIngredients={5}", cloudGoodsURL, gameID, UserID, ItemID, Amount, WWW.EscapeURL(JsonConvert.SerializeObject(ItemIngredients)));

    //    WWWPacket.Creat(url, callback);
    //}

    //public void Login(Guid gameID, string userEmail, string password, Action<string> callback)
    //{
    //    string url = string.Format("{0}SPLoginUserLogin?gameID={1}&userEMail={2}&userPassword={3}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail), WWW.EscapeURL(password));

    //    WWWPacket.Creat(url, callback);
    //}


    //public void Register(Guid gameID, string userEmail, string password, string userName, Action<string> callback)
    //{
    //    string url = string.Format("{0}SPLoginUserRegister?gameID={1}&userEMail={2}&userPassword={3}&userName={4}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail), WWW.EscapeURL(password), WWW.EscapeURL(userName));

    //    WWWPacket.Creat(url, callback);
    //}


    //public void ForgotPassword(Guid gameID, string userEmail, Action<string> callback)
    //{
    //    string url = string.Format("{0}ForgotPassword?gameID={1}&userEMail={2}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail));
    //    WWWPacket.Creat(url, callback);
    //}


    //public void ResendVerificationEmail(Guid gameID, string userEmail, Action<string> callback)
    //{
    //    string url = string.Format("{0}ResendVerificationEmail?gameID={1}&userEMail={2}", cloudGoodsURL, gameID, WWW.EscapeURL(userEmail));
    //    WWWPacket.Creat(url, callback);
    //}
}
