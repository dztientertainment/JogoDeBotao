using UnityEngine;
using System.Collections;

public class ButtonCamera : MonoBehaviour {

	public CameraControl cameraControl;

	void OnMouseDown(){
		cameraControl.changeCamera ();
		
	}
}
