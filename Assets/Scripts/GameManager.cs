using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public bool playedDaily;
	private bool powerUpActive = false;
	private int symbolToTest = 0, gamePoints = 0, lives = 0, combo;
	private float timer = 0, timerForPowerUp = 0, timerPowerActive = 0, totalTime;
	private GameObject gm, textPoints, cameraManager, gameOverPanel;
	private Settings gameSettings;
	private SymbolManager symbolManager;
	private SFXManager sfxmg, wfsfxmg;

	private enum GameState { Paused, Running }
	private enum TypeOfRun { Test, Normal, Daily }
	private GameState gameState = GameState.Paused;
	private TypeOfRun currentRun = TypeOfRun.Normal;

	void Awake () {
		gm = GameObject.Find ("GameManager");
		textPoints = GameObject.Find ("TextPoints");
		cameraManager = GameObject.FindWithTag ("CameraManager");
		gameSettings = gm.GetComponent<Settings> ();
		symbolManager = GameObject.Find ("SymbolManager").GetComponent<SymbolManager> ();
		sfxmg = GameObject.FindWithTag ("SFXManager").GetComponent<SFXManager> ();
		wfsfxmg = GameObject.FindWithTag ("WolfSFXManager").GetComponent<SFXManager> ();
		gameOverPanel = GameObject.Find ("GameOverPanel");
	}

	void Update () {
		if (lives < 0 && gameState == GameState.Running) {
			gameState = GameState.Paused;

			switch (currentRun){
			case TypeOfRun.Normal:
				if (int.Parse(PlayerPrefs.GetString("PlayRecord", "0")) < gamePoints)
					PlayerPrefs.SetString("PlayRecord", gamePoints.ToString());
				break;

			case TypeOfRun.Daily:
				PlayerPrefs.SetString("DailyRecord", gamePoints.ToString());
				break;
			}

			this.transform.parent.Find("Lightning").GetComponent<Animator> ().SetTrigger("Light");
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
				case TypeOfRun.Daily:
					symbolManager.addDailyToSequence ();
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

		sfxmg.GetComponent <SFXManager> ().playCorrectSFX();
	}

	public void WrongSymbol () {
		combo = 0;
	}

	public void LostSymbol () {
		gameSettings.currentGameDif = 1;
		lives -= 1;
	}

	public void SetGameMode (int option) {
		symbolToTest = option;
		lives = gameSettings.maxLives;
		gameSettings.currentGameDif = 1;
		gameSettings.currentTimeMod = 1;
		combo = 0;
		gamePoints = 0;
		timer = gameSettings.spawnInterval;
		timerForPowerUp = 0;
		totalTime = 0;
		gameOverPanel.SetActive (false);

		if (option == 1) {
			gameState = GameState.Running;
			currentRun = TypeOfRun.Normal;
			symbolManager.setNormal ();
			cameraManager.GetComponent<CameraManager> ().AnimateMenuToGame();
		} else if (option == 2) {
			if (!playedDaily){
				string today = System.DateTime.Now.ToString("yyyyMMdd");

				gameState = GameState.Running;
				currentRun = TypeOfRun.Daily;
				PlayerPrefs.SetString("DailyRecord", "0");
				PlayerPrefs.SetString("PlayedDaily", today);
				symbolManager.setDaily (int.Parse (today));

				cameraManager.GetComponent<CameraManager> ().AnimateMenuToGame();	
			}
		}
	}

	public void SetPowerUpWolf () {
		wfsfxmg.GetComponent <SFXManager> ().playWolfSFX();
		gameSettings.currentTimeMod = 0.5f;
		powerUpActive = true;
	}

	public void ResetGame () {
		symbolManager.reset ();
		gameOverPanel.SetActive (true);
		gameOverPanel.transform.Find("Text").GetComponent<Text> ().text = "SCORE : " + gamePoints;
	}
}