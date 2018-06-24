using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	private int currentScene = 0;

	private GameObject camIntro, camMenu, camGame, camOptions, camTotem;
	private GameObject[] cameras = new GameObject[5];

	public GameObject screenIntro, screenMenu, screenGame, screenOptions, screenTotem;
	private GameObject[] screens = new GameObject[5];

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
			// Changing into Menu
			if (currentScene != 0)
				screenMenu.transform.Find ("Canvas").Find ("SkyForeground").gameObject.SetActive (false);
		}

		currentScene = targetScene;
	}

	public void AnimateIntroToMenu () {
		screenIntro.transform.Find ("Canvas").Find ("Sky").GetComponent<Animator>().SetTrigger ("End");
	}
}
