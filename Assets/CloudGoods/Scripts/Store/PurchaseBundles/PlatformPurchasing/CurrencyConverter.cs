using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPlay;
using Newtonsoft.Json.Linq;

class CurrencyConverter
{
    static float standardCurrencyFactor = 1;
    static float premiumCurrencyFactor = 1;
    static float resellFactor = 0.5f;

    public static void Initialize()
    {
        ServiceClient.Open.GetPriceMultipliers(ReceivedConversionFactor);
    }

    private static void ReceivedConversionFactor(string jsonData)
    {
        string stripped = JToken.Parse(jsonData).ToString();
        JObject obj = JObject.Parse(stripped);

        CheckForInvalidJObjects(obj);
        CheckForZeroOrLessConversionFactor();
    }

    private static void CheckForInvalidJObjects(JObject obj)
    {
        if (obj["Coin"] == null || string.IsNullOrEmpty(obj["Coin"].ToString()) ||
            !float.TryParse(obj["Coin"].ToString(), out standardCurrencyFactor))
        {
            throw new System.Exception("Could not parse Standard currency factor from json data.");
        }

        if (obj["Credit"] == null || string.IsNullOrEmpty(obj["Credit"].ToString()) ||
            !float.TryParse(obj["Credit"].ToString(), out premiumCurrencyFactor))
        {
            throw new System.Exception("Could not parse Premium currency factor from json data.");
        }
    }

    private static void CheckForZeroOrLessConversionFactor()
    {
        if (standardCurrencyFactor <= 0)
        {
            throw new System.Exception("Coin conversion factor returned zero or less.");
        }

        if (premiumCurrencyFactor <= 0)
        {
            throw new System.Exception("Credit conversion factor returned zero or less.");
        }
    }

    /// <summary>
    /// Converts the item enegry to premium currency worth.
    /// </summary>
    /// <param name="itemEnergy">item energy</param>
    /// <returns>item price in premium currency according to item energy</returns>
    public static int GetPremiumCurrency(int itemEnergy)
    {
        return ((int)Math.Ceiling((itemEnergy * premiumCurrencyFactor)));
    }

    /// <summary>
    /// Converts the item energy to standard currency worth.
    /// </summary>
    /// <param name="itemEnergy">item energy</param>
    /// <returns>item price in standard currency according to item energy</returns>
    public static int GetStandardCurrency(int itemEnergy)
    {
        return (int)(itemEnergy * standardCurrencyFactor);
    }

    /// <summary>
    /// Get the resell value of an item according to its item enegry.
    /// </summary>
    /// <param name="itemEnergy">item enegry</param>
    /// <returns>item price in standard currency the user will get if item is sold</returns>
    public static int GetResellStandardCurrency(int itemEnergy)
    {
        return (int)(GetStandardCurrency(itemEnergy) * resellFactor);
    }
}
