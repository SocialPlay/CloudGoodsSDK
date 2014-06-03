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

    //static public string iconPath = SocialPlaySettings.mainPath + "Icons/" + (EditorGUIUtility.isProSkin ? "Dark" : "Light") + "/";
    static public string iconPath = SocialPlaySettings.mainPath + "Icons/";

    static public Texture2D spLogo { get { if (mLogo == null) mLogo = AssetDatabase.LoadAssetAtPath(iconPath + "SocialPlay_Logo_small.png", typeof(Texture2D)) as Texture2D; return mLogo; } }
    //static public Texture2D iconList { get { if (mListIcon == null) mListIcon = AssetDatabase.LoadAssetAtPath(iconPath + "IconList.png", typeof(Texture2D)) as Texture2D; return mListIcon; } }
    //static public Texture2D iconThumb { get { if (mThumbIcon == null) mThumbIcon = AssetDatabase.LoadAssetAtPath(iconPath + "IconThumbnails.png", typeof(Texture2D)) as Texture2D; return mThumbIcon; } }
    //static public Texture2D iconSettings { get { if (mSettingsIcon == null) mSettingsIcon = AssetDatabase.LoadAssetAtPath(iconPath + "IconSettings.png", typeof(Texture2D)) as Texture2D; return mSettingsIcon; } }
    //static public Texture2D iconNew { get { if (mNewIcon == null) mNewIcon = AssetDatabase.LoadAssetAtPath(iconPath + "IconNew.png", typeof(Texture2D)) as Texture2D; return mNewIcon; } }

    static Texture2D mLogo;
    static Texture2D mListIcon;
    static Texture2D mThumbIcon;
    static Texture2D mSettingsIcon;
    static Texture2D mNewIcon;

    static string[] derivedDBNames;
    static string[] derivedSpawnerNames;

    //GUIContent myPageLink = new GUIContent("My Games", "Opens your games dashboard on the Player.IO web page");
    static GUIContent tutorialLink = new GUIContent("Tutorials", "Opens the Tutorial page in your browser.");
    static GUIContent devCenter = new GUIContent("Developer Portal", "Manage your items and recipes.");
    static GUIContent settingsContent = new GUIContent(" Settings", "Manage your game app settings");
    //static public GUIContent newContent = new GUIContent(" Add New", iconNew, "Adds new item");

    /// <summary>
    /// Load mSettings.
    /// </summary>

    static public SocialPlaySettings Get()
    {
        SocialPlaySettings mSettings = (SocialPlaySettings)Resources.Load("SocialPlaySettings", typeof(SocialPlaySettings));

        if (mSettings == null)
        {
            string path = SocialPlaySettings.mainPath + "Resources/";
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
        //mDB = target as Inventory;
        if (mInst == null) mInst = this;

        GUILayout.BeginVertical(GUILayout.Width(spLogo.height));
        GUILayout.BeginHorizontal();
        if (spLogo != null) GUILayout.Label(spLogo);
        GUIStyle title = EditorStyles.boldLabel;
        title.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginVertical();
        GUILayout.Label("Cloud Goods (v) " + SocialPlaySettings.VERSION, title);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(tutorialLink, GUILayout.MaxWidth(125)))
        {
            Application.OpenURL("http://developer.socialplay.com/#tutorials");
        }
        if (GUILayout.Button(devCenter, GUILayout.MaxWidth(125)))
        {
            Application.OpenURL("http://developer.socialplay.com/");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        //EditorGUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        DrawGUI();

        /*if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            if(mDBGo != null) EditorUtility.SetDirty(mDBGo);
        } */
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
        string url = EditorGUILayout.TextField("Url", mSettings.url);
        string bundlesUrl = EditorGUILayout.TextField("Bundles Url", mSettings.bundlesUrl);

        if (mSettings.appID != appId ||
            mSettings.appSecret != appSecret ||
            mSettings.url != url ||
            mSettings.bundlesUrl != bundlesUrl)
        {
            mSettings.appID = appId;
            mSettings.appSecret = appSecret;
            mSettings.url = url;
            mSettings.bundlesUrl = bundlesUrl;
        }

        EditorGUILayout.Separator();
    }

    #endregion
}