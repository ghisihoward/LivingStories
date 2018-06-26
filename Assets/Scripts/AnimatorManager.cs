using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour {

	private GameManager gameManager;
	private CameraManager cameraManager;

	// Use this for initialization
	void Awake () {
		cameraManager = GameObject.FindWithTag ("CameraManager").GetComponent<CameraManager> ();
		gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	}

	public void InTheDark () {
		cameraManager.Flashing();
	}

	public void InTheLight () {
		gameManager.ResetGame();
	}
}
