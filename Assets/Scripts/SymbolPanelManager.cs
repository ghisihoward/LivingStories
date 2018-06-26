using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolPanelManager : MonoBehaviour {

	public Text symbolTextPanel, symbolTextTitle;

	// Use this for initialization
	void Awake () {
		symbolTextTitle = GameObject.FindWithTag ("SymbolTextTitle").GetComponent<Text> ();
		symbolTextPanel = GameObject.FindWithTag ("SymbolTextPanel").GetComponent<Text> ();
	}

	public void ToggleText (int symbol) {
		switch (symbol) {
		case 1:
			symbolTextTitle.text = "ARROW";
			symbolTextPanel.text = "A common tool used by Natives, the Arrow symbol idea flows from strength to protection - and even peace!";
			break;
		case 2:
			symbolTextTitle.text = "CIRCLE";
			symbolTextPanel.text = "It's round shape relates to balance so it often surrounds other messages. It brings protection and guidance.";
			break;
		case 3:
			symbolTextTitle.text = "WATER";
			symbolTextPanel.text = "The importance of water, found on rivers and oceans, relates to the symbology of life in most Native cultures.";
			break;
		case 4:
			symbolTextTitle.text = "EARTH";
			symbolTextPanel.text = "Close to the idea of motherhood, earth represents what is considered sacred in nature and in the challenges of life.";
			break;
		case 5:
			symbolTextTitle.text = "PERSON";
			symbolTextPanel.text = "A pictogram that shows mankind, it is used to tell stories about the world we live in, from beginning to end.";
			break;
		case 6:
			symbolTextTitle.text = "WOLF";
			symbolTextPanel.text = "From guidance to destruction, the identity of the wolf is rich and deeply rooted within Native American culture.";
			break;
		default:
			break;
		}
	}
}
