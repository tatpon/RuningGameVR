using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedController : MonoBehaviour {

    //Declare for using other script variable
    private LeftControllerVelocity left; 
    private RightControllerVelocity right;
    private StageController stage;
    private SoundController sound;
    
    //GameObject Insert
    [Tooltip("Insert Left Contoller (GameObj)")]
    public GameObject leftDevice;
    [Tooltip("Insert Right Contoller (GameObj)")]
    public GameObject rightDevice;
    public GameObject CameraRig;
    public GameObject UI;
    public GameObject soundmanager;

    //Adjustable Parameters
    [Range(0,10)]
    [Tooltip("Player's Speed")]
    public int speed = 0;
    [Tooltip("Default at 0.03")]
    public float SpeedMultiplier = 0.03f;

    [Range(1,3)]
    [Tooltip("Increase Step(s)")]
    public int steps = 1;
    [Range(1,3)]
    [Tooltip("Decrease Step(s)")]
    public int decrease = 1;
    [Range(1,10)]
    public int lessSpeed = 4;
    [Range(1,10)]
    public int maxSpeed = 8;
    [Range(1, 10)]
    [Tooltip("When player stop, How many second until Decrease HP")]
    public int NoMoving = 2;
    [Tooltip("Minimum Threshold before speed decrease")]
    public int minT = 30;

    [Tooltip("Threshold before speed increase")]
    public int maxT = 80;

    [Tooltip("Delta Time (Delay time)")]
    public float Delta = 0.5f;

    public bool UserSwing = true;
    [Range(0,10)]
    public int SetSpeed = 3;

    [Range(0,3)]
    public int HitStep = 1;
    [Tooltip("Speed Up Over Time")]
    public bool SpeedUpOverTime = true;
    [Range(0,100)]
    [Tooltip("Speed Up Timer")]
    public int speedUpTime = 5;
    [Tooltip("SpeedMuitiplier Step")]
    public float speedUpStep = 0.02f;

    //Private Vairable
    private float VelL;
    private float VelR;
    private float currentDelta = 0;
    private bool enterCol = false;
    [HideInInspector]
    public bool gameover = false;
    private float starttime;
    private float currenttime;
    private float stoppedtime;
    private bool stopped = false;
    private bool getspeed = false;
    private float gettime = 0;
    private float timer;
    [HideInInspector]
    public int currentLevel = 1;
    private int levelcal = 1;
    private float currentSlow;
    private float LvTimer;
    private float LvStepper;
    private float score;

    private float progress = 0;
    [HideInInspector]
    public bool isstart = false;
    public bool easymode;




    private void Awake()
    {
        starttime = CameraRig.GetComponent<EnemyController>().TimeUntilSpawn;
        currentSlow = NoMoving;
        sound = soundmanager.GetComponent<SoundController>();
        
        
    }
    void FixedUpdate()
    {
        
        starttime -= Time.deltaTime;
        if (starttime <= 0)
        {
            isstart = true;
            calculateScore();
        }

        if (gameover == false)
        {
            GetSpeed();

            CheckNomove();

            CheckLevel();

            if (Input.GetKeyUp(KeyCode.Space))
            {
                PlayerHit();
            }
            
        }
        else
        {
            GameOver();
        }
        
          
        
    }
       
    public void GetSpeed()
    {
        left = leftDevice.GetComponent<LeftControllerVelocity>();
        right = rightDevice.GetComponent<RightControllerVelocity>();

        VelL = left.AvgVelL;
        VelR = right.AvgVelR;
        currentDelta += Time.deltaTime;
        // print(VelL + " " + VelR); //Print Current AVGSpeed //For Debuging

        //print(currentDelta + " " + Delta);
        if (currentDelta >= Delta)
            {
                if (UserSwing == false) { speed = SetSpeed + 1; }
                if (VelL > maxT && VelR > maxT) //Check Increase
                {
                    if (speed < 10)
                    {
                        //increase speed
                        speed = speed + steps;

                        if (speed > 10)
                        {
                            //Check Speed not more than 10
                            speed = 10;
                        }
                    }
                }
                else if (VelL < maxT && VelL > minT || VelR < maxT && VelR > minT) //Check Maintain
                {
                    if (speed == 0)
                    {
                        //When Fully Stopped
                        speed++;
                    }
                    //maintain speed
                }
                else if (VelL < minT && VelR < minT) //Check Decrease
                {
                    if (speed != 0)
                    {
                        if (speed < 0)
                        {
                            //Check Speed not less than 0
                            speed = 0;
                        }
                        //decrease speed
                        speed = speed - decrease;
                    }
                }
               
                currentDelta = 0; //Reset Delta
            }
        else
            {
            
                //currentDelta++; //Increase Current Delta
            }
       
        
       
        //print(speed);
        /*To get speed parameter using GetComponet<> from this scrpit by referencing CameraRig Object*/
    }
 

    public void PlayerHit()
    {
        if (speed >= HitStep && speed > 0 && enterCol == false && isstart)
        {
            speed = speed - HitStep;
        }
        else if(speed <= HitStep)
        {
            speed = 0;
        }
        enterCol = true;
        //print(speed);
        CameraRig.GetComponent<EnemyController>().TransformEnemy();
        UI.GetComponent<UIController>().HitRemain();

        sound.PlaySound("HIT");

        leftDevice.GetComponent<HapticController>().Haptic(1000); //haptic L
        rightDevice.GetComponent<HapticController>().Haptic(1000); //haptic R

        if(currentLevel == 2)
        {

        }
    }
    public void PlayerSlow()
    {
        CameraRig.GetComponent<EnemyController>().TransformEnemy();
        UI.GetComponent<UIController>().HitRemain();
    }
    public void CheckNomove()
    {
        
        if (speed <= lessSpeed && isstart && gameover == false)
        {
            
            
            
            if (stopped)
            {
                //Debug.Log("NOMOVING = " + currenttime + "|" + stoppedtime);
                currenttime += Time.deltaTime;
                currentSlow -= Time.deltaTime;
                UI.GetComponent<UIController>().tooSlow(currentSlow);
                if (currenttime - stoppedtime > NoMoving)
                {
                    PlayerSlow();
                    stopped = false;
                    currentSlow = NoMoving;
                    leftDevice.GetComponent<HapticController>().Haptic(500); //haptic L
                    rightDevice.GetComponent<HapticController>().Haptic(500); //haptic R
                    
                }

            }
            else
            {
                stoppedtime = currenttime;
                stopped = true;
            }
            

        }
        else
        {
            currentSlow = NoMoving;
            stopped = false;
            UI.GetComponent<UIController>().ClearWarning();

        }
        

    }
    public void ExitCollision()
    {
        enterCol = false;
    }
    public void GameOver()
    {
        gameover = true;
        speed = 0;
        UserSwing = false;
        SetSpeed = 0;
        UI.GetComponent<UIController>().GameOver();
        
        
        
    }
    public void SpeedUp()
    {
        SpeedMultiplier = SpeedMultiplier + speedUpStep;
        
    }
    public void CheckLevel()
    {
        LvStepper = 100 / (speedUpTime / Time.deltaTime);

        //Debug.Log("Step : "+LvStepper+" | " + Time.deltaTime);
        if(speed >= maxSpeed && isstart )
        {
            LvTimer += Time.deltaTime;
            progress = progress + LvStepper;
            //Debug.Log("Progress : " + progress);
            if (LvTimer <= speedUpTime)
            {
                //Debug.Log(LvTimer);
                UI.GetComponent<UIController>().ProgressPlus(progress);
            }
            else
            {
                Debug.Log("LevelUp");
                UI.GetComponent<UIController>().increaseLevel();
                if (SpeedUpOverTime)
                {
                    SpeedUp();
                }
                LevelUp();
                LvTimer = 0;
                progress = 0;
            }
        }
    }
    public void LevelUp()
    {
        currentLevel++;
        levelcal++;
        if (currentLevel > 2)
        {
            currentLevel = 1;
        }
        
    }
    public void calculateScore()
    {
        if (!gameover)
        {
            score += Time.deltaTime * levelcal;
            UI.GetComponent<UIController>().ScoreIndecator(score);
        }
    }
    public void setEasyMode(bool iseasy)
    {
        easymode = iseasy;
    }
}
