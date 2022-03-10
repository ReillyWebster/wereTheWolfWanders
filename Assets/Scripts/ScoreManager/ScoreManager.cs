using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int currentScore, highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentScore = 0;

        highScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 500;

        UIManager.instance.currentScore.text = $"Score: {currentScore}";
        UIManager.instance.highScore.text = $"High Score: {highScore}";
    }

    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("highScore", highScore);
            UIManager.instance.highScore.text = $"High Score: {currentScore}";
        }
        UIManager.instance.currentScore.text = $"Score: {currentScore}";
    }
}
