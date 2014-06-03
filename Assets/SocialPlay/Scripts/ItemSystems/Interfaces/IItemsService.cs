using System;
using System.Collections.Generic;
using SocialPlay.Generic;
using UnityEngine;
using SocialPlay.Data;


public interface IItemService
{
    //void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<string> callback, string ANDTags = "", string ORTags = "");

    //void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<string> callback);

    //void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback);

    //void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback);

    //void RemoveItemStack(Guid StackRemove, Action<string> callback);

    //void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback);

    //void DeductStackAmount(Guid StackRemove, int amount, Action<string> callback);

    //void CompleteQueueItem(Guid gameID, int QueueID, int percentScore, int location, Action<string> callback);

    //void AddInstantCraftToQueue(Guid gameID, Guid UserID, int ItemID, int Amount, List<KeyValuePair<string, int>> ItemIngredients, Action<string> callback);

    //void Login(Guid gameID, string userEmail, string password, Action<string> callback);

    //void Register(Guid gameID, string userEmail, string password, string userName, Action<string> callback);

    //void ForgotPassword(Guid gameID, string userEmail, Action<string> callback);

    //void ResendVerificationEmail(Guid gameID, string userEmail, Action<string> callback);

}
