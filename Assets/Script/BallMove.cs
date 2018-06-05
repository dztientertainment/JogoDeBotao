using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {

	public Ball ball;

	void Start () {
		ball = GameObject.Find ("Ball").GetComponent<Ball> ();

		if (++Ball.ballStyle >= ball.images.Length)
			Ball.ballStyle = 0;

		Ball.isBallStyle1 = Ball.ballStyle == 1;
		Ball.isBallStyle2 = Ball.ballStyle == 2;

		GetComponent<SpriteRenderer>().sprite = ball.images[Ball.ballStyle];
		StartCoroutine ( showMe() );
	}

	//Exibe a seta que indica o sentido da jogada
	public IEnumerator showMe(  ){
		yield return new WaitForSeconds (1);
		GameObject.Destroy( this.gameObject );
	}
}
