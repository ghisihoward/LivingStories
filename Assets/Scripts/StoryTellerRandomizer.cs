using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTellerRandomizer : MonoBehaviour {

	private int stseed, numberofbraids, hairchoice;
	public Sprite braid;
	public Sprite[] possiblehairs;

	// Use this for initialization
	void Awake () {
		stseed = PlayerPrefs.GetInt ("STSeed", -1);

		if (stseed == -1) {
			stseed = Random.Range (0, int.MaxValue/2);
			PlayerPrefs.SetInt("STSeed", stseed);
		}
		
		Random.InitState (stseed);
		numberofbraids = Random.Range (0, 4);
		hairchoice = Random.Range (0, 4);

		switch (numberofbraids) {
		case 0:
			this.transform.Find("Head").Find("Braids").Find("LeftBraid").gameObject.SetActive(false);
			this.transform.Find("Head").Find("Braids").Find("RightBraid").gameObject.SetActive(false);
			break;
		case 1:
			this.transform.Find("Head").Find("Braids").Find("LeftBraid").gameObject.SetActive(false);
			break;
		case 2:
			this.transform.Find("Head").Find("Braids").Find("RightBraid").gameObject.SetActive(false);
			break;
		case 3:
			break;
		default:
			break;
		}

		if (hairchoice != 3)
			this.transform.Find("Head").Find ("Hair"). GetComponent<Image> ().sprite = possiblehairs[hairchoice];

		new Random ();
	}
}
