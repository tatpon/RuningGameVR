using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliderCheck : MonoBehaviour {

    public GameObject cam;
    public GameObject UI;
    public float OutOfRangeTimer = 10;
    
    private bool isstart;


    ///  Color Change System

    public Material Stage2Mat;
    public Color NormalState;
    public Color HitState;



    /// </MCCS>

    //public GameObject RightContoroller;
    //public GameObject LeftContoroller;


	// Use this for initialization
	void Start () {
        //var HapDev = GetComponent<HapticController>();

        //RightContoroller = GameObject.Find("Controller (right)");
        //LeftContoroller = GameObject.Find("Controller(left)");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        isstart = cam.GetComponent<SpeedController>().isstart;
        


	}
    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.tag == "Barrier" && isstart)
        {
            Debug.Log("HITTTTTT");
            cam.GetComponent<SpeedController>().PlayerHit();
            //UI.GetComponent<UIController>().HitRemain();

            //HapticController hap = GetComponent<HapticController>();
            // hap.Haptic();
            Stage2Mat.color = HitState;
            Stage2Mat.SetColor("_EmissionColor", HitState);


        }
       
        
    }
    public void OnTriggerStay(Collider collision)
    {
        UI.GetComponent<UIController>().TextHit();
        if (collision.tag == "Restirct" && isstart)
        {
            OutOfRangeTimer -= Time.deltaTime;
            
            UI.GetComponent<UIController>().OutofRange(OutOfRangeTimer);
            if(OutOfRangeTimer <= 0)
            {
                cam.GetComponent<SpeedController>().GameOver();

            }
           
        }
        Stage2Mat.color = HitState;
        Stage2Mat.SetColor("_EmissionColor", HitState);
        //Debug.Log("Stay on :"+collision.name);
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Barrier" && isstart)
        {
            cam.GetComponent<SpeedController>().ExitCollision();

        }
        if (collision.tag == "Restirct" && isstart)
        {

            UI.GetComponent<UIController>().ClearOutOfRange();
            OutOfRangeTimer = 10;
        }
        Stage2Mat.color = NormalState;
        Stage2Mat.SetColor("_EmissionColor", new Color(0.0f, 255.0f, 255.0f, 255.0f));
    }
}
