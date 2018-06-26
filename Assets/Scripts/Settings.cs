using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
	public bool log = false;

	public int maxLives;
	public float gameSpeed = 0.8f;
	public float spawnInterval = 3;
	public float currentGameDif = 1;
	public float currentTimeMod = 1;
	public float maxGameDif = 2;
	public float ladderGameDif = 1.5f;
	public float difFluctuation = 1.10f;
	public float powerUpInterval = 15f;
	public float powerUpIntervalFluctuation = 10f;
	public float powerUpDuration = 5f;
	public GameObject particleSymbol;
	public GameObject particleSymbolCore;
	public GameObject[] easySymbols;
	public GameObject[] hardSymbols;
	public GameObject[] powerUpSymbols;
}
