using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.SceneManagement;

public class ServerScript : MonoBehaviour {


	public Button button;
	private const int listenPort = 11000;
	static float ReceiveData;
	public static Boolean ServerIsRunning = false;
	String RacketPosition;
	String BallPositionX;
	String BallPositionY;

	void Start () {
		Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void Update(){

		if (GameObject.Find ("RacketLeft") == null)
			return;
		
		RacketPosition = GameObject.Find ("RacketLeft").transform.position.y.ToString();
		BallPositionX = BallScript.ballPosition.x.ToString();
		BallPositionY = BallScript.ballPosition.y.ToString();

		if(ServerIsRunning)
			GameObject.Find ("RacketRight").transform.position = new Vector2(4.96f,ReceiveData);
	}

	void TaskOnClick(){
		var t = new Thread (() => (StartListener()));
		t.Start ();
		ServerIsRunning = true;
		//SceneManager.LoadScene("Game",LoadSceneMode.Additive);
	}

	private void StartListener(){
		
		UdpClient listener = new UdpClient(listenPort);

		try 
		{
			while (true)
			{
				var remoteEP = new IPEndPoint(IPAddress.Any, 11000); 
				var data = listener.Receive(ref remoteEP); // listen on port 

				ReceiveData = float.Parse(Encoding.ASCII.GetString(data));

				byte[] senddata = Encoding.ASCII.GetBytes(RacketPosition + ";" + BallPositionX + ";" + BallPositionY);
				listener.Send(senddata, senddata.Length, remoteEP); // reply back
			}

		} 
		catch (Exception e) 
		{
			Console.WriteLine(e.ToString());
		}
		finally
		{
			listener.Close();
		}
	}

}