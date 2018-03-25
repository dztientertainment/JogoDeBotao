using UnityEngine;
using System.Collections;

public class ButtonPiece : MonoBehaviour {

	//Força do botao
	public int force;

	private Vector3 clickInitial;
	private Vector3 clickDiference;

	void Update () {
		//Esta açao começa em OnMouseDown().
		//Se o objeto foi clicado (clickInitial!=0), entao...
		if (clickInitial != Vector3.zero) {
			//Processaa a distancia entre o toque inicial e posiçao atual do click/touch.
			if (Input.GetMouseButton (0)) {
				//clickDistance = Vector2.Distance (clickInitial, Camera.main.ScreenToWorldPoint (Input.mousePosition));
				clickDiference = clickInitial - Camera.main.ScreenToWorldPoint (Input.mousePosition);
			}
			//Ao liberar, acionar o montante da distancia como força oa objeto
			if (Input.GetMouseButtonUp (0)) {
				GetComponent<Rigidbody2D> ().AddForce ( clickDiference*force );
				clickInitial = Vector3.zero;

				Intensity.disappear();

			}
		}

		//Simula um "freio" por aderencia (Field.adhesion)
		if (GetComponent<Rigidbody2D> ().velocity != Vector2.zero)
			GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (GetComponent<Rigidbody2D> ().velocity, Vector2.zero,Field.adhesion*0.01f);

		//Simula um "freio" para a rotaçao da peça por aderencia (Field.adhesion)
		if (GetComponent<Rigidbody2D> ().angularVelocity != 0)
			GetComponent<Rigidbody2D> ().angularVelocity += GetComponent<Rigidbody2D> ().angularVelocity>0?-Field.adhesion:Field.adhesion;
		if (Mathf.Abs (GetComponent<Rigidbody2D> ().angularVelocity) < Field.adhesion)
			GetComponent<Rigidbody2D> ().angularVelocity = 0;
	}

	void OnMouseDown(){
		//Captura a posiçao inicial do clique/touch sobre o objeto
		if (Input.GetMouseButtonDown (0)) {
			clickInitial = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			Intensity.appear();
		}
	}


}
