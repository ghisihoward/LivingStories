using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private float timer = 0;
	private GameObject gm;
	private Settings gameSettings;
	private SymbolManager symbolManager;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager");
		gameSettings = gm.GetComponent<Settings>();
		symbolManager = GameObject.Find ("SymbolManager").GetComponent<SymbolManager>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= gameSettings.spawnInterval) {
			timer = 0;
			symbolManager.addRandomToSequence ();
			// spawn symbol;
			// move symbol;
		}
	}

	void CorrectSymbol () {
	}

	void WrongSymbol () {
	}

	void LostSymbol () {
	}
}
