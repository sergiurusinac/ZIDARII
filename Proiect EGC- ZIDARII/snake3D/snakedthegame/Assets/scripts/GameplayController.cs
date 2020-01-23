using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//spaunezi mancarea
public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;


    public GameObject fruit_pickup;

    private float min_x = -4.25f, max_x = 4.45f, min_y = -2.34f, max_y = 2.24f;
    private float poz_z = 6f;

    private Text score_text;
    private int scoreCount;

    void Awake()//awake in fata lui start
    {
        MakeInstance();
    }

    void Start()
    {

        score_text = GameObject.Find("Score").GetComponent<Text>();
        Invoke("StartSpawn", 0.5f);
    }

    void MakeInstance()
    {

        if( instance == null)
        {
            instance = this;
        }
    }

    void StartSpawn()
    {
        StartCoroutine(SpawnPickups());
    }

    public void CancelSpawn()
    {
        CancelInvoke("StartSpawn");
    }

    IEnumerator SpawnPickups()
    {
        yield return new WaitForSeconds(Random.RandomRange(1f, 1.5f));//asteapta sa apara alt fruct

        if(Random.Range(0, 10) >= 2)
        {

            Instantiate(fruit_pickup, new Vector3(Random.Range(min_x, max_x), Random.Range(min_y, max_y), poz_z), Quaternion.identity);//nu depaseste mapa
        }

        Invoke("StartSpawn", 0f);//update
             
    }

    public void score()
    {
        scoreCount++;
        score_text.text = "Scoreul tau: " + scoreCount;
    }

}//end
