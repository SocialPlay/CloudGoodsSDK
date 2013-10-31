using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace SocialPlay_Editor
{
    public class ServerAtlasInforamtion : MonoBehaviour
    {
        public string LastAppID = "";
        public Texture2D LastDefaultImage = null;
        public List<ItemImageInformation> currentlyRecivedImages = new List<ItemImageInformation>();
        public Atlases atlasData = new Atlases();
    }


    [System.Serializable]
    public class ItemImageInformation
    {
        public string itemName;
        public Texture2D image;
        public string imageName;
        public int baseItemID;

        internal Rect atlasRect = new Rect();



    }
}