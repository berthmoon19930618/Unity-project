using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFunction : MonoBehaviour {
    public GameObject Emeny;
    public GameObject EmenyB;
    public float time;
    public float Generator = 1f;

    public Text ScoreText;
    public int Score = 0;
    public static GameFunction Instance;

    public GameObject GameTitle;
    public GameObject Manual;
    public GameObject VictoryTitle;
    public GameObject GameOverTitle;
    public GameObject PlayButton;
    public GameObject RestartButton;
    public GameObject QuitButton;
    public bool IsPlaying = false;

    void Start ()
    {
        Manual.SetActive(false);
        GameOverTitle.SetActive(false);
        RestartButton.SetActive(false);
        Instance = this;
        VictoryTitle.SetActive(false);
    }
	
	void Update ()
    {
        if (Score == 100)
        {
            Generator = 0.7f;
        }
        if(Score == 200)
        {
            Generator = 0.6f;
        }
        if (Score == 300)
        {
            Generator = 0.5f;
        }
        if(Score == 400)
        {
            Generator = 0.4f;
        }
        if (Score == 500)
        {
            Generator = 0.2f;
        }
        if (Score == 800)
        {
            Generator = 0.1f;
        }
        if (Score == 1000)
        {
            VictoryTitle.SetActive(true);
            IsPlaying = false;
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
            GameObject[] tInvaderArray = GameObject.FindGameObjectsWithTag("Emeny");

            foreach (GameObject tGO in tInvaderArray)
            {
                Destroy(tGO);
            }
            GameObject[] tInvaderBArray = GameObject.FindGameObjectsWithTag("EmenyB");

            foreach (GameObject tGOB in tInvaderBArray)
            {
                Destroy(tGOB);
            }

        }
        time += Time.deltaTime;
        //if (time > Generator)
        if (time > Generator && IsPlaying == true)

        {
            Vector3 pos = new Vector3(Random.Range(2.5f, -2.5f), 4.5f, 0);
            Instantiate(Emeny, pos, Quaternion.identity);
            Vector3 posB = new Vector3(Random.Range(2f, -2f), 4.5f, 0);
            Instantiate(EmenyB, posB, Quaternion.identity);
            time = 0f;
        }
    }
    public void AddScore()
    {
        Score += 10;
        ScoreText.text = "Score: " + Score;
    }
    public void GameStart()
    {
        IsPlaying = true;
        GameTitle.SetActive(false);
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        Manual.SetActive(true);

    }
    public void GameOver()
    {
        IsPlaying =false;
        GameOverTitle.SetActive(true);
        RestartButton.SetActive(true);
        QuitButton.SetActive(true);
    }
    public void Restart()
    {
        Application.LoadLevel("Space");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
