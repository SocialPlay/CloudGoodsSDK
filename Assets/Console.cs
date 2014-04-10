using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour
{

    static List<string> logs = new List<string>();

    static Rect screeSpace = new Rect(0, 0, Screen.width, Screen.height);

    // Use this for initialization
    void Start()
    {
        logs = new List<string>();
    }


    public static void WriteLine(string line)
    {
        logs.Add(line);
        Debug.Log(line);
    }

    void OnGUI()
    {
        GUILayout.BeginArea(screeSpace);
        List<string> five = logs;
        if (five.Count > 5)
        {
            five = logs.GetRange(logs.Count - 5, logs.Count);
        }

        foreach (string log in five)
        {
            GUILayout.Label(log,GUI.skin.textArea);
        }
        GUILayout.EndArea();
    }

}
