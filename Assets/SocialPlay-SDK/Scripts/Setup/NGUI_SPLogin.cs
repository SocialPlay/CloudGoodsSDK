using UnityEngine;
using System.Collections;
using System;

public class NGUI_SPLogin : MonoBehaviour
{


    #region Login variables
    public GameObject loginTab;
    public UIInput loginUserEmail;
    public UIInput loginUserPassword;
    public UILabel loginErrorLabel;

    public UIToggle autoLoginToggle;

    public GameObject resendVerificationTextObject;

    private UIInputVisualValidation loginUserEmailValidator;
    private UIInputVisualValidation loginUserPasswordValidator;

    #endregion

    #region Register variables
    public GameObject registerTab;
    public UIInput registerUserEmail;
    public UIInput registerUserPassword;
    public UIInput registerUserPasswordConfirm;
    public UIInput registerUserName;
    public UILabel registerErrorLabel;

    private UIInputLengthValidation registerUserNameValidator;
    private UIInputVisualValidation registerUserEmailValidator;
    private UIInputVisualValidation registerUserPasswordValidator;
    private UIInputVisualValidation registerUserPasswordConfirmValidator;
    #endregion

    #region Confirmations Variables
    public GameObject confirmationTab;
    public UILabel confirmationStatus;
    public UIButton confirmationButton;

    #endregion

    void OnEnable()
    {
        SPLogin.loginMessageResponce += RecivedLoginResponce;
        SPLogin.recivedUserInfo += RecivedUserGuid;
        SPLogin.RegisterMessageResponce += RegisterMessageResponce;
        SPLogin.ForgotPasswordResponce += ForgotPasswordResponce;
        SPLogin.ResentVerificationResponce += ResentVerificationResponce;
    }

    void OnDisable()
    {
        SPLogin.loginMessageResponce -= RecivedLoginResponce;
        SPLogin.recivedUserInfo -= RecivedUserGuid;
        SPLogin.RegisterMessageResponce -= RegisterMessageResponce;
        SPLogin.ForgotPasswordResponce -= ForgotPasswordResponce;
        SPLogin.ResentVerificationResponce -= ResentVerificationResponce;
    }

    void Start()
    {

        ContainerKeybinding.DisableKeybinding("Login");

        loginTab.SetActive(true);
        registerErrorLabel.text = "";
        registerTab.SetActive(false);
        confirmationTab.SetActive(false);

        loginUserEmailValidator = loginUserEmail.GetComponent<UIInputVisualValidation>();
        loginUserPasswordValidator = loginUserPassword.GetComponent<UIInputVisualValidation>();

        registerUserNameValidator = registerUserName.GetComponent<UIInputLengthValidation>();
        registerUserEmailValidator = registerUserEmail.GetComponent<UIInputVisualValidation>();
        registerUserPasswordValidator = registerUserPassword.GetComponent<UIInputVisualValidation>(); ;
        registerUserPasswordConfirmValidator = registerUserPasswordConfirm.GetComponent<UIInputVisualValidation>();
        resendVerificationTextObject.SetActive(false);
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("SocialPlay_Login_UserEmail")))
        {
            loginUserEmail.value = PlayerPrefs.GetString("SocialPlay_Login_UserEmail");
        }

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("SocialPlay_UserGuid")))
        {
            SPLogin.UserInfo userInfo = new SPLogin.UserInfo(new Guid(PlayerPrefs.GetString("SocialPlay_UserGuid")), PlayerPrefs.GetString("SocialPlay_UserName"), PlayerPrefs.GetString("SocialPlay_UserEmail"));

            GameAuthentication.OnUserAuthorized(new WebserviceCalls.UserInfo(userInfo.ID.ToString(), userInfo.name, userInfo.email));

            RecivedUserGuid(userInfo);
        }
    }

    #region webservice responce events

    void RecivedUserGuid(SPLogin.UserInfo obj)
    {
        if (autoLoginToggle != null && autoLoginToggle.value == true)
        {
            PlayerPrefs.SetString("SocialPlay_UserGuid", obj.ID.ToString());
            PlayerPrefs.SetString("SocialPlay_UserName", obj.name);
            PlayerPrefs.SetString("SocialPlay_UserEmail", obj.email);
        }

        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = "User logged in";
        this.gameObject.SetActive(false);
        ContainerKeybinding.EnableKeybinding("Login");
    }

    void ResentVerificationResponce(SPLogin.SPLogin_Responce responce)
    {
        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = responce.message;
    }

    void ForgotPasswordResponce(SPLogin.SPLogin_Responce responce)
    {
        //resendVerificationTextObject.SetActive(false);
        //loginErrorLabel.text = responce.message;

        confirmationStatus.text = responce.message;

    }

    void RecivedLoginResponce(SPLogin.SPLogin_Responce recivedMessage)
    {
        if (recivedMessage.code == 3)
        {
            resendVerificationTextObject.SetActive(true);
            return;
        }

        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = recivedMessage.message;
    }

    void RegisterMessageResponce(SPLogin.SPLogin_Responce responce)
    {
        resendVerificationTextObject.SetActive(false);

        if (responce.code == 0)
        {
            confirmationStatus.text = "Verification Email has been sent to your Email";
            confirmationButton.onClick.Clear();
            confirmationButton.onClick.Add(new EventDelegate(this, "SwitchToLogin"));
            confirmationButton.GetComponentInChildren<UILabel>().text = "To Login";
        }
        else
        {
            confirmationStatus.text = responce.message;
            confirmationButton.onClick.Clear();
            confirmationButton.onClick.Add(new EventDelegate(this, "SwitchToRegister"));
            confirmationButton.GetComponentInChildren<UILabel>().text = "Back";
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
            PlayerPrefs.SetString("SocialPlay_Login_UserEmail", loginUserEmail.value);
            SPLogin.Login(loginUserEmail.value.ToLower(), loginUserPassword.value);
        }
    }

    public void Register()
    {

        string ErrorMsg = "";
        if (!registerUserNameValidator.IsValidCheck())
        {
            if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
            ErrorMsg += "-Invalid User Name";
        }
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
            SPLogin.RegisterUser(registerUserEmail.value, registerUserPassword.value, registerUserName.value);
        }
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
            confirmationButton.onClick.Clear();
            confirmationButton.onClick.Add(new EventDelegate(this, "SwitchToLogin"));
            confirmationButton.GetComponentInChildren<UILabel>().text = "Back";
            SPLogin.ForgotPassword(loginUserEmail.value);
        }
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
            confirmationButton.onClick.Clear();
            confirmationButton.onClick.Add(new EventDelegate(this, "SwitchToLogin"));
            confirmationButton.GetComponentInChildren<UILabel>().text = "Back";
            SPLogin.ResendVerificationEmail(loginUserEmail.value);
        }

    }

    #endregion
}
