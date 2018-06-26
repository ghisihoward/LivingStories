using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip menuMusic, gameMusic, tutorialMusic;
	private float volume;

	public void ChangeMusic (int i) {
		if (i == 1) {
			this.GetComponent <AudioSource> ().clip = menuMusic;
		} else if (i == 2) {
			this.GetComponent <AudioSource> ().clip = gameMusic;
		} else if (i == 3) {
			this.GetComponent <AudioSource> ().clip = tutorialMusic;
		}

		this.GetComponent <AudioSource> ().Play ();
	}

	public void ToggleMusic (bool toggle) {
		if (toggle) {
			volume = 1;
		} else {
			volume = 0;
		}
		this.GetComponent <AudioSource> ().volume = volume;
	}
}
