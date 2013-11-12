using UnityEngine;

class GUIStorePlatform : MonoBehaviour
{
    public static GUIStorePlatform Instance { get; set; }

    void Start()
    {
        Instance = this;
    }

    //public Texture2D CreditIcon = null;
    public string CreditIconName = "creditB_1";
    public string CreditName = "Credits";
    public Color MainColor = Color.gray;
    public Color MainColorFade = Color.gray;
    public Color SecondaryColor = Color.gray;
    public string currencyName = "USD";
    public int currencyMultiplier = 1;
    public GameObject currencyIcon;

}

