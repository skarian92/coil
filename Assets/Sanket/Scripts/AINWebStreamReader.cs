using UnityEngine;
using System.Collections.Generic;
using System.Net;


public class AINWebStreamReader {

	volatile bool running = false;
	string url = "";
	System.Threading.Thread thread = null;
	Queue<string> buffer = new Queue<string>();
	object lockHandle = new object();
	string message;

//	~AINWebStreamReader()
//	{
//		Dispose();
//	}

//	public void Start1(string aURL)
//	{
//		if (!running)
//		{
//			url = aURL;
//			thread = new System.Threading.Thread(Run);
//			thread.Start();
//		}
//	}
	public void Start1(string aURL)
	{
		Debug.Log ("WEB_STREAM_READER main func");
		Debug.Log ("URL : " + aURL);
		running = true;
		ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
		var request = System.Net.HttpWebRequest.Create(aURL);
		//Add authorization here
		Debug.Log ("STream request of web stream reader : "+ request);
		var response = request.GetResponse();
		Debug.Log ("STream response of web stream reader : "+ response);
		var stream = response.GetResponseStream();
		byte[] data = new byte[3000];
		message = "";
		int count = stream.Read(data, 0, 3000);
		Debug.Log ("Count : " + count);
		if (count > 0)
		{
			
			message = System.Text.UTF8Encoding.UTF8.GetString(data, 0, count);
			Debug.Log ("Message is : " + message);
				//buffer.Enqueue(message);

		}
		
	}

	public string GetNextBlock()
	{
		
		Debug.Log ("Inside GetNextBlock");
		Debug.Log ("Message is : " + message);
		return message;
	}

//	public void Dispose()
//	{
//		running = false;
//		thread.Abort();
//	}
}
