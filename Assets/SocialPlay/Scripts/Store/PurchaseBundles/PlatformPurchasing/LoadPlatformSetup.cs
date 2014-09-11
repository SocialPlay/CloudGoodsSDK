using UnityEngine;
using System.Collections;

public delegate void PlatformSetupLoaded(GameObject platformSetup);

public class LoadPlatformSetup : MonoBehaviour {

    public static event PlatformSetupLoaded platformSetupLoaded;

    public GameObject purchaseCreditsButton;
    public GameObject PaidCurrencyMessagePanel;

    public int platformID;

    public GameObject socialPlayPlatformSetupPrefab;
    public GameObject kongregatePlatformSetupPrefab;

	// Use this for initialization
	void Start () {
        Debug.Log("Load platform start");

        if (Application.isEditor)
            loadSetupForPlatformFromPlatformID(platformID);
        //else
        //    loadSetupForPlatformFromPlatformID(GatherGameInfo.allInfo.platformID);

        CheckForSocialplayPlatform();
	}

    void CheckForSocialplayPlatform()
    {
        //if (GatherGameInfo.allInfo.platformType == "Portal")
        //{
        //    purchaseCreditsButton.SetActive(false);
        //    PaidCurrencyMessagePanel.SetActive(false);
        //}
    }

    void loadSetupForPlatformFromPlatformID(int platformID)
    {
        Debug.Log("Loadplatform set up: " + platformID);

        switch (platformID)
        {
            case 1:
                platformSetupLoaded((GameObject)GameObject.Instantiate(socialPlayPlatformSetupPrefab));
                purchaseCreditsButton.SetActive(true);
                PaidCurrencyMessagePanel.SetActive(false);
                break;
            case 4:
                platformSetupLoaded((GameObject)GameObject.Instantiate(kongregatePlatformSetupPrefab));
                purchaseCreditsButton.SetActive(true);
                PaidCurrencyMessagePanel.SetActive(false);
                break;
            default:
                platformSetupLoaded((GameObject)GameObject.Instantiate(socialPlayPlatformSetupPrefab));
                purchaseCreditsButton.SetActive(true);
                PaidCurrencyMessagePanel.SetActive(false);
                break;
        }

    }
}
