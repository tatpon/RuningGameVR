using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject enemy;
    public GameObject Camrig;
    public GameObject UI;
    public float EnemySpawnDistance = -40f;

    public int TotalHit = 10;
    public int TimeUntilSpawn = 10;
    private StageController StageCon;

    private int distance;
    private float timer;
    private bool spawn;
    private float enemysteps;
    private GameObject spawnEnemy;
    private int remain;
   


    // Use this for initialization
    void Start () {
        enemysteps = Mathf.Abs(EnemySpawnDistance) / TotalHit;
        remain = TotalHit;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        
        if(timer > TimeUntilSpawn && spawn == false)
        {
            SpawnEnemy();
            
        }
       
        if(remain <= 0)
        {
            GameOver();
        }

       
        

    }
    public void SpawnEnemy()
    {
        spawnEnemy = Instantiate(enemy, new Vector3(0, 0, EnemySpawnDistance), Quaternion.identity);
        spawn = true;
    }
    public void TransformEnemy()
    {
        // transform.position = new Vector3(0, 0, EnemySpawnDistance - enemysteps);
        //print("HIIIIIITTTTTT FUCK YOU" + enemysteps);
        remain--;
        //print(remain);
        spawnEnemy.transform.position += new Vector3(0, 0, enemysteps);

        
    }
    public void GameOver()
    {
        Destroy(spawnEnemy);
        Camrig.GetComponent<SpeedController>().GameOver();


    }
}
