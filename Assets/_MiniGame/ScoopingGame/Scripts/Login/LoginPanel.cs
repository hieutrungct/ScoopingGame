using NTPackage.Functions;
using NTPackage.UI;
using Rubik.Account;
using Rubik.Manager;
using TMPro;
using UnityEngine;

public class LoginPanel : MonoBehaviour
{
    public string Username;
    public string Password;

    // public override void OnUI(object data = null)
    // {
    //     if (this.IsShow()) return;
    //     base.OnUI(data);
    //     passwordInput.contentType = TMP_InputField.ContentType.Password;
    //     passwordInput.text = "";
    //     userNameInput.text = "";
    //     textError.text = "";
    // }

    public TMP_InputField userNameInput, passwordInput;
    public TextMeshProUGUI textError;
    public void OnClickLogin()
    {
        //SoundController.Instance.PlaySingle(FXSound.Instance.Fx_Button1);
        this.Username = userNameInput.text;
        this.Password = passwordInput.text;
        if (!this.CheckUsername()) return;
        if (!this.CheckPassword()) return;
        if (this.Username.Length == 0 || this.Password.Length == 0)
        {
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("login_username_password_empty", "Username and password cannot be empty");
            return;
        }
        ServerManager.instance.LoginByUsernamePassword(this.Username, this.Password, Success);
    }

    public void Success(AuthenResponse authenResponse)
    {
        if (authenResponse.Status == 1)
        {
            PopupManager.Instance.OffUI(PopupCode.LoginPanel);
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText(authenResponse.Message, authenResponse.Message);
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.SuccessColor);
        }
        else
        {
            this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText(authenResponse.Message, authenResponse.Message);
            this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
        }
    }

    public void OnclickSignUp()
    {
        PopupManager.Instance.OnUI(PopupCode.RegisterPanel);
    }

    public void QuickPlay()
    {
        // this.OffUI();
        ServerManager.instance.LoginByDeviceID(true);
    }

    public void _OnClickLoginByGooglePlay()
    {
        // GoogleAuthen.GoogleAuthen.Instance.LoginGooglePlayGames((playerId, playerName, email, success) =>
        //     {
        //         if (success)
        //         {
        //             ServerManager.instance.LoginByGooglePlay();
        //         }
        //         else
        //         {
        //             this.textError.text = Lean.Localization.LeanLocalization.GetTranslationText("login_google_play_error", "Failed to login Google Play account");
        //             this.textError.color = NTFunction.StringHexToColor(AccountConfig.ErrorColor);
        //         }
        //     });
    }

    public void _OnChangeRemember()
    {
        if (!AccountManager.Instance.IsAutoLoginSelection())
        {
            // this.BtnRemember.Chose();
            AccountManager.Instance.SetIsAutoLoginSelection(true);
        }
        else
        {
            // this.BtnRemember.Unchose();
            AccountManager.Instance.SetIsAutoLoginSelection(false);
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
}
