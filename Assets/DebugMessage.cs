using UnityEngine;
using System.Collections;

public class DebugMessage : MonoBehaviour {

	public UILabel label;

	// Use this for initialization
	void Awake () {
		iOSConnect.onReceivedMessage += OnIOSMessage;
	}
	
	void OnIOSMessage(string message){
		label.text = message;
	}
}
