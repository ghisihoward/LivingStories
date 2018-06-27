using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private int currentScene = 0, nextScene = 1;

	private GameObject camIntro, camMenu, camGame, camOptions, camTotem;
	private GameObject[] cameras = new GameObject[5];

	private GameObject screenIntro, screenMenu, screenGame, screenOptions, screenTotem;
	private GameObject[] screens = new GameObject[5];

	private GameObject flashOverlay, HSManager, audioManager, storyTeller;

	private bool playedIntroAnimation = false, forcedSwitchIntro = false;

	public void Start () {
		camIntro = this.transform.Find ("CameraIntro").gameObject;
		camMenu = this.transform.Find ("CameraMenu").gameObject;
		camGame = this.transform.Find ("CameraGame").gameObject;
		camOptions = this.transform.Find ("CameraOptions").gameObject;
		camTotem = this.transform.Find ("CameraTotem").gameObject;
		cameras = new GameObject[] {camIntro, camMenu, camGame, camOptions, camTotem};

		screenIntro = GameObject.Find ("ScreenIntro");
		screenMenu = GameObject.Find ("ScreenMenu");
		screenGame = GameObject.Find ("ScreenGame");
		screenOptions = GameObject.Find ("ScreenOptions");
		screenTotem = GameObject.Find ("ScreenTotem");
		screens = new GameObject[] {screenIntro, screenMenu, screenGame, screenOptions, screenTotem};

		audioManager = GameObject.FindWithTag ("AudioManager");
		HSManager = GameObject.FindWithTag ("HSManager");
		storyTeller = GameObject.FindWithTag ("StoryTellerMenu");

		currentScene = 0;
		SwitchCamera (0);
	}

	public void SwitchCamera (int targetScene) {
		// Change Camera
		foreach (GameObject camera in cameras){
			if (targetScene == Array.IndexOf (cameras, camera)){
				screens[Array.IndexOf (cameras,camera)].SetActive (true);
				camera.SetActive (true);
			} else {
				screens[Array.IndexOf (cameras,camera)].SetActive (false);
				camera.SetActive (false);
			}
		}

		// Prime Animators
		if (targetScene == 1) {
			screenMenu.transform.Find("Canvas").Find("Tutorial").gameObject.SetActive(false);
			// Changing into Menu
			if (currentScene != 0 && currentScene != 2) {
				screenMenu.transform.Find ("Canvas").Find ("SkyForeground").gameObject.SetActive (false);
			} else {
				audioManager.GetComponent <AudioManager> ().ChangeMusic (1);
			}

		} else if (targetScene == 2) {
			// Changing into Game
			audioManager.GetComponent <AudioManager> ().ChangeMusic (2);
		}

		HSManager.GetComponent <HighScoreManager> ().updateScores();
		currentScene = targetScene;
	}

	public void AnimateIntroToMenu () {
		if (!playedIntroAnimation) {
			playedIntroAnimation = true;
			screenIntro.transform.Find ("Canvas").Find ("Sky").GetComponent<Animator>().SetTrigger ("StartAnimation");	
		} else if(!forcedSwitchIntro) {
			forcedSwitchIntro = true;
			nextScene = 1;
			screenIntro.transform.Find ("Canvas").Find ("Flasher").GetComponent<Animator>().SetTrigger ("Flash");
		}
	}

	public void AnimateMenuToGame () {
		nextScene = 2;
		screenMenu.transform.Find ("Canvas").Find ("Flasher").GetComponent<Animator>().SetTrigger ("Flash");
	}

	public void AnimateGameToMenu () {
		nextScene = 1;
		screenGame.transform.Find ("Canvas").Find ("Flasher").GetComponent<Animator>().SetTrigger ("Flash");
	}

	public void Flashing () {
		SwitchCamera (nextScene);
	}

	public void SeeTutorial () {
		audioManager.GetComponent<AudioManager> ().ChangeMusic (3);
		screenMenu.transform.Find("Canvas").Find("MenuList").gameObject.SetActive(false);
		screenMenu.transform.Find("Canvas").Find("Tutorial").gameObject.SetActive(true);
		storyTeller.gameObject.transform.Find("ButtonToTutorial").gameObject.SetActive(false);
	}

	public void LeaveTutorial () {
		audioManager.GetComponent<AudioManager> ().ChangeMusic (1);
		screenMenu.transform.Find("Canvas").Find("MenuList").gameObject.SetActive(true);
		screenMenu.transform.Find("Canvas").Find("Tutorial").gameObject.SetActive(false);
		storyTeller.gameObject.transform.Find("ButtonToTutorial").gameObject.SetActive(true);
	}
}
