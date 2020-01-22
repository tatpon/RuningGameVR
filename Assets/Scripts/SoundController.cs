using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioSource audioSrc;
    public AudioClip playerHit;
    public AudioClip click;
    public AudioClip gameover;
	// Use this for initialization
	void Awake () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "HIT":
                {
                    audioSrc.PlayOneShot(playerHit); 
                    break;
                }
            case "Click":
                {
                    audioSrc.PlayOneShot(click);
                    break;
                }
            case "GameOver":
                {
                    audioSrc.PlayOneShot(gameover);
                    break;
                }
        }
    }
}
