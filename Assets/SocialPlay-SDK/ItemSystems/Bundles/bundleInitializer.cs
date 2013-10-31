using SocialPlay.Bundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPlay.ItemSystems
{
   public  class bundleInitializer
    {
        public static void SetBungleURL(string url)
        {
            BundleSystem.URL = url;
        }
    }
}
