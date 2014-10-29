using UnityEngine;
using UnityEditor;
using System.Collections;

public class AboutCloudGoodsWindow : EditorWindow
{
    GUIStyle titleStyle = new GUIStyle();
    GUIStyle linkStyle = new GUIStyle();
    Texture2D Logo = null;
    //Texture2D Logo2 = null;
    Rect logoPos = new Rect(5, 5, 128, 128);
    Rect lastRect;
    static AboutCloudGoodsWindow window = null;


    // Add menu named "My Window" to the Window menu
    [MenuItem("Cloud Goods/About")]
    static void AboutWindow()
    {
        if (window == null)
        {
            window = ScriptableObject.CreateInstance(typeof(AboutCloudGoodsWindow)) as AboutCloudGoodsWindow;
            window.ShowUtility();
        }
        else
        {
            EditorWindow.FocusWindowIfItsOpen(typeof(AboutCloudGoodsWindow));
        }
        Init();
    }

    static void Init()
    {
        window.title = "About Cloud Goods";

        window.minSize = new Vector2(600, 350);
        window.maxSize = window.minSize;
        window.titleStyle.fontSize = 35;
        window.titleStyle.fontStyle = FontStyle.Bold;
        window.titleStyle.normal.background = null;
        window.Logo = Resources.Load("Textures/SocialPlay_Logo", typeof(Texture2D)) as Texture2D;
        //window.Logo2 = Resources.Load("Textures/SocialPlay_Logo2", typeof(Texture2D)) as Texture2D;

    }

    void OnGUI()
    {
        if (window == null)
        {
            this.Close();
            return;
        }

        linkStyle = new GUIStyle(GUI.skin.label);
        linkStyle.normal.textColor = Color.blue;

        GUI.DrawTexture(window.logoPos, Logo);
        GUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.BeginVertical();
        GUILayout.Space(100);
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Label("Cloud Goods", titleStyle);
        GUILayout.Label("Version 1.0");
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Label("Cloud Goods is a Virtual Goods and Virtual Economy service developed by SocialPlay.  Use Cloud ");
        GUILayout.Label("Goods to help you manage your players, items, and sales in real-time right from the web.  There's no");
        GUILayout.Label("need to republish your game, changes you make from the Developer Portal effect your games in real-");
        GUILayout.Label("time.", GUILayout.Height(40));
        GUILayout.Label("Start selling in minutes with the included IOS and Android in-app purchasing plugins", GUILayout.Height(40));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Create an account at");
        
        GUILayout.Label("developers.socialplay.com", linkStyle);
        lastRect = GUILayoutUtility.GetLastRect();
        EditorGUIUtility.AddCursorRect(lastRect, MouseCursor.Link);
        if (lastRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseUp)
        {
            Application.OpenURL("http://developer.socialplay.com/");
        }
        GUILayout.Label("and check out the tutorials to get started. For");
        
        
        GUILayout.EndHorizontal();

        GUILayout.Label("support or bug reports, feel free to contact us at support@socialplay.com");

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.Label("(c) 2014 SocialPlay Inc.");

        lastRect = GUILayoutUtility.GetLastRect();
        EditorGUIUtility.AddCursorRect(lastRect, MouseCursor.Link);
        if (lastRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseUp)
        {
            Application.OpenURL("http://developer.socialplay.com/");
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

    }

}
