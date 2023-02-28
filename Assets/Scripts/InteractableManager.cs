using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager iManager = null;
    void Awake()
    {
        if (iManager == null)
        {
            iManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (iManager != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Restart()
    {
        Destroy(this.gameObject);
    }
}
