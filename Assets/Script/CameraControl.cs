using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Vector3 ajust;
	private float distance;

	void Update () {
		//Os cliques para ajuste de camera so funcionam para a camera tipo 1 (Field)
		if (Input.GetMouseButtonDown (0) && !ButtonPiece.isUsing) {
			//print ("CLIQUE 1 EM: " + Camera.main.ScreenToWorldPoint (Input.mousePosition));
			ajust = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
		if (Input.GetMouseButton (0) && !ButtonPiece.isUsing) {
			Camera.main.transform.position -= new Vector3((Camera.main.ScreenToWorldPoint (Input.mousePosition).x - ajust.x)*0.2f
		 												 ,(Camera.main.ScreenToWorldPoint (Input.mousePosition).y - ajust.y)*0.2f
		 	                                             ,0);
		}

		//Zoom
		if (Input.GetKeyDown (KeyCode.Z)) Camera.main.orthographicSize += 0.1f;
		if (Input.GetMouseButtonDown (1)) {
			ajust = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//Camera.main.orthographicSize -= 0.1f;
		}
		if (Input.GetMouseButton (1)) {
			distance = 	ajust.x - Camera.main.ScreenToWorldPoint (Input.mousePosition).x +
						ajust.y - Camera.main.ScreenToWorldPoint (Input.mousePosition).y;
			Camera.main.orthographicSize += distance*0.01f;

		}

	}


}
