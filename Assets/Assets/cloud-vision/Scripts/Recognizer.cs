using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recognizer : MonoBehaviour {

//	public string screenShotURL= "http://www.my-server.com/cgi-bin/screenshot.pl";
	public string screenShotURL= "http://172.20.10.2:3000/upload";
//	public string screenShotURL= "http://192.168.1.66:3000/upload";
	public GameObject BusyIndicator;
	public Text assetId;
	public GameObject confirmationDialog;
	public enum Mode
	{
		Record,
		Processing,
		Result,
		Confirmed

	};
	private Mode mode = Mode.Record;
	private string recognizedText;
	private bool isWaitingForResponse = false;
	public delegate void TextRecognizedHandler(string text);
	public event TextRecognizedHandler TextRecognized; 
	public ConfigManager configManager;



	// Use this for initialization
	void Start () {
		recognizedText = "test";
//		configManager = GameObject.Find ("ConfigManager").GetComponentsInChildren<ConfigManager>()[0];
	}

	public void Update() {
		if (BusyIndicator && isWaitingForResponse) {
			BusyIndicator.SetActive (true);
		}
		if (BusyIndicator && !isWaitingForResponse) {
			BusyIndicator.SetActive (false);
		}

//		if (Input.GetButtonDown ("Jump")) {
//		if (Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Jump")) {
//			StartCoroutine (UploadPNG ());
//		}
	}

	IEnumerator UploadPNG() {
		string url = configManager.getServer () + "/upload";
		isWaitingForResponse = true;
		// We should only read the screen after all rendering is complete
		BusyIndicator.SetActive(false);
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		var tex = new Texture2D( width, height, TextureFormat.RGB24, false );

		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();
		BusyIndicator.SetActive (true);
		// Encode texture into PNG
		byte[] bytes = tex.EncodeToPNG();
		Destroy( tex );

		// Create a Web Form
		WWWForm form = new WWWForm();
		form.AddField("frameCount", Time.frameCount.ToString());
		form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");
		Dictionary<string, string> headers = form.headers;
		headers ["content-length"] = bytes.Length.ToString();

		// Upload to a cgi script
		WWW w = new WWW(url, form);
		yield return w;
		isWaitingForResponse = false;
		if (BusyIndicator) {
//			BusyIndicator.SetActive (false);
		}
		if (!string.IsNullOrEmpty(w.error)) {
			print(w.error);
		}
		else {
			print("Finished Uploading Screenshot");
			mode = Mode.Result;
			recognizedText = w.text;
			//recognizedText = "Sanket is here";
			Debug.Log ("response: " + w.text);
			//Debug.Log ("response: " + recognizedText);
			if (!string.IsNullOrEmpty(recognizedText)) {
				confirmationDialog.SetActive (true);
			}
		}
	}

	public void confirmationDialogHandler(string choice) {
		confirmationDialog.SetActive (false);
		mode = Mode.Record;
		if (choice.CompareTo ("ok") == 0) {
			if (TextRecognized != null) {
				TextRecognized (assetId.text);
			}
		} else {
			mode = Mode.Record;
		}
	}

	void OnGUI()
	{
		if (recognizedText != null) {
			assetId.text = recognizedText;
		}			
		string modeString;
		if (mode == Mode.Record) {
			modeString = "Text";
		} else if (mode == Mode.Result) {
			modeString = "Close";

		} else {
			return;
		}
		if (mode != Mode.Processing) {
			if (GUI.Button (new Rect (Screen.width - 150.0f, 0.0f, 150.0f, 100.0f), modeString)) {
				if (mode == Mode.Record) {
					StartCoroutine (UploadPNG ());
					mode = Mode.Processing;
				} else {
					// close text display
					mode = Mode.Record;
				}
			}
		}
		if (mode == Mode.Result) {
			confirmationDialog.SetActive (true);

		}
		
//		if (mode == Mode.Result) {
//		if (true) {
//			GUIStyle style = GUI.skin.textArea;
//			style.fontSize = 32;
//			GUI.TextArea( new Rect(Screen.width/2-Screen.width/8, Screen.height/2-Screen.height/8, Screen.width/4, Screen.height/4), recognizedText);
//		}
	}
}

