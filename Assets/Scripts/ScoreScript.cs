using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

	public GUIText scoreText;
	public static int score;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}
}
