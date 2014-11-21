using UnityEngine;
using System.Collections;

public class OpenPremiumPanelOnLogin : MonoBehaviour {

    public PremiumCurrencyBundleStore premiumPanel;

    void OnEnable()
    {
        CloudGoods.OnRegisteredUserToSession += CloudGoods_OnRegisteredUserToSession;
    }

    void CloudGoods_OnRegisteredUserToSession(string obj)
    {
		Debug.Log ("User Registered to session: " + obj);
        premiumPanel.gameObject.SetActive(true);    
    }
}
