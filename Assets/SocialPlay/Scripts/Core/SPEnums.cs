using UnityEngine;
using System.Collections;

public enum SocialPlayPlatform
{
    Facebook = 1,
    SocialPlay = 2,
    Google = 3,
    Twitter = 4,
    Custom = 5
}

public enum SocialPlayBundle
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

public enum CurrencyType
{
	Coins,
	Credits
}

public enum PlatformPurchase
{
    Android,
    IOS,
    Facebook,
    Kongergate
}