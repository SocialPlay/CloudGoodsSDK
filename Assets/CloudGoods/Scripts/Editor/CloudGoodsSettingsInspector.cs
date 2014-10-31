using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// Inspector class used to edit Inventory Databases.
/// </summary>

[CustomEditor(typeof(CloudGoodsSettings))]
public class CloudGoodsSettingsInspector : Editor
{
    [MenuItem("Cloud Goods/Settings", false, 0)]
    static void SelectSettings()
    {
        Selection.activeObject = Get();
    }

    static CloudGoodsSettings.ScreenType screen;// { get { return .screen; } set { mSettings.screen = value; } }
    static Vector2 scrollPos;

    static public string iconPath = CloudGoodsSettings.mainPath + "Icons/";

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
    static CloudGoodsSettings Get()
    {
        CloudGoodsSettings mSettings = CloudGoodsSettings.instance;

        if (mSettings == null)
        {
            Debug.Log("Setting is not found");
            string path = "Assets/Resources/";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
            mSettings = (CloudGoodsSettings)ScriptableObject.CreateInstance("CloudGoodsSettings");
            AssetDatabase.CreateAsset(mSettings, path + "CloudGoodsSettings.asset");
        }
        return mSettings;
    }

    public override void OnInspectorGUI()
    {     
        GUILayout.BeginVertical(GUILayout.Width(spLogo.height));
        GUILayout.BeginHorizontal();
        if (spLogo != null) GUILayout.Label(spLogo);
        GUIStyle title = EditorStyles.boldLabel;
        title.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginVertical();
        GUILayout.Label("Cloud Goods (v) " + CloudGoodsSettings.VERSION, title);

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

    void DrawGUI()
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
        for (int i = 0, imax = (int)CloudGoodsSettings.ScreenType._LastDoNotUse; i < imax; i++)
        {
            GUIStyle active = activeTabStyleLeft;
            if (i > 2) active = activeTabStyle;
            if (i == (int)CloudGoodsSettings.ScreenType._LastDoNotUse - 1) active = activeTabStyleRight;

            GUIStyle inactive = inactiveTabStyleLeft;
            if (i > 2) inactive = inactiveTabStyle;
            if (i == (int)CloudGoodsSettings.ScreenType._LastDoNotUse - 1) inactive = inactiveTabStyleRight;

            GUI.backgroundColor = screen == (CloudGoodsSettings.ScreenType)i ? Color.cyan : Color.white;

            GUIContent mName = new GUIContent(((CloudGoodsSettings.ScreenType)i).ToString());
            if (GUILayout.Button(((CloudGoodsSettings.ScreenType)i == CloudGoodsSettings.ScreenType.Settings) ? settingsContent : mName, screen == (CloudGoodsSettings.ScreenType)i ? active : inactive))
            {
                GUI.FocusControl("empty");
                screen = (CloudGoodsSettings.ScreenType)i;
            }
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndHorizontal();

        switch (screen)
        {
            case CloudGoodsSettings.ScreenType.Settings:
                DrawSettingsGUI();
                break;
            case CloudGoodsSettings.ScreenType.About:
                DrawAboutGUI();
                break;
        }
    }

    #region Settings GUI

    void DrawSettingsGUI()
    {
        serializedObject.Update();
        // GUILayout.Label("Settings", "BoldLabel");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("appID"), new GUIContent("App ID"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("appSecret"), new GUIContent("App Secret"));

        if (string.IsNullOrEmpty(serializedObject.FindProperty("appID").stringValue) || string.IsNullOrEmpty(serializedObject.FindProperty("appSecret").stringValue))
        {
            EditorGUILayout.HelpBox("Go To http://developer.socialplay.com to get your AppID and AppSecret", MessageType.Warning);
        }
        EditorGUILayout.Separator();
        GUILayout.Label("Android", "BoldLabel");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("androidKey"), new GUIContent("Key"));

        EditorGUILayout.Separator();
        GUILayout.Label("Defaults", "BoldLabel");
        serializedObject.FindProperty("defaultTexture").objectReferenceValue = EditorGUILayout.ObjectField("Default Texture", serializedObject.FindProperty("defaultTexture").objectReferenceValue, typeof(Texture2D), false) as Texture2D;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultUIItem"), new GUIContent("Default UI Item", "Default UI Item"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultItemDrop"), new GUIContent("Default Drop Prefab", "The Item that will be droped if no asset is found for the item spawning into the world"));
        
        CloudGoodsSettings.BuildPlatformType type = (CloudGoodsSettings.BuildPlatformType)serializedObject.FindProperty("buildPlatform").enumValueIndex;
        serializedObject.FindProperty("buildPlatform").enumValueIndex = (int)(CloudGoodsSettings.BuildPlatformType)EditorGUILayout.EnumPopup("Platform build Type", type); 
        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    void DrawAboutGUI()
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