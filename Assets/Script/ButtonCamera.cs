using UnityEngine;
using System.Collections;

public class ButtonCamera : MonoBehaviour {

	public Camera camera;
	public GameObject ball;

	public void OnMouseDown(){
		Camera.current.transform.position = ball.transform.position;
	}
}
