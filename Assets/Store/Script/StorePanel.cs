using UnityEngine;
using System.Collections;

public class StorePanel : MonoBehaviour {

    public GameObject homeButton;

    public GameObject homePanel;
    public GameObject storePanel;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(homeButton).onClick += OnHomeButtonClick;
	}

    void OnHomeButtonClick(GameObject go)
    {
        homePanel.SetActive(true);
        storePanel.SetActive(false);
    }
}
