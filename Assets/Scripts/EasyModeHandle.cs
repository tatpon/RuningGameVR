using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyModeHandle : MonoBehaviour {
    public bool easymode = false;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   
    }

}
