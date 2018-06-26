using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SymbolManager : MonoBehaviour {
	private bool wolfPowerUp = false;
	private int dailySeed;
	private Random dailyRNG;
	private GameObject gm;
	private Settings gameSettings;
	private List<GameObject> symbols = null;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager");
		gameSettings = gm.GetComponent<Settings> ();
		symbols = new List<GameObject> ();
	}

	public void doUpdate () {
		for (int i = symbols.Count - 1; i >= 0; i--) {
			if (symbols [i].GetComponent<Symbol> ().getToDestroy ()) { 
				GameObject remnantSymbol = symbols [i];
				symbols.Remove (remnantSymbol);
				remnantSymbol.GetComponent<Symbol> ().SelfDestruct ();
			} else {
				symbols [i].GetComponent<Symbol> ().doUpdate ();
			}
		}
	}

	public void reset () {
		foreach (GameObject symbol in symbols) {
			symbol.GetComponent<Symbol> ().SelfDestruct ();
		}
		symbols.Clear ();
	}

	public void addToSequence (GameObject symbol) {
		GameObject symbolObject = Instantiate (symbol, this.transform);
		Vector3 newPos = new Vector3 (
			                 Random.Range (-2, 2f), 
			                 symbolObject.transform.position.y, 
			                 symbolObject.transform.position.z); 
		symbolObject.transform.position = newPos;
		symbols.Add (symbolObject);
	}

	public void addToSequence (int arrayIndex) {
		this.addToSequence (gameSettings.easySymbols [arrayIndex]);
	}

	public void addRandomToSequence () {
		if (gameSettings.currentGameDif > gameSettings.ladderGameDif && Random.Range (0f, 1f) > 0.5f) {
			this.addToSequence (
				gameSettings.hardSymbols [Random.Range (0, gameSettings.hardSymbols.Length)]
			);
		} else {
			this.addToSequence (
				gameSettings.easySymbols [Random.Range (0, gameSettings.easySymbols.Length)]
			);
		}
	}

	public void addDailyToSequence() {
		if (gameSettings.currentGameDif > gameSettings.ladderGameDif && Random.Range (0f, 1f) > 0.5f) {
			this.addToSequence (
				gameSettings.hardSymbols [Random.Range (0, gameSettings.hardSymbols.Length)]
			);
		} else {
			this.addToSequence (
				gameSettings.easySymbols [Random.Range (0, gameSettings.easySymbols.Length)]
			);
		}
	}

	private void removeFromSequence (GameObject symbol) {
		Instantiate(gameSettings.particleSymbol, symbol.transform.position, Quaternion.identity);
		Instantiate(gameSettings.particleSymbolCore, symbol.transform.position, Quaternion.identity);
		symbol.GetComponent<Symbol> ().setToDestroy (true);
	}

	public void lostSymbol (GameObject symbol) {
		removeFromSequence (symbol);
	}

	public void setPowerUp (){
		wolfPowerUp = true;
	}

	public void setNormal () {
		new Random();
	}

	public void setDaily (int seed){
		dailySeed = seed;
		Random.InitState(dailySeed);
	}

	// Checks if it was correctly swiped
	void gestureDone (string gestureTried) {
		if (gm.GetComponent<Settings> ().log) {
			Debug .Log ("Gestured Tried: " + gestureTried);
			Debug.Log ("I had: ");
			foreach (GameObject symbol in symbols) {
				Debug.Log (symbol.GetComponent<Symbol> ().className);
			}
		}
			
		foreach (GameObject symbol in symbols) {
			if (symbol.GetComponent<Symbol> ().className.Equals (gestureTried)) {
				if (gm.GetComponent<Settings> ().log) {
					Debug.Log ("Removed: " + gestureTried);
				}
				this.removeFromSequence (symbol);
				gm.BroadcastMessage ("CorrectSymbol");
				return;
			}
		}

		if (wolfPowerUp && gestureTried == "Wolf") {
			transform.parent.Find("WolfPower").gameObject.SetActive(false);
			transform.parent.GetComponent<GameManager> ().SetPowerUpWolf();
		}

		gm.BroadcastMessage ("WrongSymbol");
	}
}
