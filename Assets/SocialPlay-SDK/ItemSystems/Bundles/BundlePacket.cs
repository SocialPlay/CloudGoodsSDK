using UnityEngine;
using System.Collections;
using System;

namespace SocialPlay.Bundles
{
    public class BundlePacket : MonoBehaviour
    {
        public static void Create(string assetURL, Action<AssetBundle> callback, int version, bool isAlwaysCallback = false)
        {
            GameObject obj = new GameObject("BundlePacket");
            BundlePacket bundle = obj.AddComponent<BundlePacket>();

            bundle.StartLoadFromCacheOrDownload(assetURL, callback, version, isAlwaysCallback);
        }

        IEnumerator LoadFromCacheOrDownload(string assetURL, Action<AssetBundle> callback, int version, bool isAlwaysCallback)
        {
            using (WWW www = WWW.LoadFromCacheOrDownload(assetURL, version))
            {
                yield return www;
                if (www.error != null)
                {                    
                    if (isAlwaysCallback)
                    {
                        Response(null, callback);
                    }
                    throw new Exception(string.Format("WWW error laoding {1}: {0}", www.error, assetURL));
                }

                Response(www.assetBundle, callback);
            }
        }

        void Response(AssetBundle bundle, Action<AssetBundle> callback)
        {
            callback(bundle);
            //bundle.Unload (false);
            Destroy(gameObject);
        }

        protected void StartLoadFromCacheOrDownload(string assetURL, Action<AssetBundle> callback, int version, bool isAlwaysCallback)
        {
            StartCoroutine(LoadFromCacheOrDownload(assetURL, callback, version, isAlwaysCallback));
        }
    }
}

