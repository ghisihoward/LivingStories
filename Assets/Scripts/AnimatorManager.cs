using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour {

	private CameraManager cameraManager;

	// Use this for initialization
	void Start () {
		cameraManager = GameObject.FindWithTag ("CameraManager").GetComponent<CameraManager> ();
	}

	public void FinishAnimationIntro () {
		cameraManager.SwitchCamera (1);
	}
}
