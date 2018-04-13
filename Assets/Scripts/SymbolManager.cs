using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SymbolManager : MonoBehaviour {

	private GameObject gm;
	private ArrayList symbols = null;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager");
		symbols = new ArrayList ();
	}

	public void addToSequence (GameObject symbol) {
		symbols.Add (symbol);
	}

	// Checks if it was correctly swiped
	void gestureDone (string gestureTried) {
		if (gm.GetComponent<Settings> ().log) {
			Debug.Log ("Gestured Tried: " + gestureTried);
			Debug.Log ("I have: ");
			foreach (GameObject symbol in symbols) {
				Debug.Log (symbol.GetComponent<Symbol> ().className);
			}
		}

		foreach (GameObject symbol in symbols) {
			if (symbol.GetComponent<Symbol> ().className.Equals (gestureTried)) {
				symbols.Remove (symbol);
				symbol.GetComponent<SpriteRenderer> ().color = gm.GetComponent<Settings> ().fadedSymbolColor;
				gm.BroadcastMessage ("correctSymbol");
			}
		}
		gm.BroadcastMessage ("wrongSymbol");
	}
}
