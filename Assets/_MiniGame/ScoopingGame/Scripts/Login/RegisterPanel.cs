using NTPackage.Functions;
using NTPackage.UI;
using Rubik.Account;
using Rubik.Manager;
using TMPro;
using UnityEngine;

public class RegisterPanel : MonoBehaviour
{
    public TMP_InputField userNameInput, passwordInput, repasswordInput;
    public TextMeshProUGUI textError;

    public string Username;
    public string Password;
    public string RePassword;

    // public override void OnUI(object data = null)
    // {
    //     base.OnUI(data);
    //     passwordInput.contentType = TMP_InputField.ContentType.Password;
    //     passwordInput.text = "";
    //     repasswordInput.text = "";
    //     userNameInput.text = "";
    // }
    public void RegisterOnclick()
    {
        this.Username = userNameInput.text;
        this.Password = passwordInput.text;
        this.RePassword = repasswordInput.text;
        if (!this.CheckUsername()) return;
        if (!this.CheckPassword()) return;
        if (!this.CheckRePassword()) return;
        StartCoroutine(AccountManager.Instance.IERegister(this.Username, this.Password, this.Username, Success));
    }
    public void Success(AuthenResponse authenResponse)
    {
        if (authenResponse.Status == 1)
        {
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText(authenResponse.Message);
            ServerManager.instance.LoginByUsernamePassword(this.Username, this.Password, (authenResponse) =>
            {
                if (authenResponse.Status == 1)
                {
                    this.textError.color = NTFunction.StringHexToColor(AccountConfig.SuccessColor);
                    PopupManager.Instance.OffUI(PopupCode.LoginPanel);
                    PopupManager.Instance.OffUI(PopupCode.RegisterPanel);
                    this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText(authenResponse.Message, authenResponse.Message);
                }
                else
                {
                    this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
                    this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText(authenResponse.Message, authenResponse.Message);
                }
            });
        }
        else
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText(authenResponse.Message);
        }
    }

    public void BackToLogin(){
        PopupManager.Instance.OffUI(PopupCode.RegisterPanel);
        PopupManager.Instance.OnUI(PopupCode.LoginPanel);
    }

    // Hold button to show password
    public void ShowPassword(){
        if(passwordInput.contentType == TMP_InputField.ContentType.Standard){
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            repasswordInput.contentType = TMP_InputField.ContentType.Password;
            //Update
        }else{
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            repasswordInput.contentType = TMP_InputField.ContentType.Standard;
            passwordInput.text+="";
            repasswordInput.text+="";
        }
    }

    public bool CheckUsername()
    {
        if (this.Username == null || this.Username.Length == 0)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("username_required", "Username is required");
            return false;
        }
        if (this.Username.Length < 6)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("username_too_short", "Username must be at least 6 characters");
            return false;
        }
        if (!AccountConfig.ValidPattern.IsMatch(this.Username))
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("username_invalid", "Username allow only letters, numbers, underscore");
            return false;
        }
        this.textError.text = "";
        return true;
    }

    public bool CheckPassword()
    {
        if (this.Password == null || this.Password.Length == 0)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("password_required", "Password is required");
            return false;
        }
        if (this.Password.Length < 6)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("password_too_short", "Password must be at least 6 characters");
            return false;
        }
        if (this.Password.Length < 6)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("password_too_short", "Password must be at least 6 characters");
            return false;
        }
        this.textError.text = "";
        return true;
    }

    public bool CheckRePassword()
    {
        if (this.Password != this.RePassword)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("password_and_repassword_not_same", "Password and Repassword are not the same");
            return false;
        }
        if (this.RePassword == null || this.RePassword.Length == 0)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("repassword_required", "Repassword is required");
            return false;
        }
        this.textError.text = "";
        return true;
    }
}
