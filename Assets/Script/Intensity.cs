using UnityEngine;
using System.Collections;

public class Intensity : MonoBehaviour {

	//Prefab
	static public GameObject intensity;
	//Instancia
	static private GameObject i;

	static public void appear(GameObject buttonPiece){
		//Se ja existe um objeto, exclua-o. //DEBUG: Criando varios objetos anonimos na tela .
		if(i!=null) GameObject.Destroy( i );
		i = (GameObject)GameObject.Instantiate(	intensity
		                                      	,buttonPiece.transform.position
		                                        ,Quaternion.identity);
	}
	static public void disappear(){
		GameObject.Destroy( i );
	}

	void Start(){
		intensity = Resources.Load<GameObject> ("Prefab/IntensityBar");
	}

	void Update () {

		if (i != null) {
			Vector3 d = Camera.main.ScreenToWorldPoint (Input.mousePosition) - i.transform.position;
			i.transform.rotation = Quaternion.Euler (0.0f
			                                        ,0.0f
			                                        ,Mathf.Atan2(d.y,d.x)*Mathf.Rad2Deg);
			//print (Vector2.Distance( Camera.main.ScreenToWorldPoint (Input.mousePosition), i.transform.position ));

			float t = 5.5f*(Vector2.Distance( Camera.main.ScreenToWorldPoint (Input.mousePosition), i.transform.position ));
			i.transform.localScale = new Vector3 (t>ButtonPiece.velMax*2?ButtonPiece.velMax*2:t
			                                     ,i.transform.localScale.y
			                                     ,i.transform.localScale.z);
		}

		//if (Input.GetKeyDown (KeyCode.Alpha1)) appear ();
		//if (Input.GetKeyDown (KeyCode.Alpha2)) disappear ();

	}
}
