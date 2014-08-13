using UnityEngine;
using System.Collections;

public class ClosePremiumCurrencyPurchase : MonoBehaviour
{

    public GameObject PaidCurrencyPurchasePanel;

    void OnEnable()
    {
        UIEventListener.Get(gameObject).onClick += ClosePaidCurrencyPurchaseWindow;
    }

    void OnDisable()
    {
        UIEventListener.Get(gameObject).onClick -= ClosePaidCurrencyPurchaseWindow;
    }

    void ClosePaidCurrencyPurchaseWindow(GameObject closeButton)
    {
        PaidCurrencyPurchasePanel.SetActive(false);
    }
}
