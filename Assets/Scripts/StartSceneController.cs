using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour {

    public GameObject left;
    public GameObject right;
    public Slider progress;
    public Text next;
    public int hold = 3;
    public float Movespeed = 0.05f;
    public GameObject sound;
    public GameObject Cage;
    public AudioSource audioSrc;
    public GameObject easy;
    public Text easytext;
    public Scrollbar easytoggle;

    private SteamVR_TrackedController tleft;
    private SteamVR_TrackedController tright;
    

    private float timer;
    private float step;
    private GameObject scenemanager;
    private bool soundplay = false;
    private bool iseasymodeon;
    private bool isMenuStillPress;
    // Use this for initialization
    void Start () {
        tleft = left.GetComponent<SteamVR_TrackedController>();
        tright = right.GetComponent<SteamVR_TrackedController>();
        
        scenemanager = GameObject.Find("SceneManager");
    }
    private void Awake()
    {
        soundplay = false;
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        step = 100 / (hold / Time.deltaTime);
        if (tleft.triggerPressed || tright.triggerPressed)
        {
            timer += Time.deltaTime;
            if (timer <= hold)
            {
                progress.value += step;
            }
            else
            {

                //load next scene


                if (Cage.transform.position.y > 4.5)
                {

                    scenemanager.GetComponent<SceneLoader>().LoadScene("StageTest_01");
                    next.text = "LOAD NEXT SCENE";

                    timer = 0;
                }
                else
                {
                    if (!soundplay)
                    {
                        sound.GetComponent<SoundController>().PlaySound("Click");
                        soundplay = true;
                    }
                    Cage.transform.position += new Vector3(0, Movespeed, 0);
                }


            }
        }
        else
        {
            timer = 0;
            progress.value = 0;
        }
        
       if(tleft.menuPressed || tright.menuPressed)
        {
            
            if (!iseasymodeon && !isMenuStillPress)
            {
                iseasymodeon = true;
                isMenuStillPress = true;
                easytoggle.value = 1;
                easytext.text = "Easy Mode : ON";
                easy.GetComponent<EasyModeHandle>().easymode = true;
            }
            else if(iseasymodeon && !isMenuStillPress)
            {
                iseasymodeon = false;
                isMenuStillPress = true;
                easytoggle.value = 0;
                easytext.text = "Easy Mode : OFF";
                easy.GetComponent<EasyModeHandle>().easymode = false;

            }
           
        }
        else
        {
            isMenuStillPress = false;
        }
    }
    
}
