using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCat : MonoBehaviour {
	public bool running  = true;
	// Use this for initialization
	void Start () {
	}

	public void start() {
		running = true;
	}

	public void stop() {
		running = false;
	}

	// Update is called once per frame
	void Update () {
		//		GetComponent<Renderer> ().material.SetFloat ("u_time", Time.time);
		if (running) {
			gameObject.SetActive (true);
		} else {
			gameObject.SetActive (false);
		}
	}
}
