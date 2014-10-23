// ----------------------------------------------------------------------
// <copyright file="AssetBundleGetter.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir & Mike Oliver
// Date: 11/15/2012
// Description: This is a utility class for downloading asset bundles
// ------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

namespace SocialPlay.Bundles
{
    internal class AssetBundleGetter
    {
        public static void LoadFromCacheOrDownload(string assetName, int bundleVersion, Action<string, AssetBundle> callback, bool isAlwaysCallback=false)
        {
            string assetURL = CloudGoods.BundlesUrl +assetName + ".unity3d";

            int version = bundleVersion;

            BundlePacket.Create(assetURL, bundle =>
            {
                callback(assetName, bundle);
            }, version,isAlwaysCallback);
        }

    }
}