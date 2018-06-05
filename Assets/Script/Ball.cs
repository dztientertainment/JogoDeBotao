using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	//Regitra o tipo de movimento da bola - 0: rasteiro - 1: Por cima - 2: chute forte
	static public int ballStyle;
	//Armazena o objeto de mensagem na tela
	static public GameObject ballMessage;

	static public bool isBallStyle1; //por cima
	static public bool isBallStyle2; //chute forte

	public Color colorSolid;
	public Color colorTrans;
	public Sprite[] images;


	public GameObject ballStyleMovement;


	//Controla o bola do jogo
	void Update () {
		if (GetComponent<Rigidbody2D> ().velocity != Vector2.zero) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (GetComponent<Rigidbody2D> ().velocity, Vector2.zero, Field.adhesion * 0.001f);
			if (GetComponent<Rigidbody2D> ().velocity.magnitude < 0.1f){
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				doStyle1 = false;
			}
		}

		if (doStyle1) {
			if (GetComponent<Rigidbody2D> ().velocity.magnitude > magnitude*0.55f &&
			    GetComponent<Rigidbody2D> ().velocity.magnitude < magnitude*0.95f) {
				GetComponent<CircleCollider2D>().enabled = false;
				GetComponent<SpriteRenderer>().color = colorTrans;
				transform.localScale = new Vector3(1,1,1);
			}
			else{
				GetComponent<CircleCollider2D>().enabled = true;
				GetComponent<SpriteRenderer>().color = colorSolid;
				transform.localScale = new Vector3(0.5f,0.5f,1);
			}

			//Regsitra o X da funçao a ser aplica da na Equaçao f(x) = size
			float intensity = GetComponent<Rigidbody2D> ().velocity.magnitude / magnitude;
			//EQUACAO DE 2º GRAU para a parabola do tamanho.
			//float size = -2*(intensity*intensity)+2*intensity+1; //100% o percurso e aereo
			//EQUACAO DE 2º GRAU para a parabola: f(x) = - 16.x² + 24.x - 7
			float size = -16*(intensity*intensity)+24*intensity-7; //50% do percurso e aereo
			if (size<1) size = 1;
			//TODO: O objeto tem scala 0.5
			size = size/2;

			transform.localScale = new Vector3(size,size,1);

//			print ("magnitude: "+magnitude);
//			print ("vel mag  : "+GetComponent<Rigidbody2D> ().velocity.magnitude);
//			print ("size     : "+size);
		}

	}

	private float magnitude;
	private bool doStyle1 = false;
	void FixedUpdate(){
		if (GetComponent<Rigidbody2D> ().velocity != Vector2.zero) {
			GameController.ishalted = true;

			if (isBallStyle1) {
				//Usou o chute por cima. Zera o estilo.
				isBallStyle1 = false; ballStyle = 0;
				//captura a velocidade aplicada
				magnitude = GetComponent<Rigidbody2D> ().velocity.magnitude;
				//Ativa a flag para o movimento especifico
				doStyle1 = true;
			}
			if (isBallStyle2) {
				//Usou o chute forte. Zera o estilo.
				isBallStyle2 = false;  ballStyle = 0;
				//Perde as proximas jogadas, veja em ButtonPiece.applyMovement(...)
				//Chute x2
				GetComponent<Rigidbody2D> ().velocity *= 2;
			}
		}
		else
			GameController.ishalted = false;

	}

	void OnMouseDown(){
		if (!GameController.ishalted) {
			if (ballMessage != null)
				GameObject.Destroy (ballMessage);

			ballMessage = (GameObject)GameObject.Instantiate (ballStyleMovement,
		                                                 	 (Vector2)Camera.main.transform.position,
		                                                 	 Quaternion.identity);
		}
	}
}
