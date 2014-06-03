using UnityEngine;
using System.Collections;

public enum SPPlatforms
{
    Facebook = 1,
    SocialPlay = 2,
    Google = 3,
    Twitter = 4,
    Custom = 5
}

public enum SPBundle
{
    CreditCoinPurchaseable = 1,
    CreditPurchasable = 2,
    CoinPurchasable = 3,
    Free = 4
}

public enum SocialPlayMessage
{
    OnSPLogin,
    OnSPLogout,
    OnForgotPassword,
    OnVerificationSent,
}
