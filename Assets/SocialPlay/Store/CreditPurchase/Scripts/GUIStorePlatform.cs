using UnityEngine;

class GUIStorePlatform : MonoBehaviour
{
    public static GUIStorePlatform Instance { get; set; }

    void Start()
    {
        Instance = this;
    }

    //public Texture2D CreditIcon = null;
    public string PaidCurrencyIconName = "creditB_1";
    public string PaidCurrencyName = "Credits";
    public Color MainColor = Color.gray;
    public Color MainColorFade = Color.gray;
    public Color SecondaryColor = Color.gray;
    public string currencyName = "USD";
    public int currencyMultiplier = 1;
    //public GameObject currencyIcon;

}

