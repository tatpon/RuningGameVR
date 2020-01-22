using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text speed;
    public Text Hit;
    public Text spawmUI;
    public Text level;
    public Text warning;
    public Image flash;
    public CanvasGroup OutOfRange;
    public CanvasGroup GameOverCanvas;
    public CanvasGroup MainUI;
    public Text Progrssion;
    public Text GameOverScore;
    public Text Easy;
    public float flashtime = 0.2f;

    public GameObject left;
    public GameObject right;
    public int triggertime;

    private int currentlevel = 1;
    private float countdown;
    private int ss;
    private int remain;
    public GameObject cam;
    private int maxSpeed;
    private int lessSpeed;
    private float progress = 0;
    private bool spawn = false;
    private float timer = 0;
    private GameObject scenemanager;
    private int currentscore = 0;
    private bool iseasy;
    // Use this for initialization
    void Awake()
    {
        remain = cam.GetComponent<EnemyController>().TotalHit;
        Hit.text = "Hit Left = " + remain.ToString();
        countdown = cam.GetComponent<EnemyController>().TimeUntilSpawn;
        maxSpeed = cam.GetComponent<SpeedController>().maxSpeed;
        lessSpeed = cam.GetComponent<SpeedController>().lessSpeed;
        scenemanager = GameObject.Find("SceneManager");
        

    }
    IEnumerator FlashHit()
    {
        while (flash.GetComponent<CanvasGroup>().alpha > 0)
        {
            flash.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            yield return null;

        }
        yield return null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        iseasy = cam.GetComponent<SpeedController>().easymode;
        ss = cam.GetComponent<SpeedController>().speed;
        if (ss >= maxSpeed)
        {
            speed.color = Color.green;
        }
        else if (ss <= lessSpeed)
        {
            speed.color = Color.red;
        }
        else speed.color = Color.white;
        speed.text = ss.ToString();
        if (spawn == false)
        {
            SpawnIndicator();
        }
        if (iseasy)
        {
            Easy.text = "Easy Mode";

        }
        else
        {
            Easy.text = "";
        }
        
    }
    public void TextHit()
    {
       
        //speed.text = "HIT";
    }
    public void HitRemain()
    {
        flash.GetComponent<CanvasGroup>().alpha = 0.5f;
        StartCoroutine(FlashHit());
        
        remain--;
        Debug.Log("UI : " + remain);
        if (remain <= 0)
        {
            Hit.text = "GAME OVER";
        }
        else
        {
            
            Hit.text = "Hit Left = " + remain.ToString();
        }
    }
    public void SpawnIndicator()
    {
        
            spawmUI.text = "Enemy Spawn in : "+ Mathf.RoundToInt(countdown).ToString();
            countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            spawmUI.text = "Spawn!!";
            spawn = true;
        }

    }public void increaseLevel()
    {
        currentlevel++;
        level.text = "Level : " + currentlevel;
        
    }
    public void tooSlow(float tooSlow)
    {
        warning.text = "TOO SLOW!! "+ "(" +Mathf.RoundToInt(tooSlow).ToString()+")";
    }
    public void ClearWarning()
    {
        warning.text = "";
    }
    public void OutofRange(float counter)
    {
        OutOfRange.alpha = 1;
        if(counter <= 0)
        {
            OutOfRange.GetComponentInChildren<Text>().text = "Game Over : Out Of Range Penalty";
        }
        else OutOfRange.GetComponentInChildren<Text>().text = "Go Back ("+counter.ToString("F1")+")";
    }
    IEnumerator ClearOut()
    {
        while (OutOfRange.alpha > 0)
        {
            OutOfRange.alpha -= Time.deltaTime;
            yield return null;

        }
        yield return null;
    }
    public void ClearOutOfRange()
    {
        StartCoroutine(ClearOut());
    }
    public void GameOver()
    {
        StartCoroutine(GameOverFade());
        MainUI.alpha = 0;
        GameOverScore.text = "Score : " + currentscore;
        if (left.GetComponent<SteamVR_TrackedController>().triggerPressed || right.GetComponent<SteamVR_TrackedController>().triggerPressed)
        {
            timer += Time.deltaTime;
            if (timer <= triggertime)
            {
                //progress bar   
            }
            else
            {
                //load start scene
                Debug.Log("load start screen");
                scenemanager.GetComponent<SceneLoader>().LoadScene("StartScene");
            }

        }
        else
        {
            timer = 0;
        }
    }
    IEnumerator GameOverFade()
    {
        while (GameOverCanvas.alpha < 1)
        {
            GameOverCanvas.alpha += Time.deltaTime/2;
            yield return null;

        }
        yield return null;
    }
    public void ProgressPlus(float percentage)
    {
       
        Progrssion.text = Mathf.RoundToInt(percentage) + "%";
       
       // Debug.Log(progress);
    }
    public void ScoreIndecator(float score)
    {
        spawmUI.text = "Score : " + Mathf.RoundToInt(score) * 100;
        currentscore = Mathf.RoundToInt(score) * 100;
    }
}
