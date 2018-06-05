using UnityEngine;
using System.Collections;

public class IntensityController : MonoBehaviour {

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

			float t = 12*(Vector2.Distance( Camera.main.ScreenToWorldPoint (Input.mousePosition), i.transform.position ));

			GameObject bar = i.GetComponent<IntensityBar>().bar;
			//Modificar o tamanho apenas da barra de intensidade
			bar.transform.localScale = new Vector3 (t>ButtonPiece.forceMax*12?ButtonPiece.forceMax*12:t,
			                                        bar.transform.localScale.y,
			                                        bar.transform.localScale.z);

			bar.transform.localPosition = new Vector3(	bar.transform.localScale.x/24-0.1f,
			                                     		bar.transform.localPosition.y,
			                                            bar.transform.localPosition.z);

		
		}

		//if (Input.GetKeyDown (KeyCode.Alpha1)) appear ();
		//if (Input.GetKeyDown (KeyCode.Alpha2)) disappear ();

	}
}
