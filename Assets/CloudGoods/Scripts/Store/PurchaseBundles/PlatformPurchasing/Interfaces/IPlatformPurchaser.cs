// ----------------------------------------------------------------------
// <copyright file="IPlatformPurchaser.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir
// ------------------------------------------------------------------------
using System;

public interface IPlatformPurchaser
{
    event Action<string> RecievedPurchaseResponse;
    event Action<string> OnPurchaseErrorEvent;

    void Purchase(PremiumBundle id, int amount, string userID);
    void OnReceivedPurchaseResponse(string data);
}

