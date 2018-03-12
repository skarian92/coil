using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	public Camera PlayerCamera;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
//		transform.LookAt(transform.position + PlayerCamera.transform.rotation * Vector3.forward,
//			PlayerCamera.transform.rotation * Vector3.up);
		Vector3 direction = PlayerCamera.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation(-direction);
	}
}
