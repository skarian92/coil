using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

// This class shows and hides doors (aka portals) when you walk into them. It listens for all OnPortalTransition events
// and manages the active portal.
public class PlacementHandler : MonoBehaviour {

	private GameObject objectToPlace;
	public GameObject overviewCanvas;
	public GameObject ObjectToPlace {
		set {
			objectToPlace = value;
		}
		get {
			return objectToPlace;
		}
	}
	public bool showGUI = true;
	public enum Mode
	{
		NotPlaced,
		Placing,
		Placed

	};
	public delegate void PlacedHandler(GameObject go);
	public event PlacedHandler Placed;

	public Mode mode = Mode.NotPlaced;
	public GameObject placementCursor;
	private const string buttonText1 = "Place";
	private const string buttonText2 = "Here";
	//	private UnityARGeneratePlane generatePlanes;
	private PlaneGenerator generatePlanes;
	private bool needsReset = false;

	private void enableGeneratePlanes(bool state) {
		if (state) {
			generatePlanes.startPlaneGeneration ();
			placementCursor.SetActive (true);
		}
		if (!state) {
			generatePlanes.stopPlaneGeneration ();
			placementCursor.SetActive (false);
		}
	}

	void Start() {
//		generatePlanes = GameObject.Find ("GeneratePlanes").GetComponent<UnityARGeneratePlane>() as UnityARGeneratePlane;
		generatePlanes = GameObject.Find ("PlaneGenerator").GetComponent<PlaneGenerator>() as PlaneGenerator;
	}
		
//	void Update(){
//		if (canvas) {
//			if (mode == Mode.Placing) {
//				canvas.SetActive (true);
//			} else {
//				canvas.SetActive (false);
//			}
//		}
//	}

	public void placeObject(GameObject go) {
		ParticleSystem particles;
		GameObject particlesGO;
		Bounds bounds;
		Renderer _renderer = go.GetComponent<Renderer> ();
		if (_renderer) {
			// it's a mesh with a renderer
			bounds = _renderer.bounds;
		} else {
			// it's a contaimner
			bounds = new Bounds (transform.position, new Vector3(0.1f, 0.1f, 0.1f)); 
			Renderer[] renderers = go.GetComponentsInChildren<Renderer> ();
			foreach (Renderer renderer in renderers)
			{
				bounds.Encapsulate (renderer.bounds);
			}	
		}
		ARHitTest.Position pos = new ARHitTest.Position();
		if (ARHitTest.HitTest (ref pos)) {		

			Debug.Log ("pos : " + pos.location.ToString());
			Debug.Log ("bounds.extent : " + bounds.ToString());
			go.transform.position = pos.location + new Vector3(0.0f, bounds.extents.y, 0.0f);
			go.transform.rotation = pos.rotation;
			go.SetActive (true);
			Debug.Log ("transform.pos : " + go.transform.position.ToString());
			if(Placed != null) {
				Placed (go);
			}
//			particlesGO = currDoor.transform.root.Find ("PlasmaExplosionEffect").gameObject;
//			particlesGO.transform.position = pos.location;
//			particles = particlesGO.GetComponent<ParticleSystem> ();
//			particles.Play ();
//			StartCoroutine(playAnimationDelayed ());
		}
	}
		

	public IEnumerator playAnimationDelayed(GameObject go) {
		Animator anim;

		yield return new WaitForSeconds(1f);
		anim = go.GetComponent<Animator> ();
		go.SetActive (true);
		anim.SetTrigger("Active");
	}

	public void ResetScene() {
		Debug.Log ("session reset");
		ARKitWorldTrackingSessionConfiguration sessionConfig = new ARKitWorldTrackingSessionConfiguration ( UnityARAlignment.UnityARAlignmentGravity, UnityARPlaneDetection.Horizontal, true, false);
		UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(sessionConfig, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
	}

	private  void handlePlaced(GameObject go) {
		Debug.Log ("placed");

		overviewCanvas.transform.position = new Vector3 (-0.7f, 0.0f, -0.2f);
		overviewCanvas.transform.position += go.transform.position;
		overviewCanvas.transform.rotation = go.transform.rotation;
		Debug.Log ("Position of equi" + go.transform.position);
		Debug.Log ("Position of panel" + overviewCanvas.transform.position);
		overviewCanvas.SetActive (true);
	}



	void OnGUI()
	{
		if (showGUI) {
			GUISkin skin = GUI.skin;
			GUIStyle style = GUI.skin.button;
			string buttonText;
			style.fontSize = 32;
			if (mode == Mode.Placing) {
				//Debug.Log("HELLOOOOOO :::::if part of gui....");
				buttonText = buttonText2;
				if (GUI.Button (new Rect (Screen.width - 350.0f, 0.0f, 150.0f, 100.0f), buttonText, style)) {
					placeObject (ObjectToPlace);
					handlePlaced (ObjectToPlace);
					mode = Mode.Placed;
					enableGeneratePlanes (false);
				}
			} else {
				//Debug.Log("HELLOOOOOO :::::else part of gui....");
				buttonText = buttonText1;
				if (GUI.Button (new Rect (Screen.width - 350.0f, 0.0f, 150.0f, 100.0f), buttonText, style)) {
					mode = Mode.Placing;
					enableGeneratePlanes (true);
				}
			}
		}
	}
}
