using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AINIoT : MonoBehaviour{

	public GameObject objectTarget;
	private bool targetFound = false;
	//public GameObject hide;
	string value;	
	public Text equ_Name_SerialNumText;
	public string nameVal;
	public string serialNumVal;
	public Text val1Text;
	public string manufacturerVal;
	public Text val2Text;
	public string modelNameVal;
	public Text buildDateText;
	public string buildDateVal;
	public Text nextServiceText;
	public string nextServiceVal;


	void Update () {
		if (targetFound) {

			Debug.Log ("TARGET FOUND");
		//Setting header val 
		equ_Name_SerialNumText.text = nameVal + " #" + serialNumVal;

		//Setting manufacturer value
		val1Text.text = manufacturerVal;

		//Setting model value
		val2Text.text = modelNameVal;

		//Setting build date 
		buildDateText.text = buildDateVal;

		//Setting next service date
		nextServiceVal = "20-9-2017";
		nextServiceText.text = nextServiceVal;
		}

	}
}
