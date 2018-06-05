using UnityEngine;
using System.Collections;

public class GoalKeeperPiece : MonoBehaviour {

	//Registra se esta instancia pertence ao Time 1
	public bool isTeam1;
	//Registra a identificaçao da instancia: bit a bit
	public int bitId;

	//Area do goleiro: vertice inferior esquerdo
	public Vector2 areaIE;
	//Area do goleiro: vertice superior direito
	public Vector2 areaSD;

	void OnMouseDrag (){

		if (GameController.isTurnOfTeam1==isTeam1 && !GameController.ishalted) {
			//A peça esta em movimento. Impede a camera de se movimentar
			ButtonPiece.isUsing = true;

			float tx = Camera.main.ScreenToWorldPoint (Input.mousePosition).x;
			if(tx > areaSD.x) tx = areaSD.x;
			if(tx < areaIE.x) tx = areaIE.x;
			float ty = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;
			if(ty < areaIE.y) ty = areaIE.y;
			if(ty > areaSD.y) ty = areaSD.y;
			print (tx+","+ty);
			transform.position = new Vector3(tx,ty,transform.position.z);
		}
	}

	void OnMouseUp(){
		ButtonPiece.isUsing = false;
	}

	void FixedUpdate () {
		//Simula um "freio" por aderencia (Field.adhesion) e a massa da pessa (rigidbody2D.mass)
		if (GetComponent<Rigidbody2D> ().velocity != Vector2.zero) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (GetComponent<Rigidbody2D> ().velocity, Vector2.zero, Field.adhesion * 0.01f);
		}
		
		//Simula um "freio" para a rotaçao da peça por aderencia (Field.adhesion)
		if (GetComponent<Rigidbody2D> ().angularVelocity != 0)
			GetComponent<Rigidbody2D> ().angularVelocity += GetComponent<Rigidbody2D> ().angularVelocity>0?-Field.adhesion:Field.adhesion;
		if (Mathf.Abs (GetComponent<Rigidbody2D> ().angularVelocity) < Field.adhesion)
			GetComponent<Rigidbody2D> ().angularVelocity = 0;
	}

}
