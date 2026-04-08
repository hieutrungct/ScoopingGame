using NTPackage.UI;
using UnityEngine;

public class Popup_Login : PopupUI
{
    public LoginPanel loginPanel;
    public RegisterPanel registerPanel;
    public void ShowPanelLogin()
    {
        loginPanel.gameObject.SetActive(true);
        registerPanel.gameObject.SetActive(false);
    }
    public void ShowPanelRegister()
    {
        loginPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(true);
    }
}
