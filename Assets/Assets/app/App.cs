using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour {
	private Recognizer recognizer;
	private PlacementHandler placementHandler;
	public GameObject testMesh;
	public bool mock = false;

	private string recognizedId;
	private GameObject currentMesh;
	// Use this for initialization
	void Start () {
		recognizer = GetComponent<Recognizer> ();
		placementHandler = GetComponent<PlacementHandler> ();
		recognizer.TextRecognized += new Recognizer.TextRecognizedHandler(handleRecognized);
		placementHandler.Placed += new PlacementHandler.PlacedHandler(handlePlaced);
		if (mock) {
			testMesh.transform.position = new Vector3 (0.0f, 0.0f, 3.0f);
			testMesh.SetActive (true);
			handlePlaced (testMesh);
			placementHandler.ObjectToPlace = testMesh;
		}
	}

	private void handleRecognized(string id) {
		Debug.Log ("recognized: " + id);
		recognizedId = id;
		currentMesh = loadMesh (recognizedId);
		placementHandler.ObjectToPlace = currentMesh;
	}

	private  void handlePlaced(GameObject go) {
		Debug.Log ("placed");
	}

	private GameObject loadMesh(string id) {
		return testMesh;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
