using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateArt : MonoBehaviour {
	public Material material;
	public bool running  = true;
	private Image image;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
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
			image.enabled = true;
			material.SetVector ("u_resolution", new Vector4 ((float)Screen.width, (float)Screen.height));
			material.SetFloat ("u_time", Time.time);
		} else {
			image.enabled = false;
		}
	}
}
