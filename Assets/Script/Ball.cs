using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	//Controla o bola do jogo
	void Update () {
		if (GetComponent<Rigidbody2D> ().velocity != Vector2.zero)
			GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (GetComponent<Rigidbody2D> ().velocity, Vector2.zero, Field.adhesion*0.001f);
	}
}
