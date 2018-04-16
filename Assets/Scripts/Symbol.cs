using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{

	public string className;
	public Sprite image;

	private GameObject gm, symbolManager;
	private Settings gameSettings;

	// Use this for initialization
	void Start()
	{
		gm = GameObject.Find("GameManager");
		symbolManager = GameObject.Find("SymbolManager");
		gameSettings = gm.GetComponent<Settings>();
	}

	void Update()
	{
		transform.Translate(Vector2.down * Time.deltaTime * gameSettings.gameSpeed);

		Vector3 screenPoint = Camera.main.WorldToViewportPoint(this.transform.position);
		bool onScreen = (
		                    screenPoint.z > 0 &&
		                    screenPoint.x > 0 && screenPoint.x < 1 &&
		                    screenPoint.y > -0.1 && screenPoint.y < 1
		                );

		if (!onScreen) {
			gm.BroadcastMessage("LostSymbol");
			symbolManager.GetComponent<SymbolManager>().lostSymbol(this.gameObject);
		}
	}

	void OnBecameInvisible()
	{
	}

	public void SelfDestruct()
	{
		Destroy(this.gameObject);
	}
}
