//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using System.IO;
//using System;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;


//namespace SocialPlay_Editor
//{
//    [ExecuteInEditMode]
//    public class AtlasGenerationWindow : EditorWindow
//    {

//        Vector2 imageSize = new Vector2(90, 90);
//        Texture2D runtimeAtlasTexture = new Texture2D(1024, 1024);
//        Rect[] runtimeAtlasRects;


//        UIAtlas runtimeAtlas;


//        bool isfileSaveFinished = false;
//        bool isWaitingForFile = false;


//        static string entireFilePath = "";
//        static string fileNamePath = "Resources/";
//        Texture2D atlasTextureFile;
//        static ServerAtlasInforamtion savedInfoFile;

//        string helpMessage = "Please Generate an atlas.";
//        MessageType messageDisaplayType = MessageType.Info;

//        string atlasName = "Atlas";

//        string AppID = "";


//        bool LoadWWW = false;
//        bool AppIDIsValid = false;
//        string generateButtonDescription = "Gets the item images from the database";

//        GameObject lastAtlasPrefab = null;

//        Texture2D defaultImage = null;

//        [MenuItem("Window/Item Atlas Generator")]
//        public static void ShowWindow()
//        {
//            EditorWindow.GetWindow(typeof(AtlasGenerationWindow));
//            string extraPath = "/";
//            if (!string.IsNullOrEmpty(fileNamePath))
//            {
//                extraPath += fileNamePath;
//            }
//            entireFilePath = Application.dataPath + extraPath;
//            UpdateSavedStats();
//        }

//        void Awake()
//        {
//            string extraPath = "/";
//            if (!string.IsNullOrEmpty(fileNamePath))
//            {
//                extraPath += fileNamePath;
//            }
//            entireFilePath = Application.dataPath + extraPath;
//        }



//        void Update()
//        {

//            if (EditorApplication.isPlaying && LoadWWW)
//            {
//                LoadWWW = false;
//                GetImages();
//            }
//            string extraPath = "/";
//            if (!string.IsNullOrEmpty(fileNamePath))
//            {
//                extraPath += fileNamePath;
//            }
//            entireFilePath = Application.dataPath + extraPath;

//            if (isWaitingForFile && isfileSaveFinished)
//            {
//                AssetDatabase.Refresh();
//                isWaitingForFile = false;
//                atlasTextureFile = Resources.Load(atlasName + "Texture") as Texture2D;
//                ConvertToNGUIAtlas();
//            }

//            try { new Guid(AppID); AppIDIsValid = true; }
//            catch { AppIDIsValid = false; }

//        }

//        void OnGUI()
//        {
//            GUI.color = Color.white;

//            GUILayout.BeginHorizontal();
//            GUILayout.FlexibleSpace();
//            AppID = EditorGUILayout.TextField(new GUIContent("Set App ID:", "Set the App ID that was assigned to your game from the website."), AppID, GUILayout.Width(450));
//            GUILayout.FlexibleSpace();
//            GUILayout.EndHorizontal();
//            GUILayout.Space(40);
//            GUILayout.BeginHorizontal();
//            atlasName = EditorGUILayout.TextField(new GUIContent("Pick atlas Email:", "Changing the Email will re-download all the images from the server"), atlasName, GUILayout.Width(Screen.width / 2));

//            if (GUILayout.Button(new GUIContent("Get Server Images", generateButtonDescription)) && AppIDIsValid) { EditorApplication.isPlaying = true; LoadWWW = true; }
//            if (GUILayout.Button(new GUIContent("Discard Current", "Disacards the data for the atlas with the selected Email"))) { Discard(); }

//            GUILayout.EndHorizontal();
//            defaultImage = EditorGUILayout.ObjectField("Default image", defaultImage, typeof(Texture2D), false) as Texture2D;


//            EditorGUILayout.HelpBox(helpMessage, messageDisaplayType);
//            if (lastAtlasPrefab != null)
//            {
//                if (GUILayout.Button(new GUIContent("Find Atlas", "Find the newly created Atlas in the project window")))
//                {
//                    EditorGUIUtility.PingObject(lastAtlasPrefab);
//                }
//            }
//        }

//        void Discard()
//        {
//            File.Delete(entireFilePath + atlasName + "Texture.png");
//            File.Delete(entireFilePath + atlasName + ".prefab");
//            File.Delete(entireFilePath + atlasName + "AtlasInforamtion.prefab");
//            File.Delete(entireFilePath + atlasName + "Mat.mat");
//            helpMessage = "Deleted all resources for " + atlasName + ".";
//            messageDisaplayType = MessageType.Info;
//            AssetDatabase.Refresh();
//        }

//        void GetImages()
//        {
//            try
//            {
//                defaultImage.Resize((int)imageSize.x, (int)imageSize.y);
//                defaultImage.Apply();
//            }
//            catch (Exception e)
//            {
//                helpMessage = e.Message;
//                messageDisaplayType = MessageType.Error;
//                return;
//            }

//            SettingUpResources();
//            helpMessage = "Loading Images from Web.";
//            messageDisaplayType = MessageType.Info;

//            EditorWWWPacket.Creat("https://SocialPlayWebService.azurewebsites.net/cloudgoods/cloudgoodsservice.svc/GetAppAtlas?AppID=" + AppID, RecivedImageInfo);
//        }

//        private void RecivedImageInfo(string info)
//        {
//            string data = "";
//            data = JToken.Parse(info).ToString();
//            savedInfoFile.atlasData = JsonConvert.DeserializeObject<Atlases>(data);
//            savedInfoFile.currentlyRecivedImages.Clear();


//            foreach (AtlasData item in savedInfoFile.atlasData)
//            {

//                ItemImageInformation itemImage = new ItemImageInformation();

//                var tex = new Texture2D((int)imageSize.x, (int)imageSize.y);
//                if (item.Image != null)
//                {
//                    tex.LoadImage(item.Image);
//                }
//                else
//                {
//                    tex = defaultImage;
//                }
//                itemImage.image = tex;
//                itemImage.imageName = item.ID.ToString();
//                itemImage.itemName = item.Name;
//                itemImage.baseItemID = item.ID;

//                savedInfoFile.currentlyRecivedImages.Add(itemImage);
//            }
//            GotImages();
//        }

//        private void SettingUpResources()
//        {
//            string extraPath = "/";
//            if (!string.IsNullOrEmpty(fileNamePath))
//            {
//                extraPath += fileNamePath;
//            }
//            entireFilePath = Application.dataPath + extraPath;

//            helpMessage = "Generating Image: Setting up resources";
//            GameObject savedInfoObject = (Resources.Load(atlasName + "-AtlasInforamtion") as GameObject);

//            if (savedInfoObject == null)
//            {
//                GameObject go = new GameObject();
//                try
//                {
//                    go.AddComponent<ServerAtlasInforamtion>();
//                    if (!Directory.Exists(entireFilePath))
//                    {
//                        Directory.CreateDirectory(entireFilePath);
//                    }
//                    PrefabUtility.CreatePrefab("Assets/" + fileNamePath + atlasName + "-AtlasInforamtion.prefab", go, ReplacePrefabOptions.ReplaceNameBased);

//                    savedInfoFile = (Resources.Load(atlasName + "-AtlasInforamtion") as GameObject).GetComponent<ServerAtlasInforamtion>();
//                    UpdateSavedStats();
//                }
//                finally
//                {
//                    DestroyImmediate(go);
//                }
//            }
//            else
//            {
//                savedInfoFile = savedInfoObject.GetComponent<ServerAtlasInforamtion>();
//                UpdateSavedStats();
//            }
//        }

//        static void UpdateSavedStats()
//        {
//            //if (savedInfoFile == null)
//            //{
//            //    GameObject savedInfoObject = (Resources.Load(atlasName + "-AtlasInforamtion") as GameObject);
//            //    if (savedInfoObject == null) return;
//            //    savedInfoFile = savedInfoObject.GetComponent<ServerAtlasInforamtion>();
//            //}

//            //if (savedInfoFile == null) return;

//            //if (savedInfoFile.LastAppID != AppID && AppID != "")
//            //{
//            //    savedInfoFile.LastAppID = AppID;
//            //}
//            //else if (AppID == "" && savedInfoFile.LastAppID != "")
//            //{
//            //    AppID = savedInfoFile.LastAppID;
//            //}
//            //if (savedInfoFile.LastDefaultImage != defaultImage && defaultImage != null)
//            //{
//            //    savedInfoFile.LastDefaultImage = defaultImage;
//            //}
//            //else if (defaultImage == null && savedInfoFile.LastDefaultImage != null)
//            //{
//            //    defaultImage = savedInfoFile.LastDefaultImage;
//            //}
//        }

//        void GotImages()
//        {
//            PackTextures();
//        }

//        void PackTextures()
//        {
//            helpMessage = "Packing Textures into single File.";
//            messageDisaplayType = MessageType.Info;

//            Texture2D[] texturesToPack = new Texture2D[savedInfoFile.currentlyRecivedImages.Count];
//            for (int i = 0; i < savedInfoFile.currentlyRecivedImages.Count; i++)
//            {
//                texturesToPack[i] = savedInfoFile.currentlyRecivedImages[i].image;
//            }
//            runtimeAtlasTexture = new Texture2D(1024, 1024);
//            runtimeAtlasRects = runtimeAtlasTexture.PackTextures(texturesToPack, 1, 4096);

//            for (int i = 0; i < savedInfoFile.currentlyRecivedImages.Count; i++)
//            {
//                savedInfoFile.currentlyRecivedImages[i].atlasRect = runtimeAtlasRects[i];
//            }
//            SaveTextureToFile(runtimeAtlasTexture, atlasName + "Texture.png");
//        }

//        void GetInfoFromResource()
//        {
//            savedInfoFile.currentlyRecivedImages.Clear();
//            MockItemImageDatas imageDatas = (Resources.Load("ItemImageDatas") as GameObject).GetComponent<MockItemImageDatas>();
//            foreach (var item in imageDatas.infos)
//            {
//                savedInfoFile.currentlyRecivedImages.Add(item);
//            }
//        }

//        void ConvertToNGUIAtlas()
//        {
//            helpMessage = "Creating NGUI Atlas.";
//            messageDisaplayType = MessageType.Info;

//            GameObject go = new GameObject();
//            try
//            {
//                runtimeAtlas = go.AddComponent<UIAtlas>() as UIAtlas;
//                Material mat = new Material(Shader.Find("Unlit/Transparent"));

//                runtimeAtlas.spriteMaterial = mat;
//                mat.mainTexture = atlasTextureFile;

//                runtimeAtlas.coordinates = UIAtlas.Coordinates.TexCoords;
//                runtimeAtlas.spriteList = new List<UIAtlas.Sprite>();
//                runtimeAtlas.spriteList.Clear();

//                foreach (var item in savedInfoFile.currentlyRecivedImages)
//                {
//                    UIAtlas.Sprite sprit = new UIAtlas.Sprite();
//                    sprit.outer = item.atlasRect;
//                    sprit.inner = item.atlasRect;
//                    sprit.Email = item.imageName;
//                    sprit.paddingLeft = 0;
//                    sprit.paddingRight = 0;
//                    runtimeAtlas.spriteList.Add(sprit);
//                }

//                AssetDatabase.CreateAsset(mat, "Assets/" + fileNamePath + atlasName + "Mat.mat");
//                GameObject atlasObject = PrefabUtility.CreatePrefab("Assets/" + fileNamePath + atlasName + ".prefab", go, ReplacePrefabOptions.ReplaceNameBased) as GameObject;
//                lastAtlasPrefab = atlasObject;
//                EditorGUIUtility.PingObject(atlasObject);
//                UnityEditor.EditorUtility.SetDirty(atlasObject);
//            }
//            finally
//            {
//                DestroyImmediate(go);
//            }

//            helpMessage = "Atlas is Created at :" + "Assets/" + fileNamePath + atlasName + ".prefab";
//            messageDisaplayType = MessageType.Info;
//            EditorApplication.isPlaying = false;
//        }

//        void SaveTextureToFile(Texture2D texture, string fileName)
//        {
//            helpMessage = "Saving texture file.";
//            messageDisaplayType = MessageType.Info;

//            isWaitingForFile = true;
//            isfileSaveFinished = false;
//            var bytes = texture.EncodeToPNG();

//            var file = File.Open(entireFilePath + fileName, FileMode.Create);

//            file.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(FinishSavingFile), null);
//            file.Close();

//        }

//        void FinishSavingFile(IAsyncResult result)
//        {
//            if (result.IsCompleted)
//            {
//                isfileSaveFinished = true;
//            }
//        }

//    }


//    public class Atlases : List<AtlasData> { }

//    public class AtlasData
//    {
//        public int ID;
//        public string Name;
//        public byte[] Image;
//    }
//}