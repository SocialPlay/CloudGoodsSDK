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

    void OnEnable()
    {
        SPLogin.loginMessageResponce += RecivedLoginResponce;
        SPLogin.recivedUserGuid += RecivedUserGuid;
        SPLogin.RegisterMessageResponce += RegisterMessageResponce;
        SPLogin.ForgotPasswordResponce += ForgotPasswordResponce;
        SPLogin.ResentVerificationResponce += ResentVerificationResponce;
    }

    void OnDisable()
    {
        SPLogin.loginMessageResponce -= RecivedLoginResponce;
        SPLogin.recivedUserGuid -= RecivedUserGuid;
        SPLogin.RegisterMessageResponce -= RegisterMessageResponce;
        SPLogin.ForgotPasswordResponce -= ForgotPasswordResponce;
        SPLogin.ResentVerificationResponce -= ResentVerificationResponce;
    }

    void Start()
    {
        loginTab.SetActive(true);
        registerErrorLabel.text = "";
        registerTab.SetActive(false);

        loginUserEmailValidator = loginUserEmail.GetComponent<UIInputVisualValidation>();
        loginUserPasswordValidator = loginUserPassword.GetComponent<UIInputVisualValidation>();


        registerUserEmailValidator = registerUserEmail.GetComponent<UIInputVisualValidation>();
        registerUserPasswordValidator = registerUserPassword.GetComponent<UIInputVisualValidation>(); ;
        registerUserPasswordConfirmValidator = registerUserPasswordConfirm.GetComponent<UIInputVisualValidation>();
        resendVerificationTextObject.SetActive(false);

    }

    #region webservice responce events

    void RecivedUserGuid(Guid obj)
    {
        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = "User logged in";
    }

    void ResentVerificationResponce(SPLogin.SPLogin_Responce responce)
    {
        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = responce.message;
    }

    void ForgotPasswordResponce(SPLogin.SPLogin_Responce responce)
    {
        resendVerificationTextObject.SetActive(false);
        loginErrorLabel.text = responce.message;
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
            registerErrorLabel.text = "Verification Email has been sent to your Email";
        }
        else
        {
            registerErrorLabel.text = responce.message;
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
    }

    public void SwitchToLogin()
    {
        loginErrorLabel.text = "";
        registerTab.SetActive(false);
        loginTab.SetActive(true);
    }

    public void Login()
    {
        string ErrorMsg = "";
        if (!loginUserEmailValidator.IsValidCheck())
        {
            ErrorMsg = "-Not Valid Email";
        }

        if (!loginUserPasswordValidator.IsValidCheck())
        {
            if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";
            ErrorMsg += "-Not Valid Password";
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
            ErrorMsg = "-Not Valid Email";
        }

        if (!registerUserPasswordValidator.IsValidCheck() || !registerUserPasswordConfirmValidator.IsValidCheck())
        {
            if (!string.IsNullOrEmpty(ErrorMsg)) ErrorMsg += "\n";

            ErrorMsg += "-Not Valid Password";

        }

        registerErrorLabel.text = ErrorMsg;
        if (string.IsNullOrEmpty(ErrorMsg))
        {
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
        SPLogin.ForgotPassword(loginUserEmail.value);
    }

    public void ResendVerificationEmail()
    {
        string ErrorMsg = "";
        if (!loginUserEmailValidator.IsValidCheck())
        {
            ErrorMsg = "Validation resend requires valid E-mail";
        }
        loginErrorLabel.text = ErrorMsg;
        SPLogin.ResendVerificationEmail(loginUserEmail.value);

    }

    #endregion
}
