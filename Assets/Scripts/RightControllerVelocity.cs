using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightControllerVelocity : MonoBehaviour
{
    public SteamVR_TrackedObject mTrackObject = null;
    public SteamVR_Controller.Device mDevice;
    public int Delta = 20;
    public float AvgVelR = 0;

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
            AvgVelR = SumVel / Delta;
            currentDelta = 0;
            SumVel = 0;
            //print("Average Velocity R = " + AvgVelR);
        }
        else
        {
            currentDelta++;
        }
    }
}
