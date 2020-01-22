using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftControllerVelocity : MonoBehaviour
{
    public SteamVR_TrackedObject mTrackObject = null;
    public SteamVR_Controller.Device mDevice;
    public int Delta = 20;
    public float AvgVelL = 0;

    private int currentDelta = 0;
    private int SumVel = 0;
    private int currentVel = 0;

    void Awake()
    {
        mTrackObject = GetComponent<SteamVR_TrackedObject>();
    }
    
    void FixedUpdate()
    {
        mDevice = SteamVR_Controller.Input((int)mTrackObject.index);

        Vector3 vel = mDevice.velocity;

        //print(vel[2]*100);
        currentVel = Mathf.Abs(Mathf.RoundToInt(vel[2] * 100)); // Multiply by 100 and Round Up to Int then Absoluted

        SumVel = SumVel + currentVel;
        if (currentDelta == Delta)
        {
            AvgVelL = SumVel / Delta;
            currentDelta = 0;
            SumVel = 0;
            //print("Average Velocity L = " + AvgVelL);
        }
        else
        {
            currentDelta++;
        }
    }
}
