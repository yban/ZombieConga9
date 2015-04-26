using UnityEngine;
using System.Collections;

public class WinRestart : MonoBehaviour {

	public void StartGame()
	{
		ZombieController.lives = 3;
		Application.LoadLevel ("CongaScene");
	}
}
