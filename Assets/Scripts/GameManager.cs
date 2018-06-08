using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	private int symbolToTest = 0, gamePoints = 0, lives = 0, combo;
	private float timer = 0, totalTime;
	private GameObject gm, textLives, textPoints, menu;
	private Settings gameSettings;
	private SymbolManager symbolManager;

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

	void Start ()
	{
		gm = GameObject.Find ("GameManager");
		textLives = GameObject.Find ("TextLives");
		textPoints = GameObject.Find ("TextPoints");
		menu = GameObject.Find ("Menu");
		gameSettings = gm.GetComponent<Settings> ();
		symbolManager = GameObject.Find ("SymbolManager").GetComponent<SymbolManager> ();
	}

	void Update ()
	{
		if (lives < 0) {
			gameState = GameState.Paused;
			gamePoints = 0;
			timer = 0;
			totalTime = 0;
			menu.SetActive (true);
			symbolManager.reset ();
		}

		if (gameState == GameState.Running) {
			timer += Time.deltaTime;
			totalTime += Time.deltaTime;
			textLives.GetComponent<Text> ().text = "Lives: " + lives;
			textPoints.GetComponent<Text> ().text = gamePoints.ToString ("0") + " Points";
			symbolManager.doUpdate ();	

			if (timer >= gameSettings.spawnInterval / gameSettings.currentGameDif) {
				timer = 0;
				switch (currentRun) {
				case TypeOfRun.Test:
					symbolManager.addToSequence (symbolToTest);
					break;
				case TypeOfRun.Normal:
					symbolManager.addRandomToSequence ();
					break;
				default:
					break;
				}
			}
		}
	}

	public void CorrectSymbol ()
	{
		gamePoints += 1 + combo;
		combo += 1;
		gameSettings.currentGameDif *= gameSettings.difFluctuation;
	}

	public void WrongSymbol ()
	{
		combo = 0;
	}

	public void LostSymbol ()
	{
		symbolManager.reset ();
		lives -= 1;
	}

	public void SetGameMode (int option)
	{
		menu.SetActive (false);
		gameState = GameState.Running;
		symbolToTest = option;
		lives = gameSettings.maxLives;
		gameSettings.currentGameDif = 1;
		combo = 0;

		if (option == 3) {
			currentRun = TypeOfRun.Normal;
		} else {
			currentRun = TypeOfRun.Test;
		}
	}
}
