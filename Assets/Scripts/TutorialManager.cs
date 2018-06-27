using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

	private int phase = 0;
	private GameObject cameraManager;

	public void Awake () {
		cameraManager = GameObject.FindWithTag ("CameraManager");
	}

	public void Advance () {
		if (phase < 2){
			this.GetComponent<Animator> ().SetTrigger("Advance");
			phase += 1;
		} else {
			phase = 0;
			cameraManager.GetComponent<CameraManager> ().LeaveTutorial ();
		}
	}
}
