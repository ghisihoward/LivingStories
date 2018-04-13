using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour {

	public string className;
	public Sprite image;

	private GameObject gm;
	private Settings gameSettings;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager");
		gameSettings = gm.GetComponent<Settings> ();
	}

	void Update () {
		transform.Translate (Vector2.down * Time.deltaTime * gameSettings.gameSpeed);
	}

	void OnBecameInvisible () {
		gm.BroadcastMessage ("LostSymbol");
		Destroy(this.gameObject);
	}

	public void SelfDestruct () {
		Destroy(this.gameObject);
	}
}
