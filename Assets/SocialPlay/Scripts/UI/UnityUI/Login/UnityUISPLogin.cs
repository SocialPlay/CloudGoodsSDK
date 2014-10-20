using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UnityUISPLogin : MonoBehaviour
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

    void OnEnable()
    {
        SP.OnUserLogin += RecivedLoginResponce;
        SP.OnUserInfo += RecivedUserGuid;
        SP.OnUserRegister += RegisterMessageResponce;
        SP.OnForgotPassword += ForgotPasswordResponce;
        SP.OnVerificationSent += ResentVerificationResponce;
        SPLogout.SPUserLogout += OnLogout;
    }

    void OnDisable()
    {
        SP.OnUserLogin -= RecivedLoginResponce;
        SP.OnUserInfo -= RecivedUserGuid;
        SP.OnUserRegister -= RegisterMessageResponce;
        SP.OnForgotPassword -= ForgotPasswordResponce;
        SP.OnVerificationSent -= ResentVerificationResponce;
        SPLogout.SPUserLogout -= OnLogout;
    }

    void Start()
    {

        ContainerKeybinding.DisableKeybinding("Login");

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
            loginUserEmail.value = PlayerPrefs.GetString("SocialPlay_Login_UserEmail");
        }

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("SocialPlay_UserGuid")))
        {
            SocialPlayUser userInfo = new SocialPlayUser(PlayerPrefs.GetString("SocialPlay_UserGuid"), PlayerPrefs.GetString("SocialPlay_UserName"), PlayerPrefs.GetString("SocialPlay_UserEmail"));

            SP.AuthorizeUser(userInfo);

            RecivedUserGuid(userInfo);
        }
    }

    #region webservice responce events

    void RecivedUserGuid(SocialPlayUser obj)
    {
        if (autoLoginToggle != null && autoLoginToggle.isOn == true)
        {
            PlayerPrefs.SetString("SocialPlay_UserGuid", obj.userGuid.ToString());
            PlayerPrefs.SetString("SocialPlay_UserName", obj.userName);
            PlayerPrefs.SetString("SocialPlay_UserEmail", obj.userEmail);
        }

        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = "User logged in";
        this.gameObject.SetActive(false);
        ContainerKeybinding.EnableKeybinding("Login");
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
        confirmationStatus.gameObject.SetActive(true);
        confirmationTab.SetActive(true);
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
            Debug.Log("login email: " + loginUserEmail.value + " login password: " + loginUserPassword.value);

            PlayerPrefs.SetString("SocialPlay_Login_UserEmail", loginUserEmail.value);
            SP.Login(loginUserEmail.value.ToLower(), loginUserPassword.value, null);
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
            SP.Register(registerUserEmail.value, registerUserPassword.value, registerUserName.value, OnRegisteredUser);
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
            SP.ForgotPassword(loginUserEmail.value, OnSentPassword);
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
            SP.ResendVerificationEmail(loginUserEmail.value, null);
        }

    }

    void OnLogout()
    {
        loginTab.SetActive(true);
    }

    #endregion
}
