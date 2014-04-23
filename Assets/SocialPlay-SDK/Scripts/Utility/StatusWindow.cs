using UnityEngine;
using System.Collections;

public class StatusWindow : MonoBehaviour {

    public GameObject statusWindowObj;

    public UILabel titleLabel;
    public UILabel statusMessage;

    public void SetStatusMessage(string title, string mainMessage)
    {
        Debug.Log("Set status msg");
        statusWindowObj.SetActive(true);

        titleLabel.text = title;
        statusMessage.text = mainMessage;
    }
	
    public void CloseWindow()
    {
        statusWindowObj.SetActive(false);
    }
}
