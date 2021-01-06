using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{

    public static GameController instance { get; private set; }
    public GameUI gameUI { get; private set; }
    [HideInInspector]
    public bool canThrow = true;
    [SerializeField]
    private GameObject logEngine;
    [SerializeField]
    private float throwDelay = 0.3f;
    [HideInInspector]
    public int gameScore;
    [HideInInspector]
    public int appleScore;

    [Header("Exploding")]
    [SerializeField]
    private GameObject log;
    private Explodable explodable;

    [Header("KnivesSpawning")]
    [SerializeField]
    private Vector2 knifeSpawnPos;
    [SerializeField]
    private GameObject knife;
    [SerializeField]
    private GameObject knifeToLog;
    [SerializeField]
    private int knivesCount;

    [Header("ApplesSpawning")]
    [SerializeField]
    private GameObject apple;
    [SerializeField]
    private AppleObject appleObject;

    private void Awake()
    {
        explodable = log.GetComponent<Explodable>();
        instance = this;
        gameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        gameUI.SetKnivesCount(knivesCount);
        KnifeSpawn();
        appleScore = Database.instance.LoadAppleScore();
        gameScore = 0;
    }


    public void OnAccurateKnifeHit()
    {
        gameScore++;
        if (gameScore > Database.instance.LoadGameScore())
        {
            Database.instance.SaveGameScore(gameScore);
        }

        ScoresController.instance.RefreshScore(gameScore, appleScore);
        if (knivesCount > 0)
        {
            KnifeSpawn();
        }
        else
        {
            GameOver(true);
        }
    }

    public void OnAppleHit()
    {
        appleScore++;
        Database.instance.SaveApplesScore(appleScore);
    }

    private void KnifeSpawn()
    {
        knivesCount--;
        Instantiate(knife, knifeSpawnPos, Quaternion.identity);
    }
    public IEnumerator MakeDelayForThrow()
    {
        canThrow = false;
        yield return new WaitForSecondsRealtime(throwDelay);
        canThrow = true;
    }
    public void GameOver(bool win)
    {
        Handheld.Vibrate();
        StartCoroutine(GameOverCoroutine(win));
    }

    private IEnumerator GameOverCoroutine(bool win)
    {
        if (win)
        {
            LogExplosion();
            yield return new WaitForSecondsRealtime(0.4f);

            GameObject logObj = (GameObject)Instantiate(log, new Vector2(0, 2), Quaternion.identity); //Creating a new Log
            GameObject.Find("LogEngine").GetComponent<WheelJoint2D>().connectedBody = logObj.GetComponent<Rigidbody2D>();
            logObj.transform.SetParent(logEngine.transform);
            CreateKnives(logObj);
            InstantiateApple(logObj);
        }
        else
        {
            gameUI.ShowRestartButton();
        }
    }
    private void LogExplosion()
    {
        Destroy(GameObject.FindGameObjectWithTag("Log"));

        // explodable.explode();
        // ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
        // ef.doExplosion(transform.position);
    }

    private void CreateKnives(GameObject logObj)
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        { //Generating knives in the log
            GameObject knifeObj = (GameObject)Instantiate(knifeToLog, knifeToLog.transform.position, Quaternion.identity);
            knifeObj.GetComponent<Knife>().isActive = false;
            //knifeObj.transform.SetParent(logObj.transform);
           
        }
    }
    private void InstantiateApple(GameObject logObj)
    {
        float random = Random.Range(0f, 1f);
        if (appleObject.probabilityToAppear / 100 > random)
        {
            Instantiate(apple, apple.transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }
    }
    private void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevelIndex);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
