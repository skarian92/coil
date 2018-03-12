using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// just a helper class to mock loading an equipment
// assumes that there is a game object named "Equipment"
public class EquipmentLoader : MonoBehaviour {
	public delegate void EquipmentLoadedHandler(GameObject equi);
	public event EquipmentLoadedHandler EquipmentLoaded;

	// Use this for initialization
	void Start () {
		
	}
	public void LoadEquipment(string equipmentId) {
		// 
	}
	public void LoadSampleEquipment() {
		// Finds static sample equi
		GameObject go = GameObject.Find("Equipment");
		if (EquipmentLoaded != null) {
			EquipmentLoaded(go);
		}
	}
}
