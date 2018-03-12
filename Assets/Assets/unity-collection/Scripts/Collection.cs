using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Collection : MonoBehaviour {
	public GameObject Red;
	public GameObject Green;
	private List<GameObject> redButtons;
	private List<GameObject> greenButtons;
	private List<CollectionEntry> data;
	public ClickHandler Click;

	public delegate void ClickHandler(GameObject Button);

	public enum Status {
		green,
		red
	}

	public class CollectionEntry {
		public string id;
		public string text;
		public Status status;
		public CollectionEntry(string id, string text, Status status) {
			this.id = id;
			this.text = text;
			this.status = status;
		}
	}

	// Use this for initialization
	void Start () {
		
		redButtons = new List<GameObject> ();
		for (int i = 0; i < Red.transform.childCount; i++) {
			redButtons.Add(Red.transform.GetChild(i).gameObject);
		}
		greenButtons = new List<GameObject> ();
		for (int i = 0; i < Green.transform.childCount; i++) {
			greenButtons.Add(Green.transform.GetChild(i).gameObject);
		}
		List<CollectionEntry> collection = new List<CollectionEntry> ();
		Status status;
		for (int i = 1; i < 5; i++) {
			if (i % 2 == 0) {
				status = Status.red;
			}
			else {
				status = Status.green;
			}
			CollectionEntry entry = new CollectionEntry ("id" + i.ToString (), "text" + i.ToString (), status);
			collection.Add (entry);
		}
		Click = testHandler;
		setData (collection);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setData(List<CollectionEntry> collectionData) {
		// reset all buttons
		resetAllButtons();
		int i = 0;
		foreach (CollectionEntry entry in collectionData) {
			if (entry.status == Status.red) {
				redButtons [i].GetComponentInChildren<Text> ().text = entry.text;
				redButtons [i].SetActive (true);
			} else {
				greenButtons [i].GetComponentInChildren<Text> ().text = entry.text;
				greenButtons [i].SetActive (true);
			}
			i++;
		}
		data = collectionData;
	}

	private void resetAllButtons() {
		foreach (GameObject button in redButtons) {
			button.GetComponentInChildren<Text> ().text = "";
			button.SetActive (false);
		}
		foreach (GameObject button in greenButtons) {
			button.GetComponentInChildren<Text> ().text = "";
			button.SetActive (false);
		}
	}
	private void testHandler(GameObject button) {
		int i = System.Int32.Parse(button.name.Substring(button.name.Length - 1)) - 1;
		string id = data[i].id;		
		Debug.Log ("id: " + id);
	}

	public void handleClick(GameObject button) {
		Click (button);
	}
}
