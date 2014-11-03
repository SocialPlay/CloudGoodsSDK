using UnityEngine;
using System.Collections;

public enum CloudGoodsPlatform
{
    Facebook = 1,
    SocialPlay = 2,
    Google = 3,
    Kongregate = 4,
    Custom = 5
}

public enum CloudGoodsBundle
{
    CreditCoinPurchaseable = 1,
    CreditPurchasable = 2,
    CoinPurchasable = 3,
    Free = 4
}

public enum CloudGoodsMessage
{
    OnSPLogin,
    OnSPLogout,
    OnForgotPassword,
    OnVerificationSent,
	OnNotEnoughFunds,
	OnPurchaseSuccess,
	OnPurchaseFail,
	OnPurchaseAmountError
}

public enum CurrencyType
{
	Standard,
	Premium
}

public enum PlatformPurchase
{
    Automatic,
    Facebook,
    Kongergate,
    Android,
    IOS    
}

public enum ImageStatus
{
    Web,
    Cache,
    Error
}