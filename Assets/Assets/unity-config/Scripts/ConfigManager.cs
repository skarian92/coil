using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour {
	public GameObject panel;
	public InputField serverInputField;
	public string serverUrl = "http://172.20.10.2:3000";

	private string _server;

	void Start () {
		_server = serverUrl;
	}

	public string getServer() {
		return _server;
	}

	public void okClick() {
		_server = serverInputField.text;
		panel.SetActive(false);
		Debug.Log (serverInputField.text);
	}

	public void configClick() {
		bool state = panel.activeSelf;
		if (!state) {
			serverInputField.text = _server;
		}
		panel.SetActive(!state);

	}
	void OnGUI() {
		if (Input.GetKeyDown (KeyCode.C)) {
			panel.SetActive(true);
//			SceneManager.LoadScene ("Config", LoadSceneMode.Single);
//			SceneManager.SetActiveScene(SceneManager.GetSceneByName("Config"));
		}
	}
}
