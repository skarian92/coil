using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentHighlighting : MonoBehaviour {
	public GameObject AssetMesh;
	public enum Status { green, yellow, red };
	public const string defaultKPI = "default";
	public string componentKPI = defaultKPI;
	private GameObject[] components;
	private List<objectStatus> originalStatus;
	private GameObject[] componentObjects;
	private System.Random rand;

	private class objectStatus {
		public objectStatus(GameObject obj, Color col) {
			this.obj = obj;
			this.color = col;
		}
		public GameObject obj;
		public Color color;
	}
		

	private GameObject[] getObjectsOfComponent(string componentName) {
		return GameObject.FindGameObjectsWithTag (componentName);
	}

	public void setColorOfComponentObjects(string componentName, Status status) {
		componentObjects = getObjectsOfComponent(componentName);
		saveOriginalStatus ();
		foreach (GameObject obj in componentObjects) {
			setStatus (obj, status);
		}
	}

	private void saveOriginalStatus() {
		originalStatus = new List<objectStatus> ();
		foreach(GameObject comp in componentObjects) {
			originalStatus.Add(new objectStatus(comp, comp.GetComponent<Renderer>().material.color));
		}
	}


	public void restoreStatus() {
		foreach (objectStatus objStatus in originalStatus) {
			objStatus.obj.GetComponent<Renderer>().material.color = objStatus.color;
		}
		originalStatus.Clear ();
	}


	private void setStatus(GameObject obj, Status status) {
		Material mat = obj.GetComponent<Renderer> ().material;
		switch (status) {
		case Status.green: 
			mat.SetColor("_Color", Color.green);
			break;
		case Status.yellow: 
			mat.SetColor("_Color", Color.yellow);
			break;
		case Status.red:
			mat.SetColor("_Color", Color.red);
			break;
		default:
			mat.SetColor("_Color", Color.grey);
			break;
		}
	}

}
