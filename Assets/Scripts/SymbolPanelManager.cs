using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolPanelManager : MonoBehaviour {

	private Text symbolTextPanel, symbolTextTitle;

	// Use this for initialization
	void Start () {
		symbolTextTitle = GameObject.FindWithTag ("SymbolTextTitle").GetComponent<Text> ();
		symbolTextPanel = GameObject.FindWithTag ("SymbolTextPanel").GetComponent<Text> ();
	}

	public void ToggleText (int symbol) {
		switch (symbol) {
		case 1:
			symbolTextTitle.text = "ARROW";
			symbolTextPanel.text = "A powerful tool for the Natives, the symbology of the Arrow flows from strength to protection - and even peace.";
			break;
		case 2:
			symbolTextTitle.text = "CIRCLE";
			symbolTextPanel.text = "Symbolic representation of balance, the circle is used to surround messages, signifying protection and guidance.";
			break;
		case 3:
			symbolTextTitle.text = "WATER";
			symbolTextPanel.text = "Usually representing long bodies of water, such as a river, it's symbology is rich, mostly regarding life.";
			break;
		case 4:
			symbolTextTitle.text = "MOTHER EARTH";
			symbolTextPanel.text = "A sacred symbology, it identifies that which is sacred in nature and illustrates the challenges of life.";
			break;
		case 5:
			symbolTextTitle.text = "PERSON";
			symbolTextPanel.text = "The symbol of mankind, used to tell stories about the inhabitants of the world, from beginning to end.";
			break;
		case 6:
			symbolTextTitle.text = "WOLF";
			symbolTextPanel.text = "From guidance to destruction, the symbology of the wolf is deeply varied and rooted within Native American culture.";
			break;
		default:
			break;
		}
	}
}
