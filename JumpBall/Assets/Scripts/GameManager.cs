using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int currentScore;
    public int currentLevel = 0;
    public static GameManager singleton;
    // Start is called before the first frame update
    void Awake()
    {
        //saber si hay un gamemanager
        if (singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("HighScore");
    }

    public void NextLevel()
    {
        Debug.Log("Pasamos de nivel");
        currentLevel++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HellixController>().LoadStage(currentLevel);

    }

    public void RestartLevel()
    {
        Debug.Log("Reiniciar livel al perder");
        singleton.currentScore = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HellixController>().LoadStage(currentLevel);
    }

    public void AddScore(int scoretoAdd)
    {
        currentScore += scoretoAdd;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore",currentScore);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
