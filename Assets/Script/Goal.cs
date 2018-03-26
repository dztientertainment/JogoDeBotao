using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public int id;
	//Registra a quantidade de gols levados
	public int count = 0;

	void OnTriggerEnter2D( Collider2D other ){
		if (other.gameObject.Equals (GameController.ball)) {
			count++;

			//TODO Criar um "RESET POSITIONS"
			GameController.ball.transform.position = new Vector3 (0,0,0);
			GameController.ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

	}

	//void OnCollisionEnter2D( Collision2D coll ){print ("COLLISION");}
}
