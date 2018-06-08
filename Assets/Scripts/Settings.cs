using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	public bool log = false;

	public int maxLives;
	public float gameSpeed = 0.8f;
	public float spawnInterval = 3;
	public float currentGameDif = 1;
	public GameObject[] possibleSymbols;
}
