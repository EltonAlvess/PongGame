using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ClientScript : MonoBehaviour {
	public Button button;
	static String RacketPosition;
	static String Data="";

	void Start () {
		Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	// Update is called once per frame
	void Update () {
		RacketPosition = MoveRacket.RacketPosition.ToString();
		Debug.Log (Data.Length);

		if (!ServerScript.ServerIsRunning && Data.Length > 0)
			GameObject.Find ("Ball").transform.position = new Vector2 (float.Parse(Data.Split(new char[]{';'})[1]),float.Parse(Data.Split(new char[]{';'})[2]));
	}

	void TaskOnClick(){
		if (!ServerScript.ServerIsRunning) {
			Thread t = new Thread (() => SendMsg ());
			t.Start ();
		}
	}

	 private static void SendMsg() 
	{
		try{
			while (true)
			{
				var client = new UdpClient();
				IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000); 
				client.Connect(ep);
				byte[] senddata = Encoding.ASCII.GetBytes(RacketPosition);

				client.Send(senddata, senddata.Length);

				var receivedData = client.Receive(ref ep);
				Data = Encoding.ASCII.GetString(receivedData);
				Debug.Log ("Racket position from Server: " + Data);
			}
		}catch(Exception ex){		
			Debug.Log (ex.Message.ToString ());
		}
		finally{
			
		}
	}


}
