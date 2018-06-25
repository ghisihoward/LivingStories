using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private bool powerUpActive = false;
	private int symbolToTest = 0, gamePoints = 0, lives = 0, combo;
	private float timer = 0, timerForPowerUp = 0, timerPowerActive = 0, totalTime;
	private GameObject gm, textPoints, cameraManager;
	private Settings gameSettings;
	private SymbolManager symbolManager;

	private enum GameState { Paused, Running }
	private enum TypeOfRun { Test, Normal }
	private GameState gameState = GameState.Paused;
	private TypeOfRun currentRun = TypeOfRun.Normal;

	void Start () {
		gm = GameObject.Find ("GameManager");
		textPoints = GameObject.Find ("TextPoints");
		cameraManager = GameObject.FindWithTag ("CameraManager");
		gameSettings = gm.GetComponent<Settings> ();
		symbolManager = GameObject.Find ("SymbolManager").GetComponent<SymbolManager> ();
	}

	void Update () {
		if (lives < 0) {
			gameState = GameState.Paused;
			cameraManager.GetComponent<CameraManager> ().SwitchCamera(1);
			symbolManager.reset ();
		}

		if (gameState == GameState.Running) {
			timer += Time.deltaTime * gameSettings.currentTimeMod;
			totalTime += Time.deltaTime * gameSettings.currentTimeMod;
			textPoints.GetComponent<Text> ().text = gamePoints.ToString ("0");
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

			if (timerForPowerUp >= gameSettings.powerUpInterval) {
				transform.Find ("WolfPower").gameObject.SetActive (true);
				timerForPowerUp = 0;
				gameSettings.powerUpInterval += gameSettings.powerUpIntervalFluctuation;
				symbolManager.setPowerUp ();
			}

			if (powerUpActive) {
				timerPowerActive += Time.deltaTime * gameSettings.currentTimeMod;
				if (timerPowerActive >= gameSettings.powerUpDuration) {
					gameSettings.currentTimeMod = 1f;
					timerPowerActive = 0;
					powerUpActive = false;
				}
			} else {
				timerForPowerUp += Time.deltaTime * gameSettings.currentTimeMod;
			}
		}
	}

	public void CorrectSymbol () {
		gamePoints += 1 + combo;
		combo += 1;

		if (gameSettings.currentGameDif <= gameSettings.maxGameDif) {
			gameSettings.currentGameDif += gameSettings.difFluctuation;
		} else {
			gameSettings.currentGameDif += (gameSettings.difFluctuation / 10);
		}
	}

	public void WrongSymbol () {
		combo = 0;
	}

	public void LostSymbol () {
		symbolManager.reset ();
		gameSettings.currentGameDif = 1;
		lives -= 1;
	}

	public void SetGameMode (int option) {
		gameState = GameState.Running;
		symbolToTest = option;
		lives = gameSettings.maxLives;
		gameSettings.currentGameDif = 1;
		combo = 0;
		gamePoints = 0;
		timer = gameSettings.spawnInterval;
		timerForPowerUp = 0;
		totalTime = 0;

		if (option == 3) {
			currentRun = TypeOfRun.Normal;
		} else {
			currentRun = TypeOfRun.Test;
		}
	}

	public void SetPowerUpWolf () {
		gameSettings.currentTimeMod = 0.5f;
		powerUpActive = true;
	}
}