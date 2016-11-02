using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {

	public float speed = 30;
	private int leftScore = 0;
	private int rightScore = 0;
	public static Vector3 ballPosition;


	void Start() {
		// Initial Velocity
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}



	void Update(){
		ballPosition = GetComponent<Rigidbody2D> ().position;
	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
	                float racketHeight) {

		return (ballPos.y - racketPos.y) / racketHeight;
	}

	void OnCollisionEnter2D(Collision2D col) {
		// Note: 'col' holds the collision information. If the
		// Ball collided with a racket, then:
		//   col.gameObject is the racket
		//   col.transform.position is the racket's position
		//   col.collider is the racket's collider
		 
		if (ServerScript.ServerIsRunning)
			return;

		//Debug.Log (col.gameObject.transform.position );

		// Hit the left Racket?
		if (col.gameObject.name == "RacketLeft") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
			                    col.transform.position,
			                    col.collider.bounds.size.y);
			
			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(1, y).normalized;
			
			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
		}
		
		// Hit the right Racket?
		if (col.gameObject.name == "RacketRight") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
			                    col.transform.position,
			                    col.collider.bounds.size.y);
			
			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(-1, y).normalized;
			
			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
		}

		if (col.gameObject.name == "WallLeft") {
			Text txt = GameObject.Find ("LeftScore").GetComponent<Text> ();
			rightScore = rightScore + 1;
			txt.text = rightScore.ToString();
		}

		if (col.gameObject.name == "WallRight") {
			Text txt = GameObject.Find ("RightScore").GetComponent<Text> ();
			leftScore = leftScore + 1;
			txt.text = leftScore.ToString();
		}
	}
}
