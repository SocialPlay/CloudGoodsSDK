﻿using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;

public class SPLogin : MonoBehaviour
{
    private static SPLogin instance;

    public class SPLogin_Responce
    {
        public int code;
        public string message;
        public UserInfo userInfo;

        public SPLogin_Responce(int caseCode, string msg, UserInfo newUserInfo)
        {
            code = caseCode;
            message = msg;
            userInfo = newUserInfo;
        }

        public override string ToString()
        {
            return "Code :" + code + "\nMessage :" + message;
        }
    }

    public class UserInfo
    {
        public Guid ID;
        public string name;
        public string email;

        public UserInfo(Guid userID, string userName, string userEmail)
        {
            ID = userID;
            name = userName;
            email = userEmail;
        }
    }

    public static event Action<SPLogin_Responce> loginMessageResponce;
    public static event Action<UserInfo> recivedUserInfo;

    public static event Action<SPLogin_Responce> RegisterMessageResponce;


    public static event Action<SPLogin_Responce> ForgotPasswordResponce;

    public static event Action<SPLogin_Responce> ResentVerificationResponce;


    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }       
    }


    public static void RegisterUser(string email, string pass, string userName)
    {
        ItemSystemGameData.AppID = new Guid(GameAuthentication.GetAppID());
        WebserviceCalls.webservice.SPLogin_UserRegister(ItemSystemGameData.AppID, email, pass, userName, instance.RecivedRegisterUser);
    }


    public static void Login(string email, string pass)
    {
        ItemSystemGameData.AppID = new Guid(GameAuthentication.GetAppID());
        WebserviceCalls.webservice.SPLogin_UserLogin(ItemSystemGameData.AppID, email, pass, instance.RecivedLoginCode);
    }

    public static void ForgotPassword(string Email)
    {
        ItemSystemGameData.AppID = new Guid(GameAuthentication.GetAppID());
        WebserviceCalls.webservice.SPLoginForgotPassword(ItemSystemGameData.AppID, Email, instance.RecivedForgotPassword);
    }

    public static void ResendVerificationEmail(string email)
    {
        ItemSystemGameData.AppID = new Guid(GameAuthentication.GetAppID());
        WebserviceCalls.webservice.SPLoginResendVerificationEmail(ItemSystemGameData.AppID, email, instance.RecivedVerificationEmailResponce);
    }

    void RecivedRegisterUser(SPLogin_Responce registerResponse)
    {
        if (registerResponse.code == 7)
        {
            registerResponse.message = "Server Related Error";
        }
        if ( RegisterMessageResponce != null)
        {
            RegisterMessageResponce(registerResponse);
        }
    }

    void RecivedLoginCode(SPLogin_Responce loginResponse)
    {
        if (loginResponse.code == 7)
        {
            loginResponse.message = "Server Related Error";
        }
        if (loginResponse.code == 0)
        {
            if (recivedUserInfo != null)
            {
                UserInfo userInfo = loginResponse.userInfo;
                GameAuthentication.OnUserAuthorized(new WebserviceCalls.UserInfo(userInfo.ID.ToString(), userInfo.name, userInfo.email));
                recivedUserInfo(userInfo);
            }
        }
        else
        {
            if (loginMessageResponce != null)
            {
                loginMessageResponce(loginResponse);
            }
        }
    }

    void RecivedForgotPassword(SPLogin_Responce forgotPasswordResponse)
    {
        if (forgotPasswordResponse.code == 7)
        {
            forgotPasswordResponse.message = "Server Related Error";
        }
        if (ForgotPasswordResponce != null)
        {
            ForgotPasswordResponce(forgotPasswordResponse);
        }
    }

    void RecivedVerificationEmailResponce(SPLogin_Responce verificationResponse)
    {
        if (verificationResponse.code == 7)
        {
            verificationResponse.message = "Server Related Error";
        }
        if (ResentVerificationResponce != null)
        {
            ResentVerificationResponce(verificationResponse);
        }

    }


}
