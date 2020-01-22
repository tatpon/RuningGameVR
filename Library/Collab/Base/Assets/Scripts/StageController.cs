using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class StageController : MonoBehaviour
{
    private SpeedController speedCon;

    [HideInInspector]
    public float SpeedMultiplier;

    [HideInInspector]
    public float StageSpeed = 0;
    [HideInInspector]
    public int PlayerSpeed = 0;
    [HideInInspector]
    public bool C_Spawn = true;
   

    public GameObject CameraRig;
    public GameObject SpawnStage01;
    public GameObject SpawnStage02;
    public GameObject SpawnStage03;

    private int speed;

    // Use this for initialization
    void Start()
    {
        CameraRig = GameObject.Find("[CameraRig]");
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();

        CheckMoveSpeed();

        Moveforward();

        CheckSpawnStage();

    }
    
    public void CheckUserInput()
    {
      
        ////////////////////////////////////////// TEST PLAYER SPEED ////// START

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SpeedPlus();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SpeedDown();
        }
       

        ////////////////////////////////////////// TEST PLAYER SPEED ////// END

    }


    ////////////////////////////////////////// TEST PLAYER SPEED ////// START
    public void SpeedPlus()
    {
        PlayerSpeed += 1;
    }

    public void SpeedDown()
    {
        PlayerSpeed -= 1;
    }
    ////////////////////////////////////////// TEST PLAYER SPEED ////// END




    public void Moveforward()
    {
        transform.position -= new Vector3(0, 0, StageSpeed);
    }

    public void CheckMoveSpeed()
    {
        
            speedCon = CameraRig.GetComponent<SpeedController>();
            speed = speedCon.speed;
            SpeedMultiplier = speedCon.SpeedMultiplier;
            PlayerSpeed = speed;
       
        

        StageSpeed = SpeedMultiplier * PlayerSpeed;
    }

    /*   GET VALUE FROM AVATAR TO MAKE DIFFERENT SPEED
    public void CheckMainSpeed()
    {
        MainSpeed = int GetFromAvatar;
    }

    */

    public void CheckSpawnStage()
    {
        if (this.transform.position.z <= 0)
        {
            if (C_Spawn == true)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        Instantiate(SpawnStage01, new Vector3(0, 0, 180), Quaternion.identity);
                        C_Spawn = false;
                        print("CASE 0 ACTIVATE");
                        break;
                              
                    case 1:
                        Instantiate(SpawnStage02, new Vector3(0, 0, 180), Quaternion.identity);
                        C_Spawn = false;
                        print("CASE 1 ACTIVATE");
                        break;
                    case 2:
                        Instantiate(SpawnStage03, new Vector3(0, 0, 180), Quaternion.identity);
                        C_Spawn = false;
                        print("CASE 2 ACTIVATE");
                        break;
                }
            }
        }
        if (this.transform.position.z <= -120)
        {
            if (C_Spawn == false)
            {
                
                Destroy(this.gameObject);
                C_Spawn = true;
            }
        }
    }




    //////////SPEED SECTOR///////////

    public void UpdateSpeedC0()
    {
        PlayerSpeed = 0;
    }
    public void UpdateSpeedC1()
    {
        PlayerSpeed = 1;
    }
    public void UpdateSpeedC2()
    {
        PlayerSpeed = 2;
    }
    public void UpdateSpeedC3()
    {
        PlayerSpeed = 3;
    }
    public void UpdateSpeedC4()
    {
        PlayerSpeed = 4;
    }
    public void UpdateSpeedC5()
    {
        PlayerSpeed = 5;
    }
    public void UpdateSpeedC6()
    {
        PlayerSpeed = 6;
    }
    public void UpdateSpeedC7()
    {
        PlayerSpeed = 7;
    }
    public void UpdateSpeedC8()
    {
        PlayerSpeed = 8;
    }
    //////////SPEED SECTOR///////////


    
}

