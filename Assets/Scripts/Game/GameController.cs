using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    public GameUI gameUI { get; private set; }
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
    private int knivesCount;

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
    }

    public void OnAccurateKnifeHit()
    {
        gameScore++;
        ScoresController.instance.RefreshScore(gameScore,appleScore);
        Database.instance.SaveGame();
        if (knivesCount > 0)
        {
            
            KnifeSpawn();
        }
        else
        {
            GameOver(true);
        }
    }

    
    private void LogExplosion(){
        explodable.explode();
		ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
		ef.doExplosion(transform.position);
    }
    private void KnifeSpawn()
    {
        knivesCount--;
        Instantiate(knife, knifeSpawnPos, Quaternion.identity);
    }
    public IEnumerator MakeDelayForThrow(){
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
            Instantiate(logEngine,new Vector2(0,2),Quaternion.identity);

            
        }
        else
        {
            gameUI.ShowRestartButton();
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
