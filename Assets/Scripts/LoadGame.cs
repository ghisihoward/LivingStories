using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (loadGameScene());
	}

	IEnumerator loadGameScene () {
		AsyncOperation async = SceneManager.LoadSceneAsync("Main");

		while (!async.isDone) {
			yield return null;
		}
	}
}
