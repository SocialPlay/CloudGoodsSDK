using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPlay;
using Newtonsoft.Json.Linq;

class CurrencyConverter
{
    static float coinFactor = 1;
    static float creditFactor = 1;
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
            !float.TryParse(obj["Coin"].ToString(), out coinFactor))
        {
            throw new System.Exception("Could not parse coin factor from json data.");
        }

        if (obj["Credit"] == null || string.IsNullOrEmpty(obj["Credit"].ToString()) ||
            !float.TryParse(obj["Credit"].ToString(), out creditFactor))
        {
            throw new System.Exception("Could not parse credit factor from json data.");
        }
    }

    private static void CheckForZeroOrLessConversionFactor()
    {
        if (coinFactor <= 0)
        {
            throw new System.Exception("Coin conversion factor returned zero or less.");
        }

        if (creditFactor <= 0)
        {
            throw new System.Exception("Credit conversion factor returned zero or less.");
        }
    }

    /// <summary>
    /// Converts the item enegry to credit worth.
    /// </summary>
    /// <param name="itemEnergy">item energy</param>
    /// <returns>item price in credits according to item energy</returns>
    public static int GetCredits(int itemEnergy)
    {
        return ((int)Math.Ceiling((itemEnergy * creditFactor)));
    }

    /// <summary>
    /// Converts the item energy to coin worth.
    /// </summary>
    /// <param name="itemEnergy">item energy</param>
    /// <returns>item price in coins according to item energy</returns>
    public static int GetCoins(int itemEnergy)
    {
        return (int)(itemEnergy * coinFactor);
    }

    /// <summary>
    /// Get the resell value of an item according to its item enegry.
    /// </summary>
    /// <param name="itemEnergy">item enegry</param>
    /// <returns>item price in coins the user will get if item is sold</returns>
    public static int GetResellCoins(int itemEnergy)
    {
        return (int)(GetCoins(itemEnergy) * resellFactor);
    }
}
