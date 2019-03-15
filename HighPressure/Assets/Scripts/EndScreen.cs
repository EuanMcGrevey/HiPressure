using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EndScreen : MonoBehaviour {
	public Text display;
	// Use this for initialization
	void Start () {
        double t = UIManager.GetTime();
        int s = UIManager.GetScore();

        if (s > UIManager.getHiScoreTime())
        {
            UIManager.setHiScoreScore(s);
            UIManager.setLoTimeTime(t);
        }

        if (t < UIManager.GetLoTimeTime())
        {
            UIManager.setLoTimeTime(t);
            UIManager.setLoTimeScore(s);
        }

		display.text = "Your Score: " + s.ToString() + "\n Your Time: " + string.Format("{0:N2}",(Math.Truncate(t * 100)/100).ToString()) + " seconds";
	}
}
