using UnityEngine;
using UnityEditor;
using System.Collections;

public class AboutSocialPlayWindow : EditorWindow
{
    GUIStyle titleStyle = new GUIStyle();
    GUIStyle linkStyle = new GUIStyle();
    Texture2D Logo = null;
    Texture2D Logo2 = null;
    Rect logoPos = new Rect(5, 5, 128, 128);
    Rect lastRect;
    static AboutSocialPlayWindow window = null;


    // Add menu named "My Window" to the Window menu
    [MenuItem("Cloud Goods/About")]
    static void AboutWindow()
    {
        if (window == null)
        {
            window = ScriptableObject.CreateInstance(typeof(AboutSocialPlayWindow)) as AboutSocialPlayWindow;
            window.ShowUtility();
        }
        else
        {
            EditorWindow.FocusWindowIfItsOpen(typeof(AboutSocialPlayWindow));
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
        window.Logo2 = Resources.Load("Textures/SocialPlay_Logo2", typeof(Texture2D)) as Texture2D;

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
        GUILayout.Space(138);
        GUILayout.BeginVertical();
        GUILayout.Space(138);
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Label("Cloud Goods", titleStyle);
        GUILayout.Label("Version Number " + 1.0f);
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Label("The body of the About window to be filled later", GUILayout.Height(100));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Head over to the tutorials to get started");


        GUILayout.Label("tutorials", linkStyle);
        lastRect = GUILayoutUtility.GetLastRect();
        EditorGUIUtility.AddCursorRect(lastRect, MouseCursor.Link);
        if (lastRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseUp)
        {
            Application.OpenURL("http://developer.socialplay.com/#tutorials");
        }


        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Powered By:");
        GUILayout.Label("Socialplay", linkStyle);

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
