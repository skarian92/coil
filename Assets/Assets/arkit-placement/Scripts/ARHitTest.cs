using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ARHitTest : MonoBehaviour {
	public class Position {
		public Vector3 location;
		public Quaternion rotation;
	};

	private static Vector3 debugPos;

	/// <summary>
	/// Function that is called on 
	/// NOTE: HIT TESTS DON'T WORK IN ARKIT REMOTE
	/// </summary>
	public static bool HitTest(ref Position position) {
		ARPoint point = new ARPoint { 
			x = 0.5f, //do a hit test at the center of the screen
			y = 0.5f
		};

		// prioritize result types
		ARHitTestResultType[] resultTypes = {
			ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, // if you want to use bounded planes
//			ARHitTestResultType.ARHitTestResultTypeExistingPlane, // if you want to use infinite planes 
//			ARHitTestResultType.ARHitTestResultTypeFeaturePoint // if you want to hit test on feature points
		}; 
		position.location = Vector3.zero;
		position.rotation = Quaternion.identity;
		foreach (ARHitTestResultType resultType in resultTypes) {
			if (HitTestWithResultType (point, resultType, ref position)) {
				return true;
			}
		}
		return false;
	}

	static bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes, ref Position position) {
		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
		if (hitResults.Count > 0) {
			ARHitTestResult hitResult = hitResults[0];
			//TODO: get the position and rotations to spawn the hat
			Vector3 pos = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
			Quaternion rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
			position.location = pos;
			position.rotation = rotation;
			Debug.Log ("found plane in hit test");
			debugPos = pos;
			//				spawnedObjects.Add( Instantiate (hitPrefab, pos, rotation) ); // in order to use for shuffling
			return true;
//			foreach (var hitResult in hitResults) {
//				//TODO: get the position and rotations to spawn the hat
//				Vector3 pos = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
//				Quaternion rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
//				position.location = pos;
//				position.rotation = rotation;
//				Debug.Log ("found plane in hit test");
////				spawnedObjects.Add( Instantiate (hitPrefab, pos, rotation) ); // in order to use for shuffling
//				return true;
//			}
		}
		return false;
	}
//	void OnGUI()
//	{
//		if(debugPos != null)
//			GUI.Box (new Rect (100, 100 + 100, 800, 60), string.Format ("Center: x:{0}, y:{1}, z:{2}", debugPos.x, debugPos.y, debugPos.z));
//
//	}
}
