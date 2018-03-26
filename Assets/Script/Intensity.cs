using UnityEngine;
using System.Collections;

public class Intensity : MonoBehaviour {

	//Prefab
	static public GameObject intensity;
	//Instancia
	static private GameObject i;

	static public void appear(GameObject buttonPiece){
		//Se ja existe um objeto, exclua-o. //DEBUG: Criar varios objetos anonimos na tela .
		if(i!=null) GameObject.Destroy( i );
		i = (GameObject)GameObject.Instantiate(	intensity
		                                      	,buttonPiece.transform.position
		                                        ,Quaternion.LookRotation( Camera.main.ScreenToWorldPoint (Input.mousePosition)) );
		//i.transform.position [2] = 0;
	}
	static public void disappear(){
		GameObject.Destroy( i );
	}
	static public void render(){

	}

	void Start(){
		intensity = Resources.Load<GameObject> ("Prefab/Intensity");
	}

	void Update () {

		if (i != null) {

			/*Vector3 dif = Camera.main.ScreenToWorldPoint (Input.mousePosition) - i.transform.position;
			Quaternion angle= Quaternion.LookRotation(dif, i.transform.up);
			i.transform.rotation = Quaternion.Lerp(i.transform.localRotation, angle, Time.deltaTime);
			*/

			//MAIS SIMPLES
			//i.transform.LookAt(Camera.main.ScreenToWorldPoint (Input.mousePosition));

			/*Vector3 relativePos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - i.transform.position;
			Quaternion rotation = Quaternion.LookRotation( relativePos );
			i.transform.rotation = rotation;
			*/

			/*Vector3 vectorToTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition) - i.transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			i.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1);
			*/

			//i.gameObject.transform.rotation.SetLookRotation( Camera.main.ScreenToWorldPoint (Input.mousePosition) );
		}

		//if (Input.GetKeyDown (KeyCode.Alpha1)) appear ();
		//if (Input.GetKeyDown (KeyCode.Alpha2)) disappear ();

	}
}
