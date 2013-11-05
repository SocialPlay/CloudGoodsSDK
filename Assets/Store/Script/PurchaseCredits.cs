using UnityEngine;
using System.Collections;

public class PurchaseCredits : MonoBehaviour {

    public GameObject creditPanel;

    public GameObject closeCreditPanelButton;

    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(gameObject).onClick += OnPurchaseButtonClick;
        UIEventListener.Get(closeCreditPanelButton).onClick += OnCloseCreditButton;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCloseCreditButton(GameObject button)
    {
        creditPanel.SetActive(false);
    }

    void OnPurchaseButtonClick(GameObject button)
    {
        creditPanel.SetActive(true);
    }
}
