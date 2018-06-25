using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

	private string playRecord, dailyRecord;
	private Text playRecordField, dailyRecordField;
	private GameManager GM;

	// Use this for initialization
	void Start () {
		GM = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		playRecordField = GameObject.FindWithTag ("PlayRecord").GetComponent<Text> ();
		dailyRecordField = GameObject.FindWithTag ("DailyRecord").GetComponent<Text> ();

		this.updateScores ();
	}

	public void updateScores () {
		if (PlayerPrefs.GetString("PlayedDaily", "0") != System.DateTime.Now.ToString("yyyyMMdd")) {
			GM.playedDaily = false;
			PlayerPrefs.SetString ("DailyRecord", "-");
		} else {
			GM.playedDaily = true;
		}

		playRecord = PlayerPrefs.GetString ("PlayRecord", "0");
		dailyRecord = PlayerPrefs.GetString ("DailyRecord", "-");

		playRecordField.text = playRecord;
		dailyRecordField.text = dailyRecord;
	}
}
