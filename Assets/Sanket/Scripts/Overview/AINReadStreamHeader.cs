using UnityEngine;
using System.Collections.Generic;
using System.Net;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Threading;
using System.Globalization;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;   
//using Newtonsoft.Json.Serialization;

public class AINReadStreamHeader : MonoBehaviour {

	//public string PhotonParticleURL = "https://api.particle.io/v1/devices/events?access_token=649e7d09d0980e4b649e42f6dcff79887d9570e2";
	private string host = "";
	public string equipmentUrl ="";
	private string modelUrlFrontPart = "";
	private string modelUrlEndPart = 	"";

	AINWebStreamReader request = null;

	EquipmentGET equipment = new EquipmentGET();

	[System.Serializable]
	public class EquipmentGET
	{
		public string equipmentId;   
		public bool hasInrevision;
		public double equipmentVersion;  
		public int completeness;
		public DateTime publishedOn;
		public string status;
		//public Completeness sectionCompleteness;
		public string sourceBPRole;
		public bool modelKnown;
		public string lifecycle;
		public string operatorName;
		public string manufacturerName;
		public string name;
		public string modelId;
		//[JsonIgnore]
		public DateTime buildDate;
//		[JsonProperty(PropertyName = "buildate")]
//		public string buildDateString
//		{
//			get { return this.buildate.ToString ("yyyy-M-d");}
//			set { this.buildate = DateTime.Parse (value);}
//		}
		public string serialNumber;
		public string batchNumber;

	}

	[System.Serializable]
	public class ModelGET
	{
		public String name;
		public String manufacturer;

	}



	void Start()
	{
		//parseDataHumidity = new DataClass ();
		//StartCoroutine(WRequest());
		getData();
	}

	void Update() {

	}
		
	// to be called for the first time or when refresh button is pressed
	public void getData()
	{
		Debug.Log ("getData funtion of AINReadStream");
		request = new AINWebStreamReader();
		//request.Start1(Url);
		string stream = request.GetNextBlock();
		Debug.Log ("getdata stream: stream");
			
				
		if (!string.IsNullOrEmpty(stream)){
			
			equipment = JsonUtility.FromJson<EquipmentGET> (stream);
		
			//equipment = JsonConvert.DeserializeObject<EquipmentGET>(stream, 
			//	new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" }); 				
			gameObject.GetComponent<AINIoT> ().nameVal = equipment.name;
			Debug.Log ("NAMEEEEE" + equipment.name);
			//check if serial number is null give batch num
			gameObject.GetComponent<AINIoT> ().serialNumVal = equipment.serialNumber;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
			gameObject.GetComponent<AINIoT> ().buildDateVal = Convert.ToString(equipment.buildDate.ToShortDateString());
			gameObject.GetComponent<AINIoT> ().nextServiceVal = "21-9-2017";

			ModelGET model = getModelData (equipment.modelId);
			if (string.IsNullOrEmpty (model.manufacturer)) {
				gameObject.GetComponent<AINIoT> ().manufacturerVal = "Acme Pumps";
			} else {
				gameObject.GetComponent<AINIoT> ().manufacturerVal = model.manufacturer;
			}
			gameObject.GetComponent<AINIoT> ().modelNameVal = model.name;

		}
				
	}

	public ModelGET getModelData(string modelID)
	{
		AINWebStreamReader requestModel = new AINWebStreamReader();
		requestModel.Start1(modelUrlFrontPart+ modelID + modelUrlEndPart);
		string stream = requestModel.GetNextBlock();
		ModelGET model = JsonUtility.FromJson<ModelGET> (stream);

		return model;
	}

//	void OnApplicationQuit()
//	{
//		if (request != null)
//			request.Dispose();
//	}

	void OnDataUpdate(decimal aAmount)
	{
		Debug.Log("Received new amount: " + aAmount);
		// Do whatever you want with the value
	}
}
