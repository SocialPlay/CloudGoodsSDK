using System;
using System.Collections.Generic;
using UnityEngine;
using SocialPlay.Bundles;

namespace SocialPlay.Bundles
{
    public class BundleSystem : MonoBehaviour
    {
        private static AssetBundleTracker assetBundleTracker;

        static public Dictionary<string, string> AssetGroupRefences = new Dictionary<string, string>();

        static public Dictionary<string, Dictionary<string, AssetObject>> AssetGroups = new Dictionary<string, Dictionary<string, AssetObject>>();

        public class AssetObject
        {
            public event Action<UnityEngine.Object> callbacks;

            public UnityEngine.Object asset = null;

            public AssetBundle bundle = null;

            public bool isComplete = false;

            public void CallEvent()
            {
                if (callbacks != null)
                {
                    callbacks(asset);
                }
            }
        }

        public static bool isInitialized = false;

        public static void InitializeTracker()
        {
            ExceptionIfURLNotSet();

            GameObject BundleObject = UnityEngine.GameObject.Find("AssetBundleTracker");
            if (BundleObject != null)
            {
                assetBundleTracker = BundleObject.GetComponent<AssetBundleTracker>();
            }
            if (assetBundleTracker == null)
            {
                GameObject tmp = new GameObject("AssetBundleTracker");
                assetBundleTracker = tmp.AddComponent<AssetBundleTracker>();
            }
            BundleSystem.isInitialized = true;
        }

        static public void Get(string assetName, Action<UnityEngine.Object> callback, string groupName = "Default", bool isAlwaysCallback = false)
        {

            if (isAlreadyRequested(assetName))
            {
                CheckIfAssetExistsInOtherGroup(assetName, groupName);

                if (isAlreadyLoaded(assetName))
                {
                    AssetObject assetObject = AssetGroups[groupName][assetName];
                    callback(assetObject.asset);
                }
                else
                {
                    AssetObject assetObject = AssetGroups[groupName][assetName];
                    assetObject.callbacks += callback;
                }
            }
            else
            {
                if (assetBundleTracker != null)
                {
                    assetBundleTracker.DisplayLoadingScreenOverlay(groupName);
                }
                DownloadAssetFromServerOrCache(assetName, callback, groupName, isAlwaysCallback);
            }
        }

        private static void CheckIfAssetExistsInOtherGroup(string assetName, string groupName)
        {
            if (AssetGroupRefences[assetName] != groupName)
            {
                throw new Exception("Unable To get new asset: " + assetName + " under group Email: " + groupName + " because it has already been used for group: " + AssetGroupRefences[assetName]);
            }
        }

        private static void DownloadAssetFromServerOrCache(string assetName, Action<UnityEngine.Object> callback, string groupName, bool isAlwaysCallback)
        {
            if (assetBundleTracker != null)
            {
                assetBundleTracker.addTotalDownloadCount(groupName);
            }
            ExceptionIfURLNotSet();
            ExceptionIfAssetNameNotSet(assetName);
            AddToReferenceList(assetName, callback, groupName);
            Debug.Log("DownloadAssetFromServerOrCache : " + assetName);
            AssetBundleGetter.LoadFromCacheOrDownload(assetName, 1, OnRecievedAssetFromServerOrCache, isAlwaysCallback);
        }

        static void AddToReferenceList(string assetName, Action<UnityEngine.Object> callback, string groupName)
        {

            AssetObject packet = new AssetObject();
            packet.callbacks += callback;

            AssetGroupRefences.Add(assetName, groupName);

            AddToAssetGroups(assetName, groupName, packet);
        }

        private static void AddToAssetGroups(string assetName, string groupName, AssetObject packet)
        {
            if (AssetGroups.ContainsKey(groupName))
            {
                AssetGroups[groupName].Add(assetName, packet);
            }
            else
            {
                AssetGroups.Add(groupName, new Dictionary<string, AssetObject>());
                AssetGroups[groupName].Add(assetName, packet);
            }
        }



        public static void OnRecievedAssetFromServerOrCache(string assetName, AssetBundle bundle)
        {
            Debug.Log(assetName);
            Debug.Log(bundle);

            if (bundle == null)
            {
                Debug.Log("Recived null asset");
            }

            string groupName = AssetGroupRefences[assetName];

            AssetObject newAssetObject = AssetGroups[groupName][assetName];

            if (bundle != null)
            {
                newAssetObject.asset = bundle.mainAsset;
                newAssetObject.bundle = bundle;
            }
            else
            {
                newAssetObject.asset = null;
                newAssetObject.bundle = null;
            }

            newAssetObject.isComplete = true;
            if (assetBundleTracker != null)
            {
                assetBundleTracker.addCompletedCount(groupName);
                assetBundleTracker.CheckIsCompletedAllDownloads(groupName);
            }
            newAssetObject.CallEvent();
        }

        public static void GroupIsReadyToBeCompleted(string groupName)
        {
            assetBundleTracker.GroupIsReadyToBeCompleted(groupName);
        }

        public static void RemoveAsset(string assetName)
        {
            string groupName = AssetGroupRefences[assetName];

            AssetGroups[groupName][assetName].bundle.Unload(true);
            AssetGroups[groupName].Remove(assetName);
            AssetGroupRefences.Remove(assetName);

            if (AssetGroups[groupName].Count <= 0)
            {
                AssetGroups.Remove(groupName);
                assetBundleTracker.trackedGroups.Remove(groupName);
            }
        }

        public static void RemoveGroupOfAssets(string groupName)
        {
            Debug.Log("Removing " + AssetGroups[groupName].Count + " assets form group : " + groupName);

            foreach (string assetName in AssetGroups[groupName].Keys)
            {
                AssetGroupRefences.Remove(assetName);
                AssetGroups[groupName][assetName].bundle.Unload(true);
                Destroy(AssetGroups[groupName][assetName].asset, 0.0f);
            }

            AssetGroups.Remove(groupName);
            assetBundleTracker.trackedGroups.Remove(groupName);
        }

        public static void RemoveAllAssets()
        {
            foreach (string groupName in AssetGroups.Keys)
            {
                foreach (string assetName in AssetGroups[groupName].Keys)
                {
                    AssetGroupRefences.Remove(assetName);
                    AssetGroups[groupName][assetName].bundle.Unload(true);
                    Destroy(AssetGroups[groupName][assetName].asset, 0.0f);
                }
            }

            AssetGroups.Clear();
            AssetGroupRefences.Clear();
            assetBundleTracker.trackedGroups.Clear();
        }

        public static bool IsAssetBeingDownloaded()
        {
            return assetBundleTracker.IsAssetBeingDownloaded();
        }

        static void ExceptionIfURLNotSet()
        {
            if (string.IsNullOrEmpty(CloudGoods.BundlesUrl))
            {
                throw new Exception("URL must be set before calling Get");
            }
        }

        static void ExceptionIfAssetNameNotSet(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
                throw new Exception("assetName must be set before calling Get");
        }

        static bool isAlreadyRequested(string assetName)
        {
            return AssetGroupRefences.ContainsKey(assetName);
        }

        static bool isAlreadyLoaded(string assetName)
        {
            string assetGroupName = AssetGroupRefences[assetName];

            if (AssetGroups[assetGroupName][assetName].isComplete)
                return true;
            else
                return false;
        }
    }
}
