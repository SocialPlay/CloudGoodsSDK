using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SocialPlay.Bundles
{
    public class GroupTracker
    {
        public int completedCount = 0;
        public int remainingDownloadCount = 0;
        public int totalDownloadCount = 0;
        public bool isReadyToComplete = false;
    }

    internal class AssetBundleTracker : MonoBehaviour
    {

        public GameObject loadingPanel = null;
        //GameObject loadingPanel;

        public static event Action<string> CompletedAllDownloads;

        public Dictionary<string, GroupTracker> trackedGroups = new Dictionary<string, GroupTracker>();



        void Start()
        {
            if(loadingPanel != null) loadingPanel.SetActive(true);
            DontDestroyOnLoad(gameObject);
        }

        public void addTotalDownloadCount(string trackedGroupName)
        {
            GroupTracker trackedGroup = trackedGroups[trackedGroupName];
            trackedGroup.totalDownloadCount++;
            trackedGroup.remainingDownloadCount = trackedGroup.totalDownloadCount - trackedGroup.completedCount;

        }

        public void addCompletedCount(string trackedGroupName)
        {
            GroupTracker trackedGroup = trackedGroups[trackedGroupName];
            trackedGroup.completedCount++;
            trackedGroup.remainingDownloadCount = trackedGroup.totalDownloadCount - trackedGroup.completedCount;
        }

        public GroupTracker GetProgressionOfGroup(string GroupName)
        {
            return trackedGroups[GroupName];
        }

        public void DisplayLoadingScreenOverlay(string trackedGroupName)
        {

            if (!trackedGroups.ContainsKey(trackedGroupName))
            {
                trackedGroups.Add(trackedGroupName, new GroupTracker());
            }
        }

        public void GroupIsReadyToBeCompleted(string groupName)
        {
            if (trackedGroups.ContainsKey(groupName))
            {
                trackedGroups[groupName].isReadyToComplete = true;

                CheckIsCompletedAllDownloads(groupName);
            }
            else
            { 
                Debug.LogWarning("Group : " + groupName + " asking to be ready does not exist");
                if (CompletedAllDownloads != null)
                    CompletedAllDownloads(groupName);
            }
        }

        public void CheckIsCompletedAllDownloads(string trackedGroupName)
        {
            GroupTracker trackedGroup = trackedGroups[trackedGroupName];
            trackedGroup.remainingDownloadCount = trackedGroup.totalDownloadCount - trackedGroup.completedCount;

            if (trackedGroup.remainingDownloadCount == 0)
            {
                RemoveLoadingPanel();

                if (trackedGroup.isReadyToComplete)
                {
                    CompletedAllDownloadsEventCall(trackedGroupName);
                    trackedGroups.Remove(trackedGroupName);
                }
            }
        }

        public bool IsAssetBeingDownloaded()
        {
            foreach (GroupTracker groupTracker in trackedGroups.Values)
            {
                if (groupTracker.remainingDownloadCount > 0)
                    return true;
            }

            return false;
        }

        private void RemoveLoadingPanel()
        {
            if(loadingPanel != null) loadingPanel.SetActive(false);
            /*if (GameObject.Find("LoadingPanel") != null)
            {
                GameObject.DestroyImmediate(loadingPanel);
            }*/
        }

        private void CompletedAllDownloadsEventCall(string groupName)
        {
            if (CompletedAllDownloads != null)
                CompletedAllDownloads(groupName);
        }


    }
}
