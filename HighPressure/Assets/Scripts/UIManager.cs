using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour {
    GameObject[] pauseObjects;
    static int score;
    private float nextTime = 0;
    private int interval = 1;

    static int hiScoreScore;
    static double hiScoreTime;
    static int loTimeScore;
    static double loTimeTime;


    private static Vector3[] possibleSpawns = new Vector3[3];

    public static double timer;
    public int target = 60;
      
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        if (timer == null) timer = 0.0;
        //pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        //hidePaused();

        hiScoreScore = 0;
        loTimeScore = 0;
        hiScoreTime = 3000.0;
        loTimeTime = 3000.0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.targetFrameRate != target)
            Application.targetFrameRate = target;

        timer += Time.deltaTime;

        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "map" && Time.time >= nextTime)
        {
            if(possibleSpawns[0] == new Vector3(0f, 0f, 0f)) {
                possibleSpawns[0] = new Vector3(-28.52f, 10.58f, 0);
                possibleSpawns[1] = new Vector3(-28.66f, -8.93f, 0);
                possibleSpawns[2] = new Vector3(28.57f, 0.61f, 0);
            }
            nextTime = Time.time + interval;
            GameObject z = GameObject.FindWithTag("Enemy");
                    Instantiate(z, GetRandomSpawnLocation(), 
                    new Quaternion(0f, 0f, 0f, 1));
        }
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public static void AddToScore(int amount)
    {
        GameObject ScoreDisplay = GameObject.FindWithTag("ScoreDisplay");
        score += amount;
        if(ScoreDisplay)
            ScoreDisplay.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        print(score);
    }

    public static int GetScore()
    {
        return score;
    }

    public static double GetTime()
    {
        return timer;
    }

    public static void ResetTime()
    {
        timer = 0.0;
    }

    public static void ResetScore()
    {
        score = 0;
    }

    public static Vector3 GetRandomSpawnLocation()
    {
        return possibleSpawns[UnityEngine.Random.Range(0,3)]; // otherwise System.Random
    }

    public Vector3[] GetSpawnLocations()
    {
        return possibleSpawns;
    }

    // leaderboard and hi score 

    public static int GetLoTimeScore()
    {
        return loTimeScore;
    }

    public static double GetLoTimeTime()
    {
        return loTimeTime;
    }

    public static int getHiScoreScore()
    {
        return hiScoreScore;
    }

    public static double getHiScoreTime()
    {
        return hiScoreTime;
    }

    public static void setHiScoreScore(int newscore)
    {
        hiScoreScore = newscore;
    }

    public static void setHiScoreTime(double newtime)
    {
        hiScoreTime = newtime;
    }

    public static void setLoTimeScore(int newscore)
    {
        loTimeScore = newscore;
    }

    public static void setLoTimeTime(double newtime)
    {
        loTimeTime = newtime;
    }

}
