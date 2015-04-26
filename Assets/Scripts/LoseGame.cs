using UnityEngine;
using System.Collections;

public class LoseGame : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Invoke ("LoadLevel", 3f);
	}
	
	void LoadLevel(){
		Application.LoadLevel ("CongaScene");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}