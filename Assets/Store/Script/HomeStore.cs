using UnityEngine;
using System;
using System.Collections;

public class HomeStore : MonoBehaviour {

    public GameObject storeButton;
    public GameObject newItemsButton;

    public GameObject storePanel;
    public GameObject homePanel;

    DateTime timeAtStart;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(storeButton).onClick += OnStoreButtonClick;
        UIEventListener.Get(newItemsButton).onClick += OnNewItemsButtonClick;

        timeAtStart = DateTime.Now;
	}

    void OnStoreButtonClick(GameObject button)
    {
        storePanel.SetActive(true);
        homePanel.SetActive(false);
    }

    void OnNewItemsButtonClick(GameObject button)
    {
        DateTime dateNow = DateTime.Now;

        Debug.Log("Time Now: " + dateNow + "\nTime at Start: " + timeAtStart);

        TimeSpan timeSpan = dateNow.Subtract(timeAtStart);

        Debug.Log("Time Difference: " + timeSpan);


    }
}
