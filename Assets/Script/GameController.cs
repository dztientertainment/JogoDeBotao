using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static public GameObject ball;
	static public GameObject camera;

	void Start () {
		ball = GameObject.Find ("Ball");
		camera = GameObject.Find ("Camera");
	}


	void Update () {
	

	}
}
