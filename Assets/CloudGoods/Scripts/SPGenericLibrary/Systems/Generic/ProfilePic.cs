// ----------------------------------------------------------------------
// <copyright file="ProfilePic.cs" company="SocialPlay">
//     Copyright statement. All right reserved
// </copyright>
// Owner: Alex Zanfir
// Date: 11/8/2012
// ------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;


public class ProfilePic : MonoBehaviour
{
    Action<Texture2D> callback;

    string urlSocialPlay = "http://socialplay.com/RetrieveProfilePic.aspx";

    public enum PlatformID
    {
        FaceBook = 1,
        SocialPlay = 2,
        Google = 3,
        Kongregate = 4
    }

    //google has been removed as we cannot get the picture without authenticating ourselves first.

    public static void GetProfilePicture(Action<Texture2D> callback, PlatformID platformID, string platformUserID)
    {
        GameObject profilePicPacket = new GameObject("ProfilePicPacket");

        ProfilePic pic = profilePicPacket.AddComponent<ProfilePic>();

        pic.callback = callback;

        pic.GetProfilePicture(platformID, platformUserID);
    }

    public void GetProfilePicture(PlatformID platformID, string platformUserID)
    {
        StartCoroutine(GetProfilePic(platformID, platformUserID));
    }

    IEnumerator GetProfilePic(PlatformID platformID, string platformUserID)
    {
        string url = urlSocialPlay + "?PID=" + (int)platformID + "&PUID=" + platformUserID;

        WWW www = new WWW(url);
        yield return www;

        if (callback != null)
            callback(www.texture);

        //Event sent, lets destroy it.
        Destroy(gameObject);
    }
}
