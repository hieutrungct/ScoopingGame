using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    
    void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (Application.targetFrameRate < 60)
        {
            Application.targetFrameRate = 60;
        }
    }
}
