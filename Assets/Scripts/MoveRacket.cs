using UnityEngine;
using System.Collections;

public class MoveRacket : MonoBehaviour {
	public float speed = 30; 
	public string axis = "";
	public static float RacketPosition;

	void FixedUpdate () {
		float v = Input.GetAxisRaw(axis);
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
	}

	void Update(){
		RacketPosition = GameObject.Find ("RacketRight").transform.position.y;
	}

}
