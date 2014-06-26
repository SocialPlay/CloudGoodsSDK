using UnityEngine;
using System.Collections;

public class iOSMessage : MonoBehaviour {

	public UILabel label;
	
	// Use this for initialization
	void Awake () {
		iOSConnect.onReceivedMessage += OnReceivedIOSMessage;
	}

	void OnReceivedIOSMessage(string message)
	{
		label.text = message;
	}

}
