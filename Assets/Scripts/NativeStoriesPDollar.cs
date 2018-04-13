using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class NativeStoriesPDollar : MonoBehaviour {

	public Transform gestureOnScreenPrefab;

	private int strokeId = -1;
	private int vertexCount = 0;

	private GameObject gm;

	private List<Gesture> trainingSet = new List<Gesture> ();
	private List<Point> points = new List<Point> ();
	private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer> ();
	private LineRenderer currentGestureLineRenderer;

	private Vector3 virtualKeyPosition = Vector2.zero;
	private Rect drawArea;
	private RuntimePlatform platform;

	private bool recognized;

	void Start () {
		gm = GameObject.Find ("GameManager");
		platform = Application.platform;

		//Load pre-made gestures
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset> ("GestureSet/NativeStories");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add (GestureIO.ReadGestureFromXML (gestureXml.text));

		//Load user custom gestures
		string[] filePaths = Directory.GetFiles (Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths)
			trainingSet.Add (GestureIO.ReadGestureFromFile (filePath));
	}

	void Update () {
		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0) {
				virtualKeyPosition = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y);
			}
		} else {
			if (Input.GetMouseButton (0)) {
				virtualKeyPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			if (recognized) {
				recognized = false;
				strokeId = -1;
				points.Clear ();
				foreach (LineRenderer lineRenderer in gestureLinesRenderer) {
					//lineRenderer.positionCount = 0;
					lineRenderer.SetVertexCount (0);
					Destroy (lineRenderer.gameObject);
				}
				gestureLinesRenderer.Clear ();
			}

			++strokeId;
			Transform tmpGesture = Instantiate (gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
			currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer> ();
				
			gestureLinesRenderer.Add (currentGestureLineRenderer);
			vertexCount = 0;
		}
			
		if (Input.GetMouseButton (0)) {
			points.Add (new Point (virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

			//currentGestureLineRenderer.positionCount = ++vertexCount;
			currentGestureLineRenderer.SetVertexCount (++vertexCount);
			currentGestureLineRenderer.SetPosition (vertexCount - 1, Camera.main.ScreenToWorldPoint (new Vector3 (virtualKeyPosition.x, virtualKeyPosition.y, 10)));
		}

		if (Input.GetMouseButtonUp (0)) {
			recognized = true;

			Gesture candidate = new Gesture (points.ToArray ());
			try {
				Result gestureResult = PointCloudRecognizer.Classify (candidate, trainingSet.ToArray ());
				gm.BroadcastMessage ("gestureDone", gestureResult.GestureClass);
			} catch (IndexOutOfRangeException) {
				Debug.Log ("No result found.");
			}
		}
	}
}
