using UnityEngine;
using System.Collections;

namespace WebModels
{
    [System.Serializable]
    public class ItemsInfo
    {
        public int ItemID;
        public int amount;
        public int location;
    }

    public enum OwnerTypes
    {
        User, instance, session
    }
}
