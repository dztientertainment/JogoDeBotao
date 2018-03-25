using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	void Update () {
		if (GetComponent<Rigidbody2D> ().velocity != Vector2.zero)
			GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (GetComponent<Rigidbody2D> ().velocity, Vector2.zero,Field.adhesion*0.005f);
	}
}
