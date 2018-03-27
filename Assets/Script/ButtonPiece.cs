using UnityEngine;
using System.Collections;

public class ButtonPiece : MonoBehaviour {
	//Caso verdadeiro, nao pode mover a camera. Pois o botao esta usando o clique/touch
	static public bool isUsing = false;
	//Velocidade maxima. DEBUG: Evitar transpassar colisores.
	static public int velMax = 10;
	//Magnitude maxima. //TODO reduzir a magnitude para 2 ou 2.5. e ajustar linearidade nas diagotnais.
	static public int distanceMax = 4;

	//Força do botao
	public int force;

	//Valor de decremento para entrar em LERP
	public float waitToLerp = 0.0f;

	private Vector3 clickInitial;
	private Vector3 clickDiference;

	void Update () {
		//Esta açao começa em OnMouseDown().
		//Se o objeto foi clicado (clickInitial!=0), entao...
		if (clickInitial != Vector3.zero) {
			//Processa a distancia entre o toque inicial e posiçao atual do click/touch.
			if (Input.GetMouseButton (0)) {
				waitToLerp = Vector2.Distance(clickInitial, Camera.main.ScreenToWorldPoint (Input.mousePosition));
				if(waitToLerp>distanceMax) waitToLerp = distanceMax;
				waitToLerp *= 2;

			}
			//Ao liberar, acionar o montante da distancia como força ao objeto
			if (Input.GetMouseButtonUp (0)) {

				GetComponent<Rigidbody2D> ().velocity = (clickInitial - Camera.main.ScreenToWorldPoint (Input.mousePosition));
				//Zerar o clickInitial
				clickInitial = Vector3.zero;
				//Retira a barra de força da tela.
				Intensity.disappear();
				//Marca que o botao liberou o clique/touch para a camera
				isUsing = false;
			}
		}

	}

	void FixedUpdate () {
		//Simula um "freio" por aderencia (Field.adhesion) e a massa da pessa (rigidbody2D.mass)
		if (GetComponent<Rigidbody2D> ().velocity != Vector2.zero) {
			if (waitToLerp > 0){
				waitToLerp -= Field.adhesion*0.01f*GetComponent<Rigidbody2D>().mass;
				float x = (Mathf.Abs(GetComponent<Rigidbody2D> ().velocity.x) > velMax)? (GetComponent<Rigidbody2D>().velocity.x<0?-1:1)*velMax : (GetComponent<Rigidbody2D>().velocity.x);
				float y = (Mathf.Abs(GetComponent<Rigidbody2D> ().velocity.y) > velMax)? (GetComponent<Rigidbody2D>().velocity.y<0?-1:1)*velMax : (GetComponent<Rigidbody2D>().velocity.y);
				GetComponent<Rigidbody2D> ().velocity = new Vector2(x,y);
			}
			else
				GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (GetComponent<Rigidbody2D> ().velocity, Vector2.zero, Field.adhesion * 0.01f);
		}
		
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
			//Exibe a barra de intensidade
			Intensity.appear( this.gameObject );
			//Marca que o botao esta usando o clique/touch. Isso inutilizara o zoom.
			isUsing = true;
		}
	}


}
