using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	//Arranjo para as cameras do jogo: Use [0] sky, [1] field e [2] ball
	public Camera[] camera;
	public int cameraActual;

	private Vector3 ajust;
	float distanceInitial;


	/**Alterar a camera
	 * Altarar a camera ativa conforme solicitado.
	 * Par1 - camNumber: O numero da camera, dentro do arranjo, a ser ativada.
	 */
	public void changeCameraTo(int camNumber){
		int c = -1;
		while (++c<camera.Length)
			camera[c].gameObject.SetActive (c==camNumber);
		cameraActual = camNumber;
	}

	/**Altarar a camera
	 * Altarar a visualizacao para a proxima camera.
	 */
	public void changeCamera(){
		if (++cameraActual >= camera.Length) cameraActual = 0;
		changeCameraTo (cameraActual);
	}

	void Start () {
		changeCameraTo (0);
	}

	void Update () {
		//Atalho para trocar de camera: 'C'.
		if (Input.GetKeyDown (KeyCode.C)) changeCamera ();
		//Os cliques para ajuste de camera so funcionam para a camera tipo 1 (Field)
		//if (cameraActual == 1) {
			if (Input.GetMouseButtonDown (1)) {
				//print ("CLIQUE 1 EM: " + Camera.main.ScreenToWorldPoint (Input.mousePosition));
				ajust = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			}
			if (Input.GetMouseButton (1)) {
				print ("x: "+(Camera.main.ScreenToWorldPoint (Input.mousePosition).x - ajust.x));
				print ("y: "+(Camera.main.ScreenToWorldPoint (Input.mousePosition).y - ajust.y));

				Camera.main.transform.position -= new Vector3((Camera.main.ScreenToWorldPoint (Input.mousePosition).x - ajust.x)*0.2f
															 ,(Camera.main.ScreenToWorldPoint (Input.mousePosition).y - ajust.y)*0.2f
				                                             ,0);
			}
		//}

/*		//Zoom
		if (Input.GetKeyDown (KeyCode.Z)) Camera.main.orthographicSize += 0.1f;
		if (Input.touchCount == 2) {
			if (Input.GetTouch(1).phase == TouchPhase.Began) {
				distanceInitial = Vector2.Distance(Input.GetTouch (0).position,Input.GetTouch (1).position);
				//Camera.main.orthographicSize -= 0.1f;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved) {
				Camera.current.orthographicSize += Vector2.Distance(Input.GetTouch (0).position, Input.GetTouch (1).position) - distanceInitial;
				if(Camera.current.orthographicSize < 2) Camera.current.orthographicSize = 2;
				if(Camera.current.orthographicSize > 8) Camera.current.orthographicSize = 8;
				//Camera.main.orthographicSize -= 0.1f;
			}
		}
*/

	}


}
