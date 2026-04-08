using NTPackage.Functions;
using UnityEngine;

public class LoginController : NTBehaviour
{
    public static LoginController Instance;
    [SerializeField] private GameObject PanelLogin;
    [SerializeField] private GameObject PanelLoading;
    // protected override void Awake()
    // {
    //     base.Awake();
    //     if (LoginController.Instance != null){
    //        NTLog.LogWarning("Only 1 Instance allow");
    //        return;
    //      }
    //     LoginController.Instance = this;
    // }

    // protected override void Start()
    // {
    //     base.Start();
    //     ShowPanelLogin();
    // }

    // public void ShowPanelLogin(){
    //     this.PanelLogin.SetActive(true);
    //     this.PanelLoading.SetActive(false);
    // }
}
