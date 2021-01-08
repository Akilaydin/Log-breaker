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
    private float throwDelay = 0.3f;
    [HideInInspector]
    public int gameScore;
    [HideInInspector]
    public int appleScore;
    [HideInInspector]
    public int levelIndex;
    [Header("BossFights")]
    [SerializeField]
    [Tooltip("Begin boss fight a every N + 1 level")]
    private int bossLevelIndex = 4;

    [Header("Exploding")]
    [SerializeField]
    private GameObject cameraShakeExplosion;
    [SerializeField]
    private GameObject destroyableLog;

    [Header("KnivesSpawning")]
    [SerializeField]
    private Vector2 knifeSpawnPos;
    [SerializeField]
    private GameObject knife;
    private int knivesCount;
    [SerializeField]
    private int minKnivesAtLevel,maxKnivesAtLevel;
    public bool isGameActive;

    

    private void Awake()
    {
        instance = this;
        gameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        isGameActive = true;
        Time.timeScale = 1;
        knivesCount = Random.Range(minKnivesAtLevel,maxKnivesAtLevel);
        gameUI.SetKnivesCount(knivesCount);
        KnifeSpawn();
        appleScore = Database.instance.LoadAppleScore();
        gameScore = 0;
        levelIndex = 1;
        ScoresController.instance.RefreshScore(gameScore, appleScore);
        Vibration.Init();
    }

  
    public void OnAccurateKnifeHit()
    {
        
        gameScore++;

        if (gameScore > Database.instance.LoadGameScore()) //To save only the highest score.
        {
            Database.instance.SaveGameScore(gameScore);
        }

        ScoresController.instance.RefreshScore(gameScore, appleScore);

        if (knivesCount > 0) //If there are remaining knives to throw.
        {
            KnifeSpawn();
        }
        else
        {
            GameOver(true);
        }
    }
    private void CameraShake(){
        GameObject cameraShaker = Instantiate(cameraShakeExplosion,transform.position,Quaternion.identity);
        Destroy(cameraShaker,5);
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
        Vibration.Vibrate(300);
        StartCoroutine(GameOverCoroutine(win));
    }

    private IEnumerator GameOverCoroutine(bool win)
    {
        
        if (win)
        {
            CameraShake();
            LogExplosion();
            yield return new WaitForSecondsRealtime(0.5f);
            StartNewLevel();
        }
        else
        {
            isGameActive = false;
            gameUI.ShowRestartButton();
        }
    }

    private void StartNewLevel(){
        if (isGameActive && levelIndex % bossLevelIndex != 0){ //If not boss fight
            knivesCount = Random.Range(minKnivesAtLevel,maxKnivesAtLevel);
            gameUI.SetKnivesCount(knivesCount);
            KnifeSpawn();
            SpawnApplesAndKnives.instance.CreateNewLog();
            levelIndex++;
            gameUI.IncreaseLevel();
        } 
        else if (isGameActive && levelIndex % bossLevelIndex == 0) //If boss fight
        {
            Debug.Log("Boss fight!");
            knivesCount = Random.Range(minKnivesAtLevel * 2,maxKnivesAtLevel * 2);
            gameUI.SetKnivesCount(knivesCount);
            KnifeSpawn();
            SpawnApplesAndKnives.instance.CreateNewLog();
            levelIndex++;
            gameUI.IncreaseLevel();
            
        }
    }
    private void LogExplosion()
    {
        Vector3 logPosition = GameObject.FindGameObjectWithTag("Log").transform.position;
        Destroy(GameObject.FindGameObjectWithTag("Log"));
        Instantiate(destroyableLog,logPosition,Quaternion.identity);
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
