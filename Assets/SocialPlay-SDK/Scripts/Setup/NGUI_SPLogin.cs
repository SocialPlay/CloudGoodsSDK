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
        loginTab.SetActive(true);
        registerErrorLabel.text = "";
        registerTab.SetActive(false);
        confirmationTab.SetActive(false);

        loginUserEmailValidator = loginUserEmail.GetComponent<UIInputVisualValidation>();
        loginUserPasswordValidator = loginUserPassword.GetComponent<UIInputVisualValidation>();


        registerUserEmailValidator = registerUserEmail.GetComponent<UIInputVisualValidation>();
        registerUserPasswordValidator = registerUserPassword.GetComponent<UIInputVisualValidation>(); ;
        registerUserPasswordConfirmValidator = registerUserPasswordConfirm.GetComponent<UIInputVisualValidation>();
        resendVerificationTextObject.SetActive(false);

    }

    #region webservice responce events

    void RecivedUserGuid(SPLogin.UserInfo obj)
    {
        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = "User logged in";
        this.gameObject.SetActive(false);
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
            SPLogin.Login(loginUserEmail.value, loginUserPassword.value);
        }
    }

    public void Register()
    {

        string ErrorMsg = "";
        if (!registerUserEmailValidator.IsValidCheck())
        {
            ErrorMsg = "-Invalid Email";
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
