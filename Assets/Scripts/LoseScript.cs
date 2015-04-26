using UnityEngine;
using System.Collections;

public class LoseScript : MonoBehaviour {

	public void StartGame()
	{
		ZombieController.lives = 3;
		Application.LoadLevel ("CongaScene");
	}
}