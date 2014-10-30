﻿using UnityEngine;
using System.Collections;

public class BuildPlatformLoginSetup : MonoBehaviour
{

    void Awake()
    {
        if (CloudGoodsSettings.BuildPlatform == CloudGoodsSettings.BuildPlatformType.Facebook)
        {
            this.gameObject.AddComponent<CloudGoodsFacebookLogin>();
        }

    }
}
