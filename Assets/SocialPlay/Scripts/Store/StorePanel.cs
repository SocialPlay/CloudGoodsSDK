using UnityEngine;
using System.Collections;

public class StorePanel : MonoBehaviour {

    public GameObject homeButton;

    public GameObject homePanel;
    public GameObject storePanel;

	// Use this for initialization
	void Start () {
        if(homeButton != null)
            UIEventListener.Get(homeButton).onClick += OnHomeButtonClick;
	}

    void OnHomeButtonClick(GameObject go)
    {
        if(homePanel != null)
            homePanel.SetActive(true);

        storePanel.SetActive(false);
    }
}
