using UnityEngine;
using System.Collections;

public class ButtonPiece : MonoBehaviour {
	//Caso verdadeiro, nao pode mover a camera. Pois o botao esta usando o clique/touch
	static public bool isUsing = false;
	//Velocidade maxima. DEBUG: Evitar transpassar colisores.
	static public int velMax = 10;
	//Magnitude maxima. //TODO reduzir a magnitude para 2 ou 2.5. e ajustar linearidade nas diagonais.
	static public int forceMax = 4;
	//Registra se esta instancia pertence ao Time 1
	public bool isTeam1;
	//Registra a identificaçao da instancia: bit a bit
	public int bitId;

	//Valor de decremento para entrar em LERP
	public float waitToLerp = 0.0f;

	//Posiçao inicial do clique/toque em tela
	private Vector3 clickInitial;

	//Zera todas as flags
	public void resetMovement(){
			//Zerar o clickInitial
			clickInitial = Vector3.zero;
			//Retirar a barra de força da tela.
			IntensityController.disappear();
			//Marca que o botao liberou o clique/touch para a camera
			isUsing = false;
	}

	public void applyMovement(Vector2 movement, float force){
		//x2 na força e movimento. DEBUG: movimentos fracos.

		//waitToLert equivale a força empregada na peça
		waitToLerp = force*2;

		//TODO limitar a velocidade do movimento
		GetComponent<Rigidbody2D> ().velocity = movement*2;

		if (Ball.isBallStyle2)
			GameController.MovementCount = GameController.movementsForTurn;
		else
			GameController.MovementCount++;

		StartCoroutine ( GameController.showArrowTurn() );

	}


	void Update () {
		//Esta açao começa em OnMouseDown().
		//Se o objeto foi clicado (clickInitial!=0), entao...
		if (clickInitial != Vector3.zero) {
			if (Input.GetMouseButton (0)){
				//se clicou tambem o botao direito, cancela a jogada
				if (Input.GetMouseButtonUp (1)) resetMovement();

//				print ("FORÇA: "+(Vector2.ClampMagnitude( clickInitial - Camera.main.ScreenToWorldPoint (Input.mousePosition) , forceMax )).magnitude);

			}

			//Ao liberar, acionar o montante da distancia como força ao objeto
			if (Input.GetMouseButtonUp (0)) {

				Vector2 direction = clickInitial - Camera.main.ScreenToWorldPoint (Input.mousePosition);
				float clamped = (Vector2.ClampMagnitude( direction , forceMax )).magnitude;
				//Aplica o movimento no botao, forncecendo a direçao e força (que significa o tempo a percorrer antes iniciar o freio)
				applyMovement ( direction, clamped);

				resetMovement();
			}
		}

		runIA ();


	}

	void OnMouseDown(){
		//Captura a posiçao inicial do clique/touch sobre o objeto
		if (Input.GetMouseButtonDown (0) && !GameController.ishalted) {
			//Permite movimento se (time 1 no turno do time 1) e (time 2 no turno do time 2)
			if(GameController.isTurnOfTeam1==isTeam1){
				clickInitial = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//Exibe a barra de intensidade
				IntensityController.appear( this.gameObject );
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
