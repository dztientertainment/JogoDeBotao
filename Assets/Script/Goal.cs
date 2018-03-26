using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public int id;

	void OnTriggerEnter2D( Collider2D other ){
		if (other.gameObject.Equals (GameController.ball)) {
			print ("GOL "+id);

			//TODO Criar um "RESET POSITIONS"
			GameController.ball.transform.position = new Vector3 (0,0,0);
			GameController.ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

	}

	//void OnCollisionEnter2D( Collision2D coll ){print ("COLLISION");}
}
