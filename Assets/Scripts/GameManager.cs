using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int lifeCount = 3;
    public int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoresText;

    void Start()
    {
        livesText.text = lifeCount.ToString();
        scoresText.text = score.ToString();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void DecreaseLife()
    {
        if (lifeCount > 1)
        {
            TakeLife();
        }
        else
        {
            RestartGame();
        }
    }

    public void PlusScore()
    {
        score += 100;
        scoresText.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<InteractableManager>().Restart();
    }

    public void TakeLife()
    {
        lifeCount -= 1;
        StartCoroutine(RestartCurrentScene());
        livesText.text = lifeCount.ToString();
    }
    
    IEnumerator RestartCurrentScene()
    {
        yield return new WaitForSecondsRealtime(1);
        
        int currScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScene);
    }
}
