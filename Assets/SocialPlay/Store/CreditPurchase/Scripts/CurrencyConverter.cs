using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPlay;
using Newtonsoft.Json.Linq;

class CurrencyConverter
{
    static float freeCurrencyFactor = 1;
    static float paidCurrencyFactor = 1;
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
            !float.TryParse(obj["Coin"].ToString(), out freeCurrencyFactor))
        {
            throw new System.Exception("Could not parse free currency factor from json data.");
        }

        if (obj["Credit"] == null || string.IsNullOrEmpty(obj["Credit"].ToString()) ||
            !float.TryParse(obj["Credit"].ToString(), out paidCurrencyFactor))
        {
            throw new System.Exception("Could not parse paid currency factor from json data.");
        }
    }

    private static void CheckForZeroOrLessConversionFactor()
    {
        if (freeCurrencyFactor <= 0)
        {
            throw new System.Exception("Coin conversion factor returned zero or less.");
        }

        if (paidCurrencyFactor <= 0)
        {
            throw new System.Exception("Credit conversion factor returned zero or less.");
        }
    }

    /// <summary>
    /// Converts the item enegry to paid currency worth.
    /// </summary>
    /// <param name="itemEnergy">item energy</param>
    /// <returns>item price in paid currency according to item energy</returns>
    public static int GetPaidCurrency(int itemEnergy)
    {
        return ((int)Math.Ceiling((itemEnergy * paidCurrencyFactor)));
    }

    /// <summary>
    /// Converts the item energy to free currency worth.
    /// </summary>
    /// <param name="itemEnergy">item energy</param>
    /// <returns>item price in free currency according to item energy</returns>
    public static int GetFreeCurrency(int itemEnergy)
    {
        return (int)(itemEnergy * freeCurrencyFactor);
    }

    /// <summary>
    /// Get the resell value of an item according to its item enegry.
    /// </summary>
    /// <param name="itemEnergy">item enegry</param>
    /// <returns>item price in free currency the user will get if item is sold</returns>
    public static int GetResellFreeCurrency(int itemEnergy)
    {
        return (int)(GetFreeCurrency(itemEnergy) * resellFactor);
    }
}
