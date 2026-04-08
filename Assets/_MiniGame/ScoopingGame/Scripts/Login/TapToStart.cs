using Rubik.Manager;
using UnityEngine;

public class TapToStart : MonoBehaviour
{
    public void OnTapToStart()
    {
        ServerManager.instance.TapToStart();
    }
}
