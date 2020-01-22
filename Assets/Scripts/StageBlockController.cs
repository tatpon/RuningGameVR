using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBlockController : MonoBehaviour {

    public float Speed = 10f;
   // public float MainFreezeTime = 5f;
  //  private float FreezeTime = 0;

  //  private int CheckPosition = 0;
    public int LimitPositionUp = 10;
  //  public int LimitPositionDown = -1;

  //  private bool RandFreeze = true;
   // private bool MoveUp = true;
   // private bool MoveDown = false;



	// Use this for initialization
	void Start () {

        //       RandomFreezeTime();
        Speed = Random.Range(7f, 15f);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Speed * Time.time, LimitPositionUp), transform.position.z);

        // CheckLimitPosition();
        // CheckFreezeTime();
    }

  /*  public void ObjectMoveUp()
    {
        this.transform.position += new Vector3(0, Speed * Time.deltaTime, 0);

    }

    public void ObjectMoveDown()
    {
        this.transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);

    }

    public void CheckLimitPosition()
    {

        if(this.transform.position.y <= 10 || MoveUp == true)
        {
            ObjectMoveUp();

        }

        if (this.transform.position.y >= 0 || MoveDown == true)
        {
            ObjectMoveUp();

        }

    }

    public void CheckFreezeTime()
    {
        if(this.transform.position.y == LimitPositionUp)
        {
            MoveUp = false;
            RandomFreezeTime();
            MoveDown = true;
            return;
            

        }

        if (this.transform.position.y == LimitPositionDown)
        {
            MoveDown = false;
            RandomFreezeTime();
            MoveUp = true;
            return;   
        }
    }*/


/// <summary>
/// ///////////////////FREEZE SYSTEM////////////////////////
/// </summary>
  /*  public void RandomFreezeTime()
    {
        if(!RandFreeze)
        {
            FreezeTime = MainFreezeTime * Random.Range(0, 2);
            StartCoroutine(FreezePosition());
            return;
        }
        
    }

    IEnumerator FreezePosition()
    {
        RandFreeze = false;
        yield return  new WaitForSeconds(FreezeTime);
    }
     */
    /////////////////////////FREEZE SYSTEM END//////////////////////////
}
