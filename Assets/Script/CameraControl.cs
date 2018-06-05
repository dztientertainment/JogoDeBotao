using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CameraControl : MonoBehaviour {

	public Text text;

	//Valor minimo para o Zoom
	public float zoomMin;
	//Valor maximo para o Zoom
	public float zoomMax;

	private Vector3 ajust;
	private float distanceInitial;

	private int focunOn = -1;
	//Focar a camera na [0] Bola, [1] Gol 1, [2] Gol 2, [3] Campo.
	public void focusOn(int opc){
		if (opc < 0) opc = 0;
		if (opc > 3) opc = 3;

		if(opc==0)
			Camera.main.transform.position = new Vector3(GameController.ball.transform.position.x,
		                                            	 GameController.ball.transform.position.y,
		                                             	 Camera.main.transform.position.z);
		if(opc==1)
			Camera.main.transform.position = new Vector3(GameController.goal1.transform.position.x,
			                                             GameController.goal1.transform.position.y,
			                                             Camera.main.transform.position.z);
		if(opc==2)
			Camera.main.transform.position = new Vector3(GameController.goal2.transform.position.x,
			                                             GameController.goal2.transform.position.y,
			                                             Camera.main.transform.position.z);
		if(opc==3)
			Camera.main.transform.position = new Vector3(GameController.field.transform.position.x,
			                                             GameController.field.transform.position.y,
			                                             Camera.main.transform.position.z);
	}

	void Update () {
		if (Input.GetMouseButtonDown (0) && !ButtonPiece.isUsing) {
				ajust = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
		//DEBUG: Desconsiderar o botao direito evita erros no Android, ao pressionar a tela com os dois dedos
		if (Input.GetMouseButton (0) && !ButtonPiece.isUsing && !Input.GetMouseButton (1)) {
				Camera.main.transform.position -= new Vector3(	(Camera.main.ScreenToWorldPoint (Input.mousePosition).x - ajust.x)*0.2f,
		 												  	  	(Camera.main.ScreenToWorldPoint (Input.mousePosition).y - ajust.y)*0.2f,
		 	                                              		0);
		}

		//Zoom
		if (Input.GetKeyDown (KeyCode.DownArrow)) Camera.main.orthographicSize -= 0.5f;
		if (Input.GetKeyDown (KeyCode.UpArrow))   Camera.main.orthographicSize += 0.5f;

		//DEBUG. Ao cancelar o movimento do botao, a cemara se movimenta
		if (Input.GetMouseButtonDown (1) && !Input.GetMouseButtonDown (0)) {
			ajust = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//Camera.main.orthographicSize -= 0.1f;
		}
		/*
		if (Input.GetMouseButton (1)) {
			distance = 	ajust.x - Camera.main.ScreenToWorldPoint (Input.mousePosition).x +
						ajust.y - Camera.main.ScreenToWorldPoint (Input.mousePosition).y;
			Camera.main.orthographicSize += distance*0.01f;

		}
		*/

		if (Input.touchCount == 2) {
			if (Input.GetTouch(1).phase == TouchPhase.Began) {
				distanceInitial = Vector2.Distance(Input.GetTouch (0).position,Input.GetTouch (1).position);
				//Camera.main.orthographicSize -= 0.1f;
			}

			if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved) {

				text.text = (distanceInitial - Vector2.Distance(Input.GetTouch (0).position, Input.GetTouch (1).position)).ToString();

/*
				Camera.current.orthographicSize += Vector2.Distance(Input.GetTouch (0).position, Input.GetTouch (1).position) - distanceInitial;
				if(Camera.current.orthographicSize < zoomMin) Camera.current.orthographicSize = zoomMin;
				if(Camera.current.orthographicSize > zoomMax) Camera.current.orthographicSize = zoomMax;
*/
			}

		}



		//Focuses
		if (Input.GetKeyDown (KeyCode.B)) focusOn (0);
		if (Input.GetKeyDown (KeyCode.Alpha1)) focusOn (1);
		if (Input.GetKeyDown (KeyCode.Alpha2)) focusOn (2);


	}


}
