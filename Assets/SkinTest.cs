using UnityEngine;
using System.Collections;

public class SkinTest : MonoBehaviour {

    public GUISkin style;

    void OnGUI()
    {
        GUILayout.Button("Button", style.label);
        GUILayout.Label("Label", style.label);
    }
}
