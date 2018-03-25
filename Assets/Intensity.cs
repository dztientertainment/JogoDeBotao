using UnityEngine;
using System.Collections;

public class Intensity : MonoBehaviour {

	//Prefab
	static public GameObject intensity;
	//Instancia
	static private GameObject i;

	static public void appear(){
		//Se ja existe um objeto, exclua-o.
		if(i!=null) GameObject.Destroy( i );
		i = (GameObject)GameObject.Instantiate( intensity, (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition), new Quaternion() );
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

		if (Input.GetKeyDown (KeyCode.Alpha1)) appear ();
		if (Input.GetKeyDown (KeyCode.Alpha2)) disappear ();

		print (i.transform.position);

	}
}
