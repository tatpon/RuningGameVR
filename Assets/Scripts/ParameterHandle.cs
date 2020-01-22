using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterHandle : MonoBehaviour {

    public GameObject CameraRig;

    private GameObject[] easy;
    
    private SpeedController para;

    private bool iseasy;
    void Awake()
    {
        
        easy = GameObject.FindGameObjectsWithTag("Parameter");
        Debug.Log(easy);
        
        para = CameraRig.GetComponent<SpeedController>();
        iseasy = easy[0].GetComponent<EasyModeHandle>().easymode;
        if (iseasy)
        {
            para.SpeedMultiplier = 0.02f;
            para.speedUpStep = 0.002f;
            para.setEasyMode(iseasy);
        }
        else
        {
            para.SpeedMultiplier = 0.03f;
            para.speedUpStep = 0.005f;
        }
        
    }

}
