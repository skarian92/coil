using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class PlaneGenerator : MonoBehaviour
{
	public GameObject planePrefab;
	private UnityARAnchorManager unityARAnchorManager;

	// Use this for initialization
	public void startPlaneGeneration () {
		unityARAnchorManager = new UnityARAnchorManager();
		UnityARUtility.InitializePlanePrefab (planePrefab);
	}

	public void stopPlaneGeneration()
	{
		unityARAnchorManager.Destroy ();
	}

}


