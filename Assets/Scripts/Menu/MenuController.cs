using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text applesText;

    public void StartNewGame(){
        StartCoroutine(StartNewGameCoroutine());
    }

    private IEnumerator StartNewGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.35f);
        SceneManager.LoadScene(1);
    }

    private void Start() {
        LoadGameScores();
    }
    private void LoadGameScores(){
        applesText.text = Database.instance.LoadAppleScore().ToString();
        scoreText.text = "High score: " + Database.instance.LoadGameScore().ToString();

    }
}
