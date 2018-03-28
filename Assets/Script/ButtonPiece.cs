using UnityEngine;
using System.Collections;

public class ButtonPiece : MonoBehaviour {
	//Caso verdadeiro, nao pode mover a camera. Pois o botao esta usando o clique/touch
	static public bool isUsing = false;
	//Velocidade maxima. DEBUG: Evitar transpassar colisores.
	static public int velMax = 20;
	//Magnitude maxima. //TODO reduzir a magnitude para 2 ou 2.5. e ajustar linearidade nas diagotnais.
	static public int forceMax = 4;
	//Registra se esta instancia pertence ao Time 1
	public bool isTeam1;

	//Valor de decremento para entrar em LERP
	public float waitToLerp = 0.0f;

	//Posiçao inicial do clique/toque em tela
	private Vector3 clickInitial;

	//Zera todas as flags
	public void resetMovement(){
			//Zerar o clickInitial
			clickInitial = Vector3.zero;
			//Retirar a barra de força da tela.
			Intensity.disappear();
			//Marca que o botao liberou o clique/touch para a camera
			isUsing = false;
	}

	public void applyMovement(Vector2 movement, float force){
		print ("force: " + force + " forceMax: "+forceMax);
		//waitToLert equivale a força empregada na peça
		waitToLerp = force > forceMax ? forceMax : force;
		print ("applyMovement POS - waitToLerp: " + waitToLerp);

		//x2 na força e movimento. DEBUG: movimentos estavam fracos.
		//TODO limitar a velocidade do movimento
		GetComponent<Rigidbody2D> ().velocity = movement*2;
		waitToLerp = force*2;

		GameController.MovementCount++;
		//if (GameController.MovementCount==0) StartCoroutine ( GameController.appearArrow(GameController.isTurnOfTeam1) );
	}


	void Update () {
		//Esta açao começa em OnMouseDown().
		//Se o objeto foi clicado (clickInitial!=0), entao...
		if (clickInitial != Vector3.zero) {
			//se clicou tambem o botao direito, cancela a jogada
			if (Input.GetMouseButton (0) && Input.GetMouseButtonUp (1)) resetMovement();

			//Ao liberar, acionar o montante da distancia como força ao objeto
			if (Input.GetMouseButtonUp (0)) {

				applyMovement (
					//Calcula a direçao do movimento
					clickInitial - Camera.main.ScreenToWorldPoint (Input.mousePosition),
					//Calcula a força do movimento, o qual dara um tempo a percorrer antes iniciar o freio
				    Vector2.Distance(clickInitial, Camera.main.ScreenToWorldPoint (Input.mousePosition))
				);

				resetMovement();
			}
		}

		runIA ();
	}

	void OnMouseDown(){
		//Captura a posiçao inicial do clique/touch sobre o objeto
		if (Input.GetMouseButtonDown (0)) {
			//Permite movimento se (time 1 no turno do time 1) e (time 2 no turno do time 2)
			if(GameController.isTurnOfTeam1==isTeam1){
				clickInitial = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//Exibe a barra de intensidade
				Intensity.appear( this.gameObject );
				//Marca que o botao esta usando o clique/touch. Isso inutilizara o zoom.
				isUsing = true;
			}
			else{
				//TODO //Exibir algum aviso visual informando erro 
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




	public void runIA(){
		if (Input.GetKeyDown (KeyCode.S)) 
			applyMovement (GameController.goal1.transform.position, 3);
	
	}

}
