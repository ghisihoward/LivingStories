using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	private float timer = 0, totalTime;
	private GameObject gm, textTime, textPoints, menu;
	private Settings gameSettings;
	private SymbolManager symbolManager;
	private int symbolToTest = 0, gamePoints = 0;

	private enum GameState
	{
		Paused,
		Running,
	}

	private enum TypeOfRun
	{
		Test,
		Normal
	}

	private GameState gameState = GameState.Paused;
	private TypeOfRun currentRun = TypeOfRun.Normal;

	// Use this for initialization
	void Start()
	{
		gm = GameObject.Find("GameManager");
		textTime = GameObject.Find("TextTime");
		textPoints = GameObject.Find("TextPoints");
		menu = GameObject.Find("Menu");
		gameSettings = gm.GetComponent<Settings>();
		symbolManager = GameObject.Find("SymbolManager").GetComponent<SymbolManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (gamePoints < 0) {
			gameState = GameState.Paused;
			gamePoints = 0;
			timer = 0;
			totalTime = 0;
			menu.SetActive(true);
			symbolManager.reset();
		}

		switch (gameState) {
			case GameState.Paused:
				break;

			case GameState.Running:

				timer += Time.deltaTime;
				totalTime += Time.deltaTime;
				textTime.GetComponent<Text>().text = "Time: " + totalTime.ToString("#0.##");
				textPoints.GetComponent<Text>().text = gamePoints.ToString("0") + " Points";
				symbolManager.doUpdate();	

				if (timer >= gameSettings.spawnInterval) {
					timer = 0;
					switch (currentRun) {
						case TypeOfRun.Test:
							symbolManager.addToSequence(symbolToTest);
							break;
						case TypeOfRun.Normal:
							symbolManager.addRandomToSequence();
							break;
						default:
							break;
					}
				}

				break;

			default:
				break;
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

	public void SetGameMode(int option)
	{
		switch (option) {
			case 0:
				menu.SetActive(false);
				symbolToTest = 0;
				currentRun = TypeOfRun.Test;
				gameState = GameState.Running;
				break;
			case 1:
				menu.SetActive(false);
				symbolToTest = 1;
				currentRun = TypeOfRun.Test;
				gameState = GameState.Running;
				break;
			case 2:
				menu.SetActive(false);
				symbolToTest = 2;
				currentRun = TypeOfRun.Test;
				gameState = GameState.Running;
				break;
			case 3:
				menu.SetActive(false);
				currentRun = TypeOfRun.Normal;
				gameState = GameState.Running;
				break;
			default:
				break;
		}
	}
}
