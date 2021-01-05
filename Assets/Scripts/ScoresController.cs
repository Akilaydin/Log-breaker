using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour
{
    public static ScoresController instance;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text applesText;
    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void RefreshScore(int gameScore, int appleScore){

        scoreText.text = gameScore.ToString();
        applesText.text = appleScore.ToString();
    }
}
