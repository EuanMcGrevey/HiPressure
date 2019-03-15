using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelfHiScore : MonoBehaviour {

    public Text display;

	// Use this for initialization
	void Start () {
        display.text = "Your high score: " + UIManager.getHiScoreScore().ToString() + " in " + UIManager.getHiScoreTime().ToString() + " seconds\n";
        display.text += "Your best time: " + UIManager.GetLoTimeTime().ToString() + " with " + UIManager.GetLoTimeScore().ToString() + " score";
	}
	
}
