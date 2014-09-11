using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// Inspector class used to edit Inventory Databases.
/// </summary>

[CustomEditor(typeof(SocialPlaySettings))]
public class SocialPlaySettingsInspector : Editor
{
    [MenuItem("Cloud Goods/Settings", false, 0)]
    static void SelectSettings()
    {
        Selection.activeObject = SocialPlaySettingsInspector.Get();
    }

    static SocialPlaySettingsInspector mInst;
    static SocialPlaySettings mSettings;
    static SocialPlaySettings.ScreenType screen { get { return mSettings.screen; } set { mSettings.screen = value; } }
    static Vector2 scrollPos;

    static public string iconPath = SocialPlaySettings.mainPath + "Icons/";

    static public Texture2D spLogo { get { if (mLogo == null) mLogo = AssetDatabase.LoadAssetAtPath(iconPath + "SocialPlay_Logo_small.png", typeof(Texture2D)) as Texture2D; return mLogo; } }

    static Texture2D mLogo;
    static Texture2D mListIcon;
    static Texture2D mThumbIcon;
    static Texture2D mSettingsIcon;
    static Texture2D mNewIcon;

    static string[] derivedDBNames;
    static string[] derivedSpawnerNames;

    static GUIContent devCenter = new GUIContent("Developer Portal", "Manage your items and players");
    static GUIContent settingsContent = new GUIContent(" Settings", "Manage your game app settings");

    /// <summary>
    /// Load mSettings.
    /// </summary>

    static public SocialPlaySettings Get()
    {
        SocialPlaySettings mSettings = (SocialPlaySettings)Resources.Load("SocialPlaySettings", typeof(SocialPlaySettings));

        if (mSettings == null)
        {
            string path = "Assets/Resources/";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
            mSettings = (SocialPlaySettings)ScriptableObject.CreateInstance("SocialPlaySettings");
            AssetDatabase.CreateAsset(mSettings, path + "SocialPlaySettings.asset");
        }
        return mSettings;
    }

    void OnEnable()
    {
        if (mSettings == null)
            mSettings = Get();
    }

    /// <summary>
    /// Draw the inspector widget.
    /// </summary>

    public override void OnInspectorGUI()
    {
        if (mInst == null) mInst = this;

        GUILayout.BeginVertical(GUILayout.Width(spLogo.height));
        GUILayout.BeginHorizontal();
        if (spLogo != null) GUILayout.Label(spLogo);
        GUIStyle title = EditorStyles.boldLabel;
        title.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginVertical();
        GUILayout.Label("Cloud Goods (v) " + SocialPlaySettings.VERSION, title);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(devCenter, GUILayout.MaxWidth(125)))
        {
            Application.OpenURL("http://developer.socialplay.com/");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        DrawGUI();
    }

    static public void DrawGUI()
    {
        GUI.SetNextControlName("empty");
        GUI.Button(new Rect(0, 0, 0, 0), "", GUIStyle.none);

        GUIStyle activeTabStyle = new GUIStyle("ButtonMid");
        GUIStyle activeTabStyleLeft = new GUIStyle("ButtonLeft");
        GUIStyle activeTabStyleRight = new GUIStyle("ButtonRight");

        GUIStyle inactiveTabStyle = new GUIStyle(activeTabStyle);
        GUIStyle inactiveTabStyleLeft = new GUIStyle(activeTabStyleLeft);
        GUIStyle inactiveTabStyleRight = new GUIStyle(activeTabStyleRight);

        activeTabStyle.normal = activeTabStyle.active;
        activeTabStyleLeft.normal = activeTabStyleLeft.active;
        activeTabStyleRight.normal = activeTabStyleRight.active;

        GUILayout.BeginHorizontal();
        for (int i = 0, imax = (int)SocialPlaySettings.ScreenType._LastDoNotUse; i < imax; i++)
        {
            GUIStyle active = activeTabStyleLeft;
            if (i > 2) active = activeTabStyle;
            if (i == (int)SocialPlaySettings.ScreenType._LastDoNotUse - 1) active = activeTabStyleRight;

            GUIStyle inactive = inactiveTabStyleLeft;
            if (i > 2) inactive = inactiveTabStyle;
            if (i == (int)SocialPlaySettings.ScreenType._LastDoNotUse - 1) inactive = inactiveTabStyleRight;

            GUI.backgroundColor = screen == (SocialPlaySettings.ScreenType)i ? Color.cyan : Color.white;

            GUIContent mName = new GUIContent(((SocialPlaySettings.ScreenType)i).ToString());
            if (GUILayout.Button(((SocialPlaySettings.ScreenType)i == SocialPlaySettings.ScreenType.Settings) ? settingsContent : mName, screen == (SocialPlaySettings.ScreenType)i ? active : inactive))
            {
                GUI.FocusControl("empty");
                screen = (SocialPlaySettings.ScreenType)i;
            }
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndHorizontal();

        switch (screen)
        {
            case SocialPlaySettings.ScreenType.Settings:
                DrawSettingsGUI();
                break;
            case SocialPlaySettings.ScreenType.About:
                DrawAboutGUI();
                break;
        }
    }

    #region Settings GUI

    static void DrawSettingsGUI()
    {
        NGUIEditorTools.SetLabelWidth(120f);
        GUILayout.Label("Settings", "BoldLabel");

        string appId = EditorGUILayout.TextField("App ID", mSettings.appID);
        string appSecret = EditorGUILayout.TextField("App Secret", mSettings.appSecret);

        if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appSecret))
        {
            EditorGUILayout.HelpBox("Go To http://developer.socialplay.com to get your AppID and AppSecret", MessageType.Warning);
        }

        GUILayout.Label("Android", "BoldLabel");
        string androidKey = EditorGUILayout.TextField("Key", mSettings.androidKey);

        GUILayout.Label("Defaults", "BoldLabel");
        Texture2D defaultTexture = EditorGUILayout.ObjectField("Default Texture", mSettings.defaultTexture, typeof(Texture2D), false) as Texture2D;
        GameObject defaultItemDrop = EditorGUILayout.ObjectField("Default Item Drop", mSettings.defaultItemDrop, typeof(GameObject), false) as GameObject;

        EditorGUILayout.Separator();

        if (mSettings.appID != appId ||
            mSettings.appSecret != appSecret ||
            mSettings.defaultTexture != defaultTexture ||
            mSettings.defaultItemDrop != defaultItemDrop ||
            mSettings.androidKey != androidKey)
        {
            mSettings.appID = appId;
            mSettings.appSecret = appSecret;
            mSettings.defaultTexture = defaultTexture;
            mSettings.defaultItemDrop = defaultItemDrop;
            mSettings.androidKey = androidKey;
            NGUIEditorTools.RegisterUndo("Social Play Settings", mSettings);
        }


        EditorGUILayout.Separator();
    }
    #endregion

    static void DrawAboutGUI()
    {
        GUIStyle linkStyle = new GUIStyle();
        linkStyle = new GUIStyle(GUI.skin.label);
        linkStyle.normal.textColor = Color.blue;

        Rect lastRect;

        GUILayout.BeginVertical();
        GUILayout.Space(5);
        GUILayout.EndVertical();
        GUILayout.Label("Cloud Goods: Your complete virtual goods monetization");
        GUILayout.Label("solution.", GUILayout.Height(30));
        GUILayout.Label("Need assistance or want to report a bug? Feel free to");
        GUILayout.Label("contact us at support@socialplay.com", GUILayout.Height(40));
        GUILayout.Label("For more information, visit:");
        GUILayout.Label("developer.socialplay.com", linkStyle);
        lastRect = GUILayoutUtility.GetLastRect();
        EditorGUIUtility.AddCursorRect(lastRect, MouseCursor.Link);
        if (lastRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseUp)
        {
            Application.OpenURL("http://developer.socialplay.com/");
        }

        GUILayout.Label("www.socialplay.com", linkStyle, GUILayout.Height(40));

        lastRect = GUILayoutUtility.GetLastRect();
        EditorGUIUtility.AddCursorRect(lastRect, MouseCursor.Link);
        if (lastRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseUp)
        {
            Application.OpenURL("http://www.socialplay.com/");
        }

        GUILayout.Label("(c) 2014 SocialPlay Inc.");
    }
}