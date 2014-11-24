using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityUICloudGoodsLogin : MonoBehaviour
{

    #region Login variables
    public GameObject loginTab;
    public InputField loginUserEmail;
    public InputField loginUserPassword;
    public Text loginErrorLabel;

    public Toggle autoLoginToggle;

    public GameObject resendVerificationTextObject;


    private InputFieldValidation loginUserEmailValidator;
    private InputFieldValidation loginUserPasswordValidator;


    #endregion

    #region Register variables
    public GameObject registerTab;
    public InputField registerUserEmail;
    public InputField registerUserPassword;
    public InputField registerUserPasswordConfirm;
    public InputField registerUserName;
    public Text registerErrorLabel;

    private InputFieldValidation registerUserEmailValidator;
    private InputFieldValidation registerUserPasswordValidator;
    private InputFieldValidation registerUserPasswordConfirmValidator;
    #endregion

    #region Confirmations Variables
    public GameObject confirmationTab;
    public Text confirmationStatus;
    #endregion

    public bool IsKeptActiveOnAllPlatforms;

    void OnEnable()
    {
        CloudGoods.OnUserLogin += RecivedLoginResponce;
        CloudGoods.OnUserInfo += RecivedUserGuid;
        CloudGoods.OnUserRegister += RegisterMessageResponce;
        CloudGoods.OnForgotPassword += ForgotPasswordResponce;
        CloudGoods.OnVerificationSent += ResentVerificationResponce;
        CloudGoods.onLogout += OnLogout;
    }

    void OnDisable()
    {
        CloudGoods.OnUserLogin -= RecivedLoginResponce;
        CloudGoods.OnUserInfo -= RecivedUserGuid;
        CloudGoods.OnUserRegister -= RegisterMessageResponce;
        CloudGoods.OnForgotPassword -= ForgotPasswordResponce;
        CloudGoods.OnVerificationSent -= ResentVerificationResponce;
        CloudGoods.onLogout -= OnLogout;
        //CloudGoodsLogout.CloudGoodsUserLogout -= OnLogout;
    }

    void Start()
    {
        if (!RemoveIfNeeded())
        {
            return;
        }

        loginTab.SetActive(true);
        registerErrorLabel.text = "";
        registerTab.SetActive(false);
        confirmationTab.SetActive(false);

        loginUserEmailValidator = loginUserEmail.GetComponent<InputFieldValidation>();
        loginUserPasswordValidator = loginUserPassword.GetComponent<InputFieldValidation>();

        registerUserEmailValidator = registerUserEmail.GetComponent<InputFieldValidation>();
        registerUserPasswordValidator = registerUserPassword.GetComponent<InputFieldValidation>(); ;
        registerUserPasswordConfirmValidator = registerUserPasswordConfirm.GetComponent<InputFieldValidation>();
        resendVerificationTextObject.SetActive(false);
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("SocialPlay_Login_UserEmail")))
        {
            loginUserEmail.text = PlayerPrefs.GetString("SocialPlay_Login_UserEmail");
        }
        else
        {
            loginUserEmail.text = null;
        }

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("SocialPlay_UserGuid")))
        {
            CloudGoodsUser userInfo = new CloudGoodsUser(PlayerPrefs.GetString("SocialPlay_UserGuid"), PlayerPrefs.GetString("SocialPlay_UserName"), PlayerPrefs.GetString("SocialPlay_UserEmail"));

            CloudGoods.AuthorizeUser(userInfo);

            RecivedUserGuid(userInfo);
        }

    }

    #region webservice responce events

    void RecivedUserGuid(CloudGoodsUser obj)
    {
        if (autoLoginToggle != null && autoLoginToggle.isOn == true)
        {
            PlayerPrefs.SetString("SocialPlay_UserGuid", obj.userGuid.ToString());
            PlayerPrefs.SetString("SocialPlay_UserName", obj.userName);
            PlayerPrefs.SetString("SocialPlay_UserEmail", obj.userEmail);
        }

        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = "User logged in";

        CloseAllTabsOnLogin();
    }

    void ResentVerificationResponce(UserResponse responce)
    {
        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = responce.message;
    }

    void ForgotPasswordResponce(UserResponse responce)
    {
        confirmationStatus.text = responce.message;
    }

    void RecivedLoginResponce(UserResponse recivedMessage)
    {
        if (recivedMessage.code == 3)
        {
            resendVerificationTextObject.SetActive(true);
            return;
        }

        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = recivedMessage.message;
    }

    void RegisterMessageResponce(UserResponse responce)
    {
        resendVerificationTextObject.SetActive(false);

        if (responce.code == 0)
        {
            confirmationStatus.text = "Verification Email has been sent to your Email";
        }
        else
        {
            confirmationStatus.text = responce.message;
        }
    }

    void LoginSuccess(Guid userID)
    {
        loginErrorLabel.text = userID.ToString();
    }
    #endregion

    #region button functions

    public void SwitchToRegister()
    {
        registerErrorLabel.text = "";
        loginTab.SetActive(false);
        registerTab.SetActive(true);
        confirmationTab.SetActive(false);
    }

    public void SwitchToLogin()
    {
        loginErrorLabel.text = "";
        registerTab.SetActive(false);
        loginTab.SetActive(true);
        confirmationTab.SetActive(false);
    }

    public void SwitchToConfirmation()
    {
        confirmationStatus.text = "Waiting ...";
        confirmationTab.SetActive(true);
        loginTab.SetActive(false);
        registerTab.SetActive(false);
    }

    void CloseAllTabsOnLogin()
    {
        confirmationTab.SetActive(false);
        loginTab.SetActive(false);
        registerTab.SetActive(false);
    }

    public void Login()
    {
        string ErrorMsg = "";
        if (!loginUserEmailValidator.IsValidCheck())
        {
            ErrorMsg = "-Invalid Email";
        }

        if (!loginUserPasswordValidator.IsValidCheck())
        {
            if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
            ErrorMsg += "-Invalid Password";
        }
        loginErrorLabel.text = ErrorMsg;
        if (string.IsNullOrEmpty(ErrorMsg))
        {
            Debug.Log("login email: " + loginUserEmail.text + " login password: " + loginUserPassword.text);

            PlayerPrefs.SetString("SocialPlay_Login_UserEmail", loginUserEmail.text);
            CloudGoods.Login(loginUserEmail.text.ToLower(), loginUserPassword.text, null);
        }
    }

    public void Register()
    {
        string ErrorMsg = "";
        if (!registerUserEmailValidator.IsValidCheck())
        {
            if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
            ErrorMsg += "-Invalid Email";
        }

        if (!registerUserPasswordValidator.IsValidCheck() || !registerUserPasswordConfirmValidator.IsValidCheck())
        {
            if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
            ErrorMsg += "-Invalid Password";
        }
        registerErrorLabel.text = ErrorMsg;
        if (string.IsNullOrEmpty(ErrorMsg))
        {
            SwitchToConfirmation();
            CloudGoods.Register(registerUserEmail.text, registerUserPassword.text, registerUserName.text, OnRegisteredUser);
        }
    }

    void OnRegisteredUser(UserResponse userResponse)
    {
        confirmationStatus.gameObject.SetActive(true);
        confirmationStatus.text = userResponse.message;
    }

    public void ForgotPassword()
    {

        string ErrorMsg = "";
        if (!loginUserEmailValidator.IsValidCheck())
        {
            ErrorMsg = "Password reset requires valid E-mail";
        }
        loginErrorLabel.text = ErrorMsg;
        if (string.IsNullOrEmpty(ErrorMsg))
        {
            SwitchToConfirmation();
            CloudGoods.ForgotPassword(loginUserEmail.text, OnSentPassword);
        }
    }

    void OnSentPassword(UserResponse userResponse)
    {
        confirmationStatus.text = "Password reset has been sent";
    }

    public void ResendVerificationEmail()
    {
        string ErrorMsg = "";
        if (!loginUserEmailValidator.IsValidCheck())
        {
            ErrorMsg = "Validation resend requires valid E-mail";
        }
        loginErrorLabel.text = ErrorMsg;
        if (string.IsNullOrEmpty(ErrorMsg))
        {
            SwitchToConfirmation();
            CloudGoods.ResendVerificationEmail(loginUserEmail.text, null);
        }

    }

    void OnLogout()
    {
        Debug.Log("User logged out");
        SwitchToLogin();
    }

    #endregion


    public bool RemoveIfNeeded()
    {
        if (IsKeptActiveOnAllPlatforms) return true;

        if (BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Automatic)
        {
            BuildPlatform.OnBuildPlatformFound += platform => { RemoveIfNeeded(); };
            return false;
        }

        if (BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Facebook || BuildPlatform.Platform == BuildPlatform.BuildPlatformType.Kongergate)
        {
            Destroy(loginTab);
            Destroy(registerTab);
            Destroy(confirmationTab);
            Destroy(this);
            return false;
        }  
        return true;
    }
}
