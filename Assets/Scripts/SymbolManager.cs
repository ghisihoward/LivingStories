using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SymbolManager : MonoBehaviour
{
	
	private GameObject gm, textSymbol;
	private Settings gameSettings;
	private List<GameObject> symbols = null;

	// Use this for initialization
	void Start ()
	{
		gm = GameObject.Find ("GameManager");
		textSymbol = GameObject.Find ("TextSymbol");
		gameSettings = gm.GetComponent<Settings> ();
		symbols = new List<GameObject> ();
	}

	public void doUpdate ()
	{
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

	public void reset ()
	{
		foreach (GameObject symbol in symbols) {
			symbol.GetComponent<Symbol> ().SelfDestruct ();
		}
		symbols.Clear ();
	}

	public void addToSequence (GameObject symbol)
	{
		GameObject symbolObject = Instantiate (symbol, this.transform);
		Vector3 newPos = new Vector3 (
			                 Random.Range (-2, 2f), 
			                 symbolObject.transform.position.y, 
			                 symbolObject.transform.position.z); 
		symbolObject.transform.position = newPos;
		symbols.Add (symbolObject);
	}

	public void addToSequence (int arrayIndex)
	{
		this.addToSequence (gameSettings.possibleSymbols [arrayIndex]);
	}

	public void addRandomToSequence ()
	{
		this.addToSequence (
			gameSettings.possibleSymbols [Random.Range (0, gameSettings.possibleSymbols.Length)]
		);
	}

	private void removeFromSequence (GameObject symbol)
	{
		symbol.GetComponent<Symbol> ().setToDestroy (true);
	}

	public void lostSymbol (GameObject symbol)
	{
		removeFromSequence (symbol);
	}

	// Checks if it was correctly swiped
	void gestureDone (string gestureTried)
	{
		if (textSymbol.activeInHierarchy)
			textSymbol.GetComponent<Text> ().text = gestureTried;

		if (gm.GetComponent<Settings> ().log) {
			Debug.Log ("Gestured Tried: " + gestureTried);
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
		gm.BroadcastMessage ("WrongSymbol");
	}
}
