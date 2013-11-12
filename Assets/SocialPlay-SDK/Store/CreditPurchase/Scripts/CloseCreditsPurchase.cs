using UnityEngine;
using System.Collections;

public class CloseCreditsPurchase : MonoBehaviour {

    public GameObject creditsPurchasePanel;

    void OnEnable()
    {
        UIEventListener.Get(gameObject).onClick += CloseCreditsPurchaseWindow;
    }

    void OnDisable()
    {
        UIEventListener.Get(gameObject).onClick -= CloseCreditsPurchaseWindow;
    }

    void CloseCreditsPurchaseWindow(GameObject closeButton)
    {
        creditsPurchasePanel.SetActiveRecursively(false);
    }
}
