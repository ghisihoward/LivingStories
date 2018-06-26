using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	public AudioClip buttonSFX, buttonLockSFX;
	public AudioClip[] correctSFXs;
	private GameObject gameManager;
	private float volume;

	void Awake () {
		gameManager = GameObject.FindWithTag ("GameManager");
	}

	public void playButtonSFX () {
		this.GetComponent <AudioSource> ().clip = buttonSFX;
		this.GetComponent <AudioSource> ().pitch = Random.Range(0.9f, 1.1f);
		this.GetComponent <AudioSource> ().Play ();
	}

	public void checkAndPlayDailySFX () {
		if (gameManager.GetComponent<GameManager> ().playedDaily){
			this.GetComponent <AudioSource> ().clip = buttonLockSFX;
			this.GetComponent <AudioSource> ().pitch = Random.Range(0.95f, 1.05f);
			this.GetComponent <AudioSource> ().Play ();
		} else {
			this.playButtonSFX();
		}
	}

	public void playCorrectSFX () {
		this.GetComponent <AudioSource> ().clip = correctSFXs[Random.Range(0, correctSFXs.Length)];
		this.GetComponent <AudioSource> ().pitch = Random.Range(0.9f, 1.1f);
		this.GetComponent <AudioSource> ().Play ();
	}

	public void ToggleSound (bool toggle) {
		if (toggle) {
			volume = 1;
		} else {
			volume = 0;
		}
		this.GetComponent <AudioSource> ().volume = volume;
	}
}
