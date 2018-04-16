using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	private float timer = 0, totalTime;
	private GameObject gm, textTime, textPoints;
	private Settings gameSettings;
	private SymbolManager symbolManager;
	private int symbolToTest = 0, gamePoints = 0;

	private enum TypeOfRun
	{
		Test,
		Normal}
	;

	private TypeOfRun currentRun = TypeOfRun.Normal;

	// Use this for initialization
	void Start()
	{
		gm = GameObject.Find("GameManager");
		textTime = GameObject.Find("TextTime");
		textPoints = GameObject.Find("TextPoints");
		gameSettings = gm.GetComponent<Settings>();
		symbolManager = GameObject.Find("SymbolManager").GetComponent<SymbolManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;
		totalTime += Time.deltaTime;
		textTime.GetComponent<Text>().text = "Time: " + totalTime.ToString("#0.##");
		textPoints.GetComponent<Text>().text = gamePoints.ToString("0") + " Points";

		if (timer >= gameSettings.spawnInterval) {
			timer = 0;
			switch (currentRun) {
				case TypeOfRun.Test:
					symbolManager.addToSequence(symbolToTest);
					Debug.Log("Doing Test");
					break;
				case TypeOfRun.Normal:
					symbolManager.addRandomToSequence();
					Debug.Log("Doing Normal");
					break;
				default:
					break;
			}
		}
	}

	public void CorrectSymbol()
	{
		gamePoints += 1;
	}

	public void WrongSymbol()
	{
	}

	public void LostSymbol()
	{
		gamePoints -= 1;
	}
}
