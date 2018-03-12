using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIElements : MonoBehaviour {

	private PlacementHandler placementHandler;
	public GameObject panel_1;
	public GameObject panel_2;
	public GameObject panel_3;
	public GameObject panel_1_component;
	public GameObject panel_2_component;
	public GameObject panel_3_component;
	public GameObject panel_problem;
	public GameObject button_components;
	public GameObject button_instructions;
	public GameObject instructionCanvas;
	public GameObject button_explode;
	public GameObject button_assemble;
	public GameObject button_order;
	public GameObject button_overview;
	public GameObject overview_canvas;
	public GameObject collection_canvas;
	Animation anim {
		get {
			return placementHandler.ObjectToPlace.GetComponent<Animation> ();
		}
	}
//	private GameObject mainObject;
	static private ComponentHighlighting highLighting;

	void Start () {
		placementHandler = GetComponent<PlacementHandler> ();
		// @Sanket: this will not work if we do asset identification via service call
//		mainObject = placementHandler.ObjectToPlace;
//		anim = mainObject.GetComponent<Animation>();
	}

	public void order(){
		instructionCanvas.SetActive (false);
		button_order.GetComponent<Button>().interactable = false;
		button_components.GetComponent<Button>().interactable = true;
	}

	public void explode(){
		Debug.Log ("ObjectToPlacepos : " + placementHandler.ObjectToPlace.transform.position);
		Debug.Log ("MainObjectpos : " + placementHandler.ObjectToPlace.transform.position);
		anim.Play ("explode");
		button_explode.SetActive (false);
		button_assemble.SetActive (true);
	}

	public void assemble(){
		
		anim.Play ("assemble");
		button_assemble.SetActive (false);
		button_explode.SetActive (true);
//		button_order.SetActive (true);
	}

	public void hideInstructions() {
		button_instructions.SetActive (false);
		instructionCanvas.SetActive(false);
	}

	public void hideProblems() {
		panel_problem.SetActive(false);
		//		if(highLighting != null)
		try {
			highLighting.restoreStatus ();
		}
		catch(Exception e) {
			;
		}
	}

	public void hideComponents() {
		panel_1_component.SetActive(false);
		panel_2_component.SetActive(false);
		panel_3_component.SetActive(false);
	}

	public void showInstructions(){
		panel_1.SetActive (false);
		panel_problem.SetActive (false);

		positionObject (instructionCanvas, new Vector3 (-0.7f, 0.0f, -0.2f));
		button_instructions.SetActive (false);
	}

	public void showProblems(){
		Debug.Log ("Showing Problems");
		panel_1_component.SetActive (false);
		panel_2_component.SetActive (false);
		panel_3_component.SetActive (false);

		highLighting = new ComponentHighlighting ();
		highLighting.setColorOfComponentObjects ("Component1", ComponentHighlighting.Status.red);

		positionObject (panel_problem, new Vector3 (-0.52f,0,-0.3f));

		button_components.GetComponent<Button>().interactable = true;
		button_instructions.SetActive (true);

		//panel_1.SetActive (false);
		panel_2.SetActive (false);
		panel_3.SetActive (false);
	}


	public void showComponents(GameObject overviewCanvas){
		Debug.Log ("ObjectToPlacepos : " + placementHandler.ObjectToPlace.transform.position);
		Debug.Log ("MainObjectpos : " + placementHandler.ObjectToPlace.transform.position);
		hideInstructions ();
		hideProblems ();
		overviewCanvas.SetActive (false);
		positionObject (panel_1, new Vector3 (0.12f,0,-0.1f));
		positionObject (panel_2, new Vector3 (0.21f,-0.1f,-0.32f));
		positionObject (panel_3, new Vector3 (0.12f,-0.27f,-0.37f));
		positionObject (panel_1_component, new Vector3 (-0.52f,0,-0.3f));
		positionObject (panel_2_component, new Vector3 (-0.52f,-0.1f,-0.3f));
		positionObject (panel_3_component, new Vector3 (-0.52f,-0.2f,-0.3f));
		positionObject (collection_canvas, new Vector3 (1.52f,-0.2f,-0.3f));
	}

	public void showOverview(GameObject overviewCanvas){
		hideInstructions ();
		hideProblems ();
		hideComponents ();
		overview_canvas.SetActive (true);
	}

	private void positionObject(GameObject obj,Vector3 v){
		Debug.Log ("main object" + placementHandler.ObjectToPlace.transform.position);
		obj.transform.position = v;
		obj.transform.position += placementHandler.ObjectToPlace.transform.position;
		obj.transform.rotation = placementHandler.ObjectToPlace.transform.rotation;
		Debug.Log ("secondary object" + obj.transform.position);
		obj.SetActive (true);
	}
}
