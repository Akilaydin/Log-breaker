using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnApplesAndKnives : MonoBehaviour
{
    public static SpawnApplesAndKnives instance;

    [Header("Apples spawning")]
    [SerializeField]
    private GameObject apple;
    [SerializeField]
    private AppleObject appleObject; //Scriptable object reference.

    [Header("Knives spawning")]
    [SerializeField]
    private GameObject knifeToLog;
    [Header("Log spawning")]
    [SerializeField]
    private GameObject logEngine;
    [SerializeField]
    private GameObject log;

    [SerializeField]
    private Transform[] spawnPositions; //an array of random spawn positions around the log.


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateNewLog()
    {
        GameObject logObj = (GameObject)Instantiate(log, new Vector2(0, 2), Quaternion.identity); //Creating a new Log
        GameObject.Find("LogEngine").GetComponent<WheelJoint2D>().connectedBody = logObj.GetComponent<Rigidbody2D>(); //Attatching created log to the Engine.
        logObj.transform.SetParent(GameObject.Find("LogEngine").transform); //Attatching created log to the Engine.
        int[] points = GeneratePoints();
        CreateKnives(points[0]);
        CreateApple(points[1]);

    }

    //Generates 2 points for apple and for knife, then compares them.
    //If points are equal, it generates them again to prevent apple and knife spawning at the same point. 
    //points[0] is for the knife. points[1] is for the apple.
    private int[] GeneratePoints()
    {
        int[] points = new int[2];
    Generating:
        points[0] = Random.Range(0, spawnPositions.Length);
        points[1] = Random.Range(0, spawnPositions.Length);

        if (points[0] == points[1])
        {
            goto Generating;
        }
        else
        {
            return points;
        }
    }
    
    private void CreateKnives(int posIndex)
    {
        int knivesToSpawnCount = Random.Range(1, 3);
        for (int i = 0; i < knivesToSpawnCount; i++)
        {
            GameObject knifeObj = (GameObject)Instantiate(knifeToLog, spawnPositions[posIndex].position, spawnPositions[posIndex].rotation); //Spawn a special knife in the spawn position.
        }
    }

    private void CreateApple(int posIndex)
    {
        float random = Random.Range(0f, 1f);
        if (appleObject.probabilityToAppear / 100 > random)
        {
            Instantiate(apple, spawnPositions[posIndex].position, Quaternion.identity);
        }
        else
        {
            return;
        }
    }
}
