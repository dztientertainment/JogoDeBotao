using UnityEngine;
using System.Collections;

public class ArrowTurn : MonoBehaviour {

	// Use this for initialization
	void Start () {

		transform.Rotate (0, 0, GameController.isTurnOfTeam1?90:270,0);

		StartCoroutine ( showMe() );

	}
	
	//Exibe a seta que indica o sentido da jogada
	public IEnumerator showMe(  ){
		yield return new WaitForSeconds (1);
		GameObject.Destroy( this.gameObject );
	}
}
